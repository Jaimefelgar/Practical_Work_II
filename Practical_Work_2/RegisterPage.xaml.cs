using System;
using Microsoft.Maui.Controls;
namespace Practical_Work_2
{

	public partial class RegisterPage : ContentPage
	{
		private const string Usersfile = "users.csv";
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
			await DisplayAlert("Success", "Registration successful!", "OK");
			await Shell.Current.GoToAsync("//MainPage");
		}

		private bool ValidateInputs()
		{
			// Add all validation logic here
			if (Password.Text != RepeatPassword.Text)
			{
				DisplayAlert("Error", "Passwords do not match", "OK");
				return false;
			}

			return true;
		}

		private void SaveUser(User user)
		{
			var line = $"{user.Name},{user.Username},{user.Password},{user.OperationsCount},{user.AcceptedPolicy}";
			File.AppendAllLines(Usersfile, new[] { line });
		}

		private async void PrivacyPolicyTapped(object sender, EventArgs e)
		{
			await DisplayAlert("Privacy Policy", "Your privacy policy text here...", "OK");
		}

		private async void ExitClicked(object sender, EventArgs e)
		{
			bool confirm = await DisplayAlert("Exit", "¿Deseas salir de la aplicación?", "Sí", "No");
			if (confirm)
			{
				Application.Current.Quit();
			}
		}
		private async void OnSignInClicked(object sender, EventArgs e)
		{
			await Shell.Current.GoToAsync("//MainPage");
		}


}
}
