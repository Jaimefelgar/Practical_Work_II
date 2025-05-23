using Microsoft.Maui.Controls;
using System.IO;

namespace Practical_Work_2;

public partial class Conversor : ContentPage
{
    private readonly string UsersFile = Path.Combine(Directory.GetCurrentDirectory(),"users.csv");
    private Converter mainConverter;

    public Conversor()
    {
        InitializeComponent();
        mainConverter = new Converter();
    }

    private async void LogoutClicked(object sender, EventArgs e)
    {
        Preferences.Remove("currentUser");
        await Shell.Current.GoToAsync("//MainPage");
    }

    private void AcClicked(object sender, EventArgs e)
    {
        DisplayLabel.Text = "";
    }

    private async void ExitClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Salir", "¿Cerrar la aplicación?", "Sí", "No");
        if (confirm) Application.Current.Quit();
    }

    private void NumberButtonClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        DisplayLabel.Text += button.CommandParameter.ToString();
    }

    private async void ConversionButtonClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        if (!int.TryParse(button.CommandParameter?.ToString(), out int opIndex)) return;

        try
        {
            var conversion = mainConverter.Operations[opIndex];
            conversion.validate(DisplayLabel.Text);

            if (conversion.NeedBitSize())
            {
                var bits = await GetBitSize(conversion.GetName());
                if (bits > 0) DisplayLabel.Text = conversion.Change(DisplayLabel.Text, bits);
            }
            else
            {
                DisplayLabel.Text = conversion.Change(DisplayLabel.Text);
            }
            
            UpdateOperationsCount();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async Task<int> GetBitSize(string conversionName)
    {
        var bitsStr = await DisplayPromptAsync(
            "Configuración",
            $"Bits requeridos para {conversionName}:",
            "Aceptar",
            "Cancelar",
            maxLength: 2,
            keyboard: Keyboard.Numeric
        );

        return int.TryParse(bitsStr, out int bits) && bits > 0 ? bits : 0;
    }

    private async void OperationsClicked(object sender, EventArgs e)
   {
    try
    {
        var currentUser = Preferences.Get("currentUser", "");
        var userLine = File.ReadLines(UsersFile)
            .FirstOrDefault(u => u.Split(',')[1] == currentUser);

        if (userLine != null)
        {
            var userData = userLine.Split(',');
            await Navigation.PushAsync(new Operations(
                name: userData[0],
                username: userData[1],
                password: userData[2],
                operations: userData[3]
            ));
        }
    }
    catch (Exception ex)
    {
        await DisplayAlert("Error", $"Error al leer datos: {ex.Message}", "OK");
    }
}

    private void UpdateOperationsCount()
     {
        try
        {
            var currentUser = Preferences.Get("currentUser", "");
            var lines = File.ReadAllLines(UsersFile).ToList();

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Split(',')[1] == currentUser)
                {
                    var data = lines[i].Split(',');
                    data[3] = (int.Parse(data[3]) + 1).ToString();
                    lines[i] = string.Join(",", data);
                    break;
                }
            }
            
            File.WriteAllLines(UsersFile, lines);
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"Error al actualizar operaciones: {ex.Message}", "OK");
        }
    }
}