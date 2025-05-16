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
	 	private async void ExitClicked(object sender, EventArgs e)
    	{
        	bool confirm = await DisplayAlert("Exit", "¿Deseas salir de la aplicación?", "Sí", "No");
        	if (confirm)
        	{
            	Application.Current.Quit();
       		}
    }
	

	}
}