using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMediaPlayer.Service;

namespace XamarinMediaPlayer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerView : ContentPage
    {
        IPlayerService PlayerService;
        public PlayerView()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            preparePlayer();
        }

        private async void preparePlayer()
        {
            PlayerService = DependencyService.Get<IPlayerService>();
            PlayerService.SetSource("http://yt-dash-mse-test.commondatastorage.googleapis.com/media/car-20120827-89.mp4");
            await PlayerService.PrepareAsync();
            PlayerService.Start();
        }
    }
}