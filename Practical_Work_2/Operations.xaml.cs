using Microsoft.Maui.Controls;

namespace Practical_Work_2;

public partial class Operations : ContentPage
{
    public Operations(string name, string username, string password, string operations)
    {
        InitializeComponent();
        LoadUserData(name, username, password, operations);
    }

    private void LoadUserData(string name, string username, string password, string operations)
    {
        lblName.Text = $"Name: {name}";
        lblUsername.Text = $"Ussername: {username}";
        lblPassword.Text = $"Password: {password}";
        lblOperations.Text = $"Operations: {operations}";
    }

    private async void BackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    private async void ExitButtonClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Exit", "¿Deseas salir de la aplicación?", "Sí", "No");
        if (confirm)
        {
            Application.Current.Quit();
        }
    }
}