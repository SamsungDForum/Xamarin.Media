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
using XamarinMediaPlayer.Service;
using static Android.Media.MediaPlayer;

[assembly: Dependency(typeof(PlayerService))]
namespace XamarinMediaPlayer.Droid.Services
{
    class PlayerService : Java.Lang.Object, IPlayerService
    {
        private VideoView _view;
        private int _PrevUiOptions;

        public int Duration => _view == null ? 0 : _view.Duration;

        public int CurrentPosition => _view == null ? 0 : _view.CurrentPosition;

        public PlayerService()
        {
            var decorView = (FrameLayout)((Activity)Forms.Context).Window.DecorView;

            _view = new VideoView(Forms.Context);
            decorView.AddView(_view, 0);

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
            _view.Pause();
        }

        public async Task PrepareAsync()
        {
        }

        public void SeekTo(int positionMs)
        {
            _view.SeekTo(positionMs);
        }

        public void SetSource(string uri)
        {
            _view.SetVideoPath(uri);
        }

        public void Start()
        {
            _view.Start();
        }

        public void Stop()
        {
            _view.StopPlayback();
        }
    }
}
