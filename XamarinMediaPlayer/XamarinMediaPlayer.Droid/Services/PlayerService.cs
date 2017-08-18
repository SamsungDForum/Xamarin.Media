using System;
using System.Threading.Tasks;
using Android.App;
using Android.Graphics;
using Android.Media;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using XamarinMediaPlayer.Droid.Services;
using XamarinMediaPlayer.Services;

[assembly: Dependency(typeof(PlayerService))]
namespace XamarinMediaPlayer.Droid.Services
{
    class PlayerService : Java.Lang.Object, IPlayerService,
        MediaPlayer.IOnPreparedListener
    {
        private VideoView _videoView;
        private int _PrevUiOptions;
        private PlayerState _playerState = PlayerState.Idle;

        public event PlayerStateChangedEventHandler StateChanged;

        public int Duration => _videoView == null ? 0 : _videoView.Duration;

        public int CurrentPosition => _videoView == null ? 0 : _videoView.CurrentPosition;

        public PlayerState State
        {
            get { return _playerState; }
            private set
            {
                _playerState = value;
                StateChanged?.Invoke(this, new PlayerStateChangedEventArgs(_playerState));
            }
        }

        public PlayerService()
        {
            var decorView = (FrameLayout)((Activity)Forms.Context).Window.DecorView;

            _videoView = new VideoView(Forms.Context);
            _videoView.SetOnPreparedListener(this);
            decorView.AddView(_videoView, 0);

            var uiOptions = _PrevUiOptions = (int)decorView.SystemUiVisibility;
            var newUiOptions = (int)uiOptions;

            newUiOptions |= (int)SystemUiFlags.LowProfile;
            newUiOptions |= (int)SystemUiFlags.Fullscreen;
            newUiOptions |= (int)SystemUiFlags.HideNavigation;
            newUiOptions |= (int)SystemUiFlags.Immersive;

            decorView.SystemUiVisibility = (StatusBarVisibility)newUiOptions;
        }

        public void Pause()
        {
            _videoView.Pause();
            State = PlayerState.Paused;
        }

        public async Task PrepareAsync()
        {
        }

        public void SeekTo(int positionMs)
        {
            _videoView.SeekTo(positionMs);
        }

        public void SetSource(string uri)
        {
            _videoView.SetVideoPath(uri);
            State = PlayerState.Preparing;
        }

        public void Start()
        {
            _videoView.Start();
            State = PlayerState.Playing;
        }

        public void Stop()
        {
            _videoView.StopPlayback();
            State = PlayerState.Stopped;
        }

        public void OnPrepared(MediaPlayer mp)
        {
            State = PlayerState.Prepared;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var decorView = (FrameLayout)((Activity)Forms.Context).Window.DecorView;

                decorView.RemoveView(_videoView);
                decorView.SystemUiVisibility = (StatusBarVisibility)_PrevUiOptions;

                _videoView.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
