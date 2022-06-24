namespace Reddit.Maui.BackgroundTask;

public partial class App : Application
{
	private readonly MyHostedService myHostedService;

	public App(MyHostedService myHostedService)
	{
		myHostedService.Start();
		InitializeComponent();

		MainPage = new AppShell();
		this.myHostedService = myHostedService;
	}

	protected override void OnResume()
	{
      myHostedService?.Start();
      base.OnResume();
	}

	protected override async void OnSleep()
	{
		await myHostedService?.StopAsync();
		base.OnSleep();
	}
}
