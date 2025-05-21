namespace Practical_Work_2
{

public partial class AppShell : Shell
{
		public AppShell()
		{
			InitializeComponent();
			Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
			Routing.RegisterRoute(nameof(Conversor), typeof(Conversor));
			Routing.RegisterRoute(nameof(ForgotPassword), typeof(ForgotPassword));
			Routing.RegisterRoute(nameof(Operations),typeof(Operations));
	}
}
}