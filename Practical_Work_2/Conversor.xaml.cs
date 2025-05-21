using System;
using Microsoft.Maui.Controls;

namespace Practical_Work_2
{


	public partial class Conversor : ContentPage
	{
		private readonly string _usersFile = Path.Combine(FileSystem.AppDataDirectory, "users.csv");
		private Converter mainconverter;

		public Conversor()
		{
			InitializeComponent();
			mainconverter = new Converter();
		}
		private async void LogoutClicked(object sender, EventArgs e)
		{
			await Shell.Current.GoToAsync("//MainPage");
		}
		private void AcClicked(object sender, EventArgs e)
		{
			DisplayLabel.Text = "";
		}
		private async void ExitClicked(object sender, EventArgs e)
		{
			bool confirm = await DisplayAlert("Exit", "¿Deseas salir de la aplicación?", "Sí", "No");
			if (confirm)
			{
				Application.Current.Quit();
			}
		}

		private void OnNumberButtonClicked(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			string number = button.CommandParameter.ToString();


			DisplayLabel.Text += number;
		}
		private async void OnConversionButtonClicked(object sender, EventArgs e)
		{
    		var button = (Button)sender;
    		if (int.TryParse(button.CommandParameter?.ToString(), out int opIndex))
    		{
       			try
        		{
            		string input = DisplayLabel.Text;
            		Conversion conversion = mainconverter.Operations[opIndex];
            
            		conversion.validate(input);
            
            		if (conversion.NeedBitSize())
            		{
                		string bitsStr = await DisplayPromptAsync(
                    	"Configuración",
                    	$"Bits requeridos para {conversion.GetName()}:",
                    	"Aceptar",
                    	"Cancelar",
                    	maxLength: 3,
                    	keyboard: Keyboard.Numeric
                		);

                	if (int.TryParse(bitsStr, out int bits) && bits > 0)
                	{
                    	DisplayLabel.Text = conversion.Change(input, bits);
               		}
                	else
                	{
                    	await DisplayAlert("Error", "Número de bits inválido", "OK");
                	}
            		}
            		else
            		{
                	DisplayLabel.Text = conversion.Change(input);
            		}
        		}
        		catch (ArgumentOutOfRangeException ex)
        		{
            		await DisplayAlert("Error", ex.Message, "OK");
        		}
        		catch (Exception ex)
        		{
          			await DisplayAlert("Error", ex.Message, "OK");
        		}
    		}
    	}

		private async void OperationsClicked(object sender, EventArgs e)
		{
    		var currentUsername = Preferences.Get("currentUser", "");
    		var userLine = File.ReadAllLines(_usersFile)
        	.FirstOrDefault(u => u.Split(',')[1] == currentUsername);

    		if (userLine != null)
    		{
        		var userData = userLine.Split(',');
        		await DisplayAlert("Tus operaciones",
            		$"Nombre: {userData[0]}\n" +
            		$"Usuario: {userData[1]}\n" +
            		$"Contraseña: {userData[2]}\n" +
            		$"Operaciones: {userData[3]}", 
            		"OK");
    		}
		}

		private void UpdateOperationsCount()
		{
    		var currentUsername = Preferences.Get("currentUser", "");
    		var lines = File.ReadAllLines(_usersFile);
    
    		for (int i = 0; i < lines.Length; i++)
    		{
       			if (lines[i].Split(',')[1] == currentUsername)
        		{
            		var data = lines[i].Split(',');
            		data[3] = (int.Parse(data[3]) + 1).ToString();
            		lines[i] = string.Join(",", data);
            		break;
        		}
    		}
    	File.WriteAllLines(_usersFile, lines);
		}
	}
}