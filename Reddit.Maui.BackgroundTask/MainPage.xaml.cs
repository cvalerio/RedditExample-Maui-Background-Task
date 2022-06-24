namespace Reddit.Maui.BackgroundTask;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
		MessagingCenter.Subscribe<MyHostedService, string>(this, "Tick", (sender, time) =>
		{
			MainThread.InvokeOnMainThreadAsync(() =>
			{
				Time.Text = time;
			});
		});
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

