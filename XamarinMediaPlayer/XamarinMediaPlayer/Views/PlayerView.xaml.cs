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
        private readonly int DefaultTimeout = 5000;
        private readonly TimeSpan UpdateInterval = new TimeSpan(0, 0, 0, 0, 100);

        private IPlayerService _playerService;
        private int _hideTime;
        private bool _isPageDisappeared = false;
        private bool _isShowing = false;

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

            _playerService = DependencyService.Get<IPlayerService>(DependencyFetchTarget.NewInstance);
            _playerService.StateChanged += OnPlayerStateChanged;
            _playerService.PlaybackCompleted += OnPlaybackCompleted;

            Play.Clicked += (s, e) =>
            {
                if (_playerService.State == PlayerState.Playing)
                    _playerService.Pause();
                else
                    _playerService.Start();
            };

            PropertyChanged += PlayerViewPropertyChanged;

            MessagingCenter.Subscribe<IKeyEventSender, string>(this, "KeyDown", (s, e) =>
            {
                show();
            });
        }

        public void show()
        {
            show(DefaultTimeout);
        }

        public void show(int timeout)
        {
            if (!_isShowing)
            {
                Play.Focus();
                _isShowing = true;
            }
            Controller.IsVisible = true;
            _hideTime = timeout;
        }

        public void hide()
        {
            Controller.IsVisible = false;
            _isShowing = false;
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

        private void OnPlaybackCompleted(object sender, EventArgs e)
        {
            UpdatePlayTime();
            show();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Play.Focus();
            Device.StartTimer(UpdateInterval, UpdatePlayerControl);
        }

        protected override void OnDisappearing()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(0), () =>
            {
                _playerService.Dispose();
                _playerService = null;

                return false;
            });
            _isPageDisappeared = true;

            base.OnDisappearing();
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.RemovePage(this);

            return true;
        }

        private void OnPlayerStateChanged(object sender, Services.PlayerStateChangedEventArgs e)
        {
            if (e.State == PlayerState.Prepared)
            {
                _playerService.Start();
                _hideTime = DefaultTimeout;
            }
            else if (e.State == PlayerState.Playing)
            {
                Play.Image = "btn_viewer_control_pause_normal.png";
            }
            else if (e.State == PlayerState.Paused)
            {
                Play.Image = "btn_viewer_control_play_normal.png";
            }
            else if (e.State == PlayerState.Stopped)
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
            if (_isPageDisappeared)
                return false;

            Device.BeginInvokeOnMainThread(() => {
                if (_playerService.State != PlayerState.Playing)
                {
                    return;
                }

                UpdatePlayTime();

                if (_hideTime > 0)
                {
                    _hideTime -= (int)UpdateInterval.TotalMilliseconds;
                    if (_hideTime <= 0)
                    {
                        hide();
                    }
                }
            });

            return true;
        }

        private void UpdatePlayTime()
        {
            CurrentTime.Text = GetFormattedTime(_playerService.CurrentPosition);
            TotalTime.Text = GetFormattedTime(_playerService.Duration);

            if (_playerService.Duration > 0)
                Progressbar.Progress = _playerService.CurrentPosition / (float)_playerService.Duration;
            else
                Progressbar.Progress = 0;
        }
    }
}