using AVFoundation;
using CoreGraphics;
using Foundation;
using UIKit;
using MyBindingIosLibrary;
using Microsoft.Maui.Platform; // Important for GetPlatformView()

namespace MauiVideoApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // You might want to set some initial layout constraints in XAML
            // or adjust them here if needed for the native view.
        }

        protected override void OnHandlerChanged()
        {
            base.OnHandlerChanged();

#if IOS
            PlayVideo();
#endif
        }

        private void PlayVideo()
        {
            if (Handler?.PlatformView is UIView uiView)
            {
                // URL of the video to play
                var videoUrl = NSUrl.FromString("http://docs.evostream.com/sample_content/assets/hls-bunny-rangerequest/playlist.m3u8");

                if (videoUrl != null)
                {
                    // Create an AVPlayer for the specified URL
                    var player = new AVPlayer(videoUrl);
					 var tracker = new NRTrackerAVPlayer(player);

                	// Start New Relic video tracking and get the tracker ID
                	NSNumber trackerId = NewRelicVideoAgent.SharedInstance.StartWithContentTracker(tracker);
					Console.WriteLine($"New Relic Tracker ID: {trackerId}");
                	System.Diagnostics.Debug.WriteLine($"New Relic Tracker ID: {trackerId}");

                    // Create an AVPlayerLayer to display the video
					var playerLayer = AVPlayerLayer.FromPlayer(player);

                    // Define your desired width and height for the video player
                    nfloat desiredWidth = 400; // Example width in device-independent units
                    nfloat desiredHeight = 300; // Example height in device-independent units

                    // Set the frame (position and size) of the AVPlayerLayer
                    // The origin (0, 0) places it at the top-left of the UIView's bounds
                    playerLayer.Frame = new CGRect(0, 0, desiredWidth, desiredHeight);

                    // Set the AutoresizingMask to control how the layer resizes with its parent view
                    // UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight will make it resize proportionally
                    uiView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
                    // Alternatively, if you want to maintain the initial size:
                    // uiView.AutoresizingMask = UIViewAutoresizing.None;

                    // Add the playerLayer as a sublayer to the UIView's layer
                    uiView.Layer.AddSublayer(playerLayer);

                    // Start video playback
                    player.Play();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Error: Invalid video URL.");
                }
            }
        }
    }
}