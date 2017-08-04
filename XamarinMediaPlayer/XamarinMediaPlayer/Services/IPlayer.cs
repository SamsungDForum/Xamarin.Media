using System;
using System.Threading.Tasks;

namespace HelloXamarinFormsWorldXaml
{
    public interface IPlayer
    {
        void SetSource(string uri);

        Task PrepareAsync();

        void SetDisplay(Object display);

        void Start();

        void Stop();

        void Pause();

        void SeekTo(int to);
    }
}
