using Marvel_Api.View;

namespace Marvel_Api;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
        //	MainPage =new NewPage1();

    }
}
