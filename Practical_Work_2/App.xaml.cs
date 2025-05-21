namespace Practical_Work_2;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		//user.csv path
        var usersPath = Path.Combine(Directory.GetCurrentDirectory(), "users.csv");
		//if users.csv doesnt exist it creates one.
        if (!File.Exists(usersPath))
        {
            File.WriteAllText(usersPath, "Name,Username,Password,OperationsCount,AcceptedPolicy\n");
        }
        

		MainPage = new AppShell();
	}
}
