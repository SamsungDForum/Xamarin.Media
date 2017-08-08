using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XamarinMediaPlayer.ViewModels;
using XamarinMediaPlayer.Models;
using XamarinMediaPlayer.Controls;

namespace XamarinMediaPlayer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ContentListPage : ContentPage
    {
        public static readonly BindableProperty FocusedContentProperty = BindableProperty.Create("FocusedContent", typeof(ContentItem), typeof(ContentListPage), default(ContentItem));
        public ContentItem FocusedContent
        {
            get
            {
                return (ContentItem)GetValue(FocusedContentProperty);
            }
            set
            {
                SetValue(FocusedContentProperty, value);
            }
        }

        public ContentListPage()
        {
            InitializeComponent();

            BgImage.Source = ImageSource.FromFile("content_list_bg.png");
            Dim.Color = Color.FromRgba(0, 0, 0, 32);

            foreach (DetailContentData content in ((ContentListPageViewModel)BindingContext).ContentList)
            {
                ContentItem item = new ContentItem();

                item.BindingContext = content;
                ((StackLayout)ContentListView.Content).Children.Add(item);
            }

            NavigationPage.SetHasNavigationBar(this, false);

            PropertyChanged += ContentFocusedChanged;
        }

        private void ContentFocusedChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("FocusedContent"))
            {
                UpdateContentInfo();
            }
        }

        protected void UpdateContentInfo()
        {
            List<string> info = FocusedContent.ContentInfo;

            ContentImage.Source = ImageSource.FromFile(FocusedContent.ContentImg);
            ContentTitle.Text = info.ElementAt(0);
            ContentDesc.Text = info.ElementAt(1);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ContentListView.SetFocus();
        }
    }
}