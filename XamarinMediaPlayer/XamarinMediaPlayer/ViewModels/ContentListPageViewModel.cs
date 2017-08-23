using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

using Xamarin.Forms;

using XamarinMediaPlayer.Models;
using XamarinMediaPlayer.Controls;

namespace XamarinMediaPlayer.ViewModels
{
    class ContentListPageViewModel : INotifyPropertyChanged
    {
        public List<DetailContentData> ContentList { get; set; }
        public ContentItem FocusedContent { get; set; }
        public ICommand ContentFocusedCommand
        {
            protected set;
            get;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ContentListPageViewModel()
        {
            ICommand ContentFocusedCommand = CreateFocusedCommand();

            ContentList = new List<DetailContentData>();
            DetailContentData content;

            content = new DetailContentData();
            content.Title = "Big Buck Bunny";
            content.Description = "Big Buck Bunny is a comedy about a well-tempered rabbit Big Buck, who finds his day spoiled by the rude actions of the forest bullies, three rodents.In the typical 1950s cartoon tradition, BigBuck then prepares for the rodents in a comical revenge.";
            content.Image = "img_1_b.png";
            content.Bg = "img_1_a.png";
            content.Source = "http://distribution.bbb3d.renderfarming.net/video/mp4/bbb_sunflower_1080p_30fps_normal.mp4";
            content.ContentFocusedCommand = ContentFocusedCommand;
            ContentList.Add(content);

            content = new DetailContentData();
            content.Title = "Caminandes:Llamigos";
            content.Description = "In this episode of the Caminandes cartoon series we learn to know our hero Koro even better! It's winter in Patagonia, food is getting scarce. Koro the Llama engages with Oti the pesky penguin in an epic fight over that last tasty berry.";
            content.Image = "img_2_b.png";
            content.Bg = "img_2_a.png";
            content.Source = "http://www.caminandes.com/download/03_caminandes_llamigos_1080p.mp4";
            content.ContentFocusedCommand = ContentFocusedCommand;
            ContentList.Add(content);

            content = new DetailContentData();
            content.Title = "Monkaa";
            content.Description = "Monkaa is a blue-furred, pink-faced monkey who consumes a crystallized meteorite, making him invincibly strong and too hot to handle. Exploring his new superpowers, Monkaa zooms through an entire universe.";
            content.Image = "img_3_b.png";
            content.Bg = "img_3_a.png";
            content.Source = "http://tylerburkhardt.com/wats1010-embedded-media/media/video/monkaa_1080p.mp4";
            content.ContentFocusedCommand = ContentFocusedCommand;
            ContentList.Add(content);

            content = new DetailContentData();
            content.Title = "Sintel";
            content.Description = "The film follows a girl named Sintel who is searching for a baby dragon she calls Scales. A flashback reveals that Sintel found Scales with its wing injured and helped care for it, forming a close bond with it. By the time its wing recovered and it was able to fly, Scales was caught by an adult dragon. Sintel has since embarked on a quest to rescue Scales, fending off beasts and warriors along the way.";
            content.Image = "img_4_b.png";
            content.Bg = "img_4_a.png";
            content.Source = "http://download.blender.org/durian/trailer/sintel_trailer-1080p.mp4";
            content.ContentFocusedCommand = ContentFocusedCommand;
            ContentList.Add(content);

            content = new DetailContentData();
            content.Title = "Glass Half";
            content.Description = "Two amateur art critics meet in a gallery and argue passionately about the pieces they see, causing chaos for everyone else, until finally they find a piece on which they can agree...";
            content.Image = "img_5_b.png";
            content.Bg = "img_5_a.png";
            content.Source = "";
            content.ContentFocusedCommand = ContentFocusedCommand;
            ContentList.Add(content);

            content = new DetailContentData();
            content.Title = "Elephants Dream";
            content.Description = "Elephants Dream is the culmination of the Orange Open Movie Project, produced during 2005-2006 by the Blender Foundation and the Netherlands Media Art Institute. The film tells the story of Emo and Proog, two people with different visions of the surreal world in which they live. Viewers are taken on a journey through that world, full of strange mechanical birds, stunning technological vistas and machinery that seems to have a life of its own.";
            content.Image = "img_6_b.png";
            content.Bg = "img_6_a.png";
            content.Source = "";
            content.ContentFocusedCommand = ContentFocusedCommand;
            ContentList.Add(content);
        }

        protected ICommand CreateFocusedCommand()
        {
            ICommand command = new Command<ContentItem>((item) =>
            {
                FocusedContent = item;
                OnPropertyChanged("FocusedContent");
            });

            return command;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
