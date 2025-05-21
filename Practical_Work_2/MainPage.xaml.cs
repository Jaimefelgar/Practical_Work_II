using Microsoft.Maui.Controls;
using System.Linq;

namespace Practical_Work_2;

public partial class MainPage : ContentPage
{
      private readonly string _usersFile = Path.Combine(Directory.GetCurrentDirectory(), "users.csv");

	public MainPage()
	{
		InitializeComponent();
		
    }

	private async void RegisterClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(RegisterPage));
		Console.WriteLine($"Ruta del archivo: {_usersFile}");
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
        try
        {
            if (!File.Exists(_usersFile))
            {
                await DisplayAlert("Error", "No hay usuarios registrados", "OK");
                return;
            }

            var users = File.ReadAllLines(_usersFile);
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
                await DisplayAlert("Error", "Credenciales inválidas", "OK");
            }
        }
        catch (IOException ex)
        {
            await DisplayAlert("Error", $"Error de acceso: {ex.Message}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error inesperado: {ex.Message}", "OK");
        }
    }

    private async void ForgotPasswordTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ForgotPassword));
    }
}