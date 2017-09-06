using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XamarinMediaPlayer.ViewModels;
using XamarinMediaPlayer.Models;
using XamarinMediaPlayer.Controls;
using XamarinMediaPlayer.Services;

namespace XamarinMediaPlayer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ContentListPage : ContentPage
    {
        NavigationPage AppMainPage;

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

        public ContentListPage(NavigationPage page)
        {
            InitializeComponent();

            AppMainPage = page;

            BgImage.Source = ImageSource.FromFile("content_list_bg.png");

            foreach (DetailContentData content in ((ContentListPageViewModel)BindingContext).ContentList)
            {
                ContentItem item = new ContentItem()
                {
                    BindingContext = content
                };
                item.OnContentSelect += new ContentSelectHandler(ContentSelected);
                ((StackLayout)ContentListView.Content).Children.Add(item);
            }

            NavigationPage.SetHasNavigationBar(this, false);

            PropertyChanged += ContentChanged;
        }

        private void ContentChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("FocusedContent"))
            {
                UpdateContentInfo();
            }
        }

        private void ContentSelected(ContentItem item)
        {
            var playerView = new PlayerView()
            {
                BindingContext = item.BindingContext
            };
            AppMainPage.PushAsync(playerView);
        }

        protected async void UpdateContentInfo()
        {
            ContentTitle.Text = FocusedContent.ContentTitle;
            ContentDesc.Text = FocusedContent.ContentDescription;

            ContentImage.Source = ImageSource.FromFile(FocusedContent.ContentImg);
            ContentImage.Opacity = 0;
            await ContentImage.FadeTo(1, 1000);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ContentListView.SetFocus();
        }
    }
}