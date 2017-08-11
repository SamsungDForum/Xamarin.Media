using System;
using System.Threading.Tasks;
using Tizen.Multimedia;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using XamarinMediaPlayer.Service;
using XamarinMediaPlayer.Tizen.Services;

[assembly: Dependency(typeof(PlayerService))]
namespace XamarinMediaPlayer.Tizen.Services
{
    class PlayerService : IPlayerService
    {
        private Player _player;

        public int Duration => _player == null ? 0 : _player.StreamInfo.GetDuration();

        public int CurrentPosition => _player == null ? 0 : _player.GetPlayPosition();

        public PlayerService()
        {
            var display = new Display(Forms.Context.MainWindow);

            _player = new Player();
            _player.Display = display;
        }

        public void Pause()
        {
            if (_player.State == PlayerState.Playing)
                _player.Pause();
        }

        public async Task PrepareAsync()
        {
            await _player.PrepareAsync();
        }

        public void SeekTo(int to)
        {
            _player.SetPlayPositionAsync(to, false);
        }

        public void SetSource(string uri)
        {
            var mediaSource = new MediaUriSource(uri);
            _player.SetSource(mediaSource);
        }

        public void Start()
        {
            if (_player.State == PlayerState.Ready ||
                _player.State == PlayerState.Paused)
                _player.Start();
        }

        public void Stop()
        {
            if (_player.State == PlayerState.Playing ||
                _player.State == PlayerState.Paused)
                _player.Stop();
        }
    }
}
