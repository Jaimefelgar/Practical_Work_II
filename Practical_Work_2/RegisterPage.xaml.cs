using Microsoft.Maui.Controls;
using System.IO;

namespace Practical_Work_2;

public partial class RegisterPage : ContentPage
{
      private readonly string _usersFile = Path.Combine(Directory.GetCurrentDirectory(), "users.csv");

    public RegisterPage()
    {
        InitializeComponent();
    }

    private async void RegisterClicked(object sender, EventArgs e)
    {
        if (!ValidateInputs()) return;

        var newUser = new User
        {
            Name = Name.Text,
            Username = Username.Text,
            Password = Password.Text,
            OperationsCount = 0,
            AcceptedPolicy = PolicyCheckBox.IsChecked
        };

        SaveUser(newUser);
        await DisplayAlert("Éxito", "Registro completado!", "OK");
        await Shell.Current.GoToAsync("//MainPage");
    }

    private bool ValidateInputs()
    {
        try
        {
            if (Password.Text != RepeatPassword.Text)
            {
                DisplayAlert("Error", "Las contraseñas no coinciden", "OK");
                return false;
            }

            if (File.ReadAllLines(_usersFile).Any(u => u.Split(',')[1] == Username.Text))
            {
                DisplayAlert("Error", "El usuario ya existe", "OK");
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"Validación fallida: {ex.Message}", "OK");
            return false;
        }
    }

    private void SaveUser(User user)
    {
        try
        {
            using (StreamWriter sw = File.AppendText(_usersFile))
            {
                var line = $"{user.Name},{user.Username}," +
                          $"{user.Password},{user.OperationsCount}," +
                          $"{user.AcceptedPolicy}";
                sw.WriteLine(line);
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"Error al guardar: {ex.Message}", "OK");
        }
    }

    private async void PrivacyPolicyTapped(object sender, EventArgs e)
    {
        await DisplayAlert("Política de Privacidad", "Tus datos se almacenarán de forma segura y no serán compartidos con terceros.", "OK");
    }

    private async void ExitClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Salir", "¿Deseas salir de la aplicación?", "Sí", "No");
        if (confirm) Application.Current.Quit();
    }

    private async void OnSignInClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}

