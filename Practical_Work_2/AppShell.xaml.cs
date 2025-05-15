namespace Practical_Work_2
{

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(LoginPage),typeof(LoginPage));
	}
}
}