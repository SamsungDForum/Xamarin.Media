using System;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMediaPlayer.Services;

namespace XamarinMediaPlayer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerView : ContentPage
    {
        private IPlayerService playerService;
        private int hideTime;

        public PlayerView()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            preparePlayer();

            Play.Clicked += (s, e) =>
            {
                if (playerService.State == PlayerState.Playing)
                    playerService.Pause();
                else
                    playerService.Start();
            };
            Next.Clicked += (s, e) =>
            {
            };
            Prev.Clicked += (s, e) =>
            {
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Play.Focus();
        }

        private async void preparePlayer()
        {
            playerService = DependencyService.Get<IPlayerService>();
            playerService.StateChanged += OnPlayerStateChanged;
            playerService.SetSource("http://yt-dash-mse-test.commondatastorage.googleapis.com/media/car-20120827-89.mp4");
            playerService.PrepareAsync();
        }

        private void OnPlayerStateChanged(object sender, Services.PlayerStateChangedEventArgs e)
        {
            if (e.State == PlayerState.Prepared)
            {
                playerService.Start();
                Device.StartTimer(new TimeSpan(0, 0, 0, 0, 500), UpdatePlayerControl);
            }
            else if (e.State == PlayerState.Playing)
            {
                Play.Image = "btn_viewer_control_pause_normal.png";
            }
            else if (e.State == PlayerState.Paused)
            {
                Play.Image = "btn_viewer_control_play_normal.png";
            }
        }

        private string GetFormattedTime(int miliseconds)
        {
            var time = TimeSpan.FromMilliseconds(miliseconds);

            if (time.TotalHours > 1)
                return time.ToString(@"hh\:mm\:ss");
            else
                return time.ToString(@"mm\:ss");
        }

        private bool UpdatePlayerControl()
        {
            Device.BeginInvokeOnMainThread(() => {
                CurrentTime.Text = GetFormattedTime(playerService.CurrentPosition);
                TotalTime.Text = GetFormattedTime(playerService.Duration);

                if (playerService.Duration > 0)
                    Progressbar.Progress = playerService.CurrentPosition / playerService.Duration;
                else
                    Progressbar.Progress = 0;
            });

            return playerService.State == PlayerState.Playing;
        }
    }
}