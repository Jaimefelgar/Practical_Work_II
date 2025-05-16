using System;
using Microsoft.Maui.Controls;
namespace Practical_Work_2
{

	public partial class Conversor : ContentPage
	{

		public Conversor()
		{
			InitializeComponent();
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
	

	}
}