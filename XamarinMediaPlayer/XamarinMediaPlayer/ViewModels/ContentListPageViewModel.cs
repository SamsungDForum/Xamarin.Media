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
            content.Info.Add("Big Buck Bunny");
            content.Info.Add("Big Buck Bunny is a comedy about a well-tempered rabbit Big Buck, who finds his day spoiled by the rude actions of the forest bullies, three rodents.In the typical 1950s cartoon tradition, BigBuck then prepares for the rodents in a comical revenge.");
            content.Image = "1_b.png";
            content.Bg = "1_a.png";
            content.Source = "http://distribution.bbb3d.renderfarming.net/video/mp4/bbb_sunflower_1080p_30fps_normal.mp4";
            content.ContentFocusedCommand = ContentFocusedCommand;
            ContentList.Add(content);

            content = new DetailContentData();
            content.Info.Add("Monument Valley");
            content.Info.Add("Xamarin.Forms is a cross-platform UI toolkit that allows developers to efficiently create native user interface layouts that can be shared across iOS, Android, Windows Phone, Windows Store, and Universal Windows Platform apps. This series introduces the basics of Xamarin.Forms development and covers building multi-platform and multi-screen applications.");
            content.Image = "2_b.png";
            content.Bg = "2_a.png";
            content.Source = "http://distribution.bbb3d.renderfarming.net/video/mp4/bbb_sunflower_1080p_30fps_normal.mp4";
            content.ContentFocusedCommand = ContentFocusedCommand;
            ContentList.Add(content);

            content = new DetailContentData();
            content.Info.Add("Aurora Borealis");
            content.Info.Add("Xamarin.Forms is a cross-platform UI toolkit that allows developers to efficiently create native user interface layouts that can be shared across iOS, Android, Windows Phone, Windows Store, and Universal Windows Platform apps. This series introduces the basics of Xamarin.Forms development and covers building multi-platform and multi-screen applications.");
            content.Image = "3_b.png";
            content.Bg = "3_a.png";
            content.Source = "http://distribution.bbb3d.renderfarming.net/video/mp4/bbb_sunflower_1080p_30fps_normal.mp4";
            content.ContentFocusedCommand = ContentFocusedCommand;
            ContentList.Add(content);

            content = new DetailContentData();
            content.Info.Add("Berlin");
            content.Info.Add("Xamarin.Forms is a cross-platform UI toolkit that allows developers to efficiently create native user interface layouts that can be shared across iOS, Android, Windows Phone, Windows Store, and Universal Windows Platform apps. This series introduces the basics of Xamarin.Forms development and covers building multi-platform and multi-screen applications.");
            content.Image = "4_b.png";
            content.Bg = "4_a.png";
            content.Source = "http://distribution.bbb3d.renderfarming.net/video/mp4/bbb_sunflower_1080p_30fps_normal.mp4";
            content.ContentFocusedCommand = ContentFocusedCommand;
            ContentList.Add(content);

            content = new DetailContentData();
            content.Info.Add("Windmill");
            content.Info.Add("Xamarin.Forms is a cross-platform UI toolkit that allows developers to efficiently create native user interface layouts that can be shared across iOS, Android, Windows Phone, Windows Store, and Universal Windows Platform apps. This series introduces the basics of Xamarin.Forms development and covers building multi-platform and multi-screen applications.");
            content.Image = "5_b.png";
            content.Bg = "5_a.png";
            content.Source = "http://distribution.bbb3d.renderfarming.net/video/mp4/bbb_sunflower_1080p_30fps_normal.mp4";
            content.ContentFocusedCommand = ContentFocusedCommand;
            ContentList.Add(content);

            content = new DetailContentData();
            content.Info.Add("Golf Course");
            content.Info.Add("Xamarin.Forms is a cross-platform UI toolkit that allows developers to efficiently create native user interface layouts that can be shared across iOS, Android, Windows Phone, Windows Store, and Universal Windows Platform apps. This series introduces the basics of Xamarin.Forms development and covers building multi-platform and multi-screen applications.");
            content.Image = "6_b.png";
            content.Bg = "6_a.png";
            content.Source = "http://distribution.bbb3d.renderfarming.net/video/mp4/bbb_sunflower_1080p_30fps_normal.mp4";
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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
