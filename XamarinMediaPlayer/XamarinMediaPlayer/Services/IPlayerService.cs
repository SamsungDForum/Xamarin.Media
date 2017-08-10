using System;
using System.Threading.Tasks;

namespace XamarinMediaPlayer.Service
{
    public interface IPlayerService
    {
        int Duration { get; }

        int CurrentPosition { get; }

        void SetSource(string uri);

        Task PrepareAsync();

        void Start();

        void Stop();

        void Pause();

        void SeekTo(int positionMs);
    }
}
