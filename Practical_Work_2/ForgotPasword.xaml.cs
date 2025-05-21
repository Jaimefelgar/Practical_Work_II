using Microsoft.Maui.Controls;
using System.IO;

namespace Practical_Work_2
{
    public partial class ForgotPassword : ContentPage
    {
      private readonly string _usersFile = Path.Combine(Directory.GetCurrentDirectory(), "users.csv");
        
        public ForgotPassword()
        {
            InitializeComponent();
        }
        private async void SigninClicked(object sender, EventArgs e)
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

        private async void ChangeButton(object sender, EventArgs e)
        {
            if (!NewPassword.IsVisible)
            {
                // Fase 1: Verificar usuario
                var user = FindUser(Username.Text);

                if (user == null)
                {
                    await DisplayAlert("Error", "Usuario no encontrado", "OK");
                    return;
                }

                // Mostrar campos de contraseña
                NewPassword.IsVisible = true;
                ConfirmNewPassword.IsVisible = true;
                SubmitButton.Text = "Cambiar contraseña";
            }
            else
            {
                // Fase 2: Cambiar contraseña
                if (NewPassword.Text != ConfirmNewPassword.Text)
                {
                    await DisplayAlert("Error", "Las contraseñas no coinciden", "OK");
                    return;
                }

                if (UpdatePassword(Username.Text, NewPassword.Text))
                {
                    await DisplayAlert("Éxito", "Contraseña actualizada correctamente", "OK");
                    await Navigation.PopAsync();
                }
            }
        }

        private User FindUser(string username)
        {
            if (!File.Exists(_usersFile)) return null;
            
            foreach (var line in File.ReadAllLines(_usersFile))
            {
                var parts = line.Split(',');
                if (parts.Length >= 2 && parts[1] == username)
                {
                    return new User
                    {
                        Name = parts[0],
                        Username = parts[1],
                        Password = parts[2],
                        OperationsCount = int.Parse(parts[3]),
                        AcceptedPolicy = bool.Parse(parts[4])
                    };
                }
            }
            return null;
        }

        private bool UpdatePassword(string username, string newPassword)
        {
            try
            {
                var lines = File.ReadAllLines(_usersFile);
                for (int i = 0; i < lines.Length; i++)
                {
                    var parts = lines[i].Split(',');
                    if (parts[1] == username)
                    {
                        parts[2] = newPassword;
                        lines[i] = string.Join(",", parts);
                        File.WriteAllLines(_usersFile, lines);
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}