using System;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMediaPlayer.Controls;
using XamarinMediaPlayer.Services;

namespace XamarinMediaPlayer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerView : ContentPage
    {
        private IPlayerService _playerService;

        public static readonly BindableProperty ContentSourceProperty = BindableProperty.Create("ContentSource", typeof(string), typeof(PlayerView), default(string));
        public string ContentSource
        {
            set { SetValue(ContentSourceProperty, value); }
            get { return (string)GetValue(ContentSourceProperty); }
        }

        public PlayerView()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            _playerService = DependencyService.Get<IPlayerService>();
            _playerService.StateChanged += OnPlayerStateChanged;

            Play.Clicked += (s, e) =>
            {
                if (_playerService.State == PlayerState.Playing)
                    _playerService.Pause();
                else
                    _playerService.Start();
            };
            Next.Clicked += (s, e) =>
            {
            };
            Prev.Clicked += (s, e) =>
            {
            };

            PropertyChanged += PlayerViewPropertyChanged;
        }

        private void PlayerViewPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("ContentSource"))
            {
                if (ContentSource == null)
                    return;

                _playerService.SetSource(ContentSource);
                _playerService.PrepareAsync();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Play.Focus();
        }

        protected override void OnDisappearing()
        {
            _playerService.Stop();

            base.OnDisappearing();
        }

        private void OnPlayerStateChanged(object sender, Services.PlayerStateChangedEventArgs e)
        {
            if (e.State == PlayerState.Prepared)
            {
                _playerService.Start();
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
                CurrentTime.Text = GetFormattedTime(_playerService.CurrentPosition);
                TotalTime.Text = GetFormattedTime(_playerService.Duration);

                if (_playerService.Duration > 0)
                    Progressbar.Progress = _playerService.CurrentPosition / (float)_playerService.Duration;
                else
                    Progressbar.Progress = 0;
            });

            return _playerService.State == PlayerState.Playing;
        }
    }
}