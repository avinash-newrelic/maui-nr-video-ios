namespace MauiVideoApp;
using NewRelic.MAUI.Plugin;

public partial class App : Application
{
    public App()
    {
        
        InitializeComponent();

        MainPage = new MainPage();
        try
        {
            Console.WriteLine("CrossNewRelic.Current Initialization");
            CrossNewRelic.Current.HandleUncaughtException();
            Console.WriteLine("HandleUncaughtException called successfully.");

            CrossNewRelic.Current.TrackShellNavigatedEvents();
            Console.WriteLine("TrackShellNavigatedEvents called successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while initializing NewRelic: {ex.Message}");
        }

        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            CrossNewRelic.Current.Start("<YOUR_ANDROID_TOKEN>");
            Console.WriteLine("NewRelic started on Android.");
        }
        else if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
        {
            CrossNewRelic.Current.Start("AA334c44011a82745e868df6ae25c7da25ddf6ebc5-NRMA");
            Console.WriteLine("NewRelic started on iOS.");
        }
    }

}