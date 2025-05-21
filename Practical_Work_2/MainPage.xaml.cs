using System;
using Microsoft.Maui.Controls;
namespace Practical_Work_2
{

	public partial class MainPage : ContentPage
	{
		private readonly string _usersFile = Path.Combine(FileSystem.AppDataDirectory, "users.csv");
		public MainPage()
		{
			InitializeComponent();
		}

		private async void RegisterClicked(object sender, EventArgs e)
		{
			await Shell.Current.GoToAsync(nameof(RegisterPage));
		}
		private async void ExitClicked(object sender, EventArgs e)
		{
			bool confirm = await DisplayAlert("Exit", "¿Deseas salir de la aplicación?", "Sí", "No");
			if (confirm)
			{
				Application.Current.Quit();
			}
		}
		private async void SigninClicked(object sender, EventArgs e)
		{
			var users = File.Exists(_usersFile) ? File.ReadAllLines(_usersFile) : Array.Empty<string>();

			var user = users.FirstOrDefault(u =>
				u.Split(',')[1] == Username.Text &&
				u.Split(',')[2] == Password.Text);
				
			if (user != null)
			{
				Preferences.Set("currentUser", Username.Text);
				await Shell.Current.GoToAsync(nameof(Conversor));
			}
			else
			{
				await DisplayAlert("Error", "Invalid credentials", "OK");
			}
		}
		private async void ForgotPasswordTapped(object sender, EventArgs e)
    	{
        	await Shell.Current.GoToAsync(nameof(ForgotPassword));
    	}

}

}