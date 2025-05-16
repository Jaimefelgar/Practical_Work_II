using System;
using Microsoft.Maui.Controls;

namespace Practical_Work_2
{


	public partial class Conversor : ContentPage
	{
		private Converter mainconverter ;

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
                
                		// Validar y convertir
                		string result = mainconverter.PerformConversion(opIndex+1, input); // +1 porque PerformConversion usa base 1
                
                		// Mostrar resultado
               			DisplayLabel.Text = result;
           			}
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }


	}
}