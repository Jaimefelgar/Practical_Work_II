using System;
using Microsoft.Maui.Controls;
namespace Practical_Work_2
{

public partial class MainPage : ContentPage
{

	public MainPage()
	{
        InitializeComponent();
	}

	private async void LoginClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(LoginPage));
	}
	 private async void ExitClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Exit", "¿Deseas salir de la aplicación?", "Sí", "No");
        if (confirm)
        {
            Application.Current.Quit(); // Cierra la aplicación
        }
    }
    private async void SigninClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(Conversor));
	}

}

}