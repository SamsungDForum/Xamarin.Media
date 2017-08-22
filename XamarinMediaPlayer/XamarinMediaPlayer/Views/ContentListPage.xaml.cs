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

        public static readonly BindableProperty SelectedContentProperty = BindableProperty.Create("SelectedContent", typeof(ContentItem), typeof(ContentListPage), default(ContentItem));
        public ContentItem SelectedContent
        {
            get
            {
                return (ContentItem)GetValue(SelectedContentProperty);
            }
            set
            {
                SetValue(SelectedContentProperty, value);
            }
        }

        public ContentListPage(NavigationPage page)
        {
            InitializeComponent();

            AppMainPage = page;

            BgImage.Source = ImageSource.FromFile("content_list_bg.png");
            Dim.Color = Color.FromRgba(0, 0, 0, 32);

            foreach (DetailContentData content in ((ContentListPageViewModel)BindingContext).ContentList)
            {
                ContentItem item = new ContentItem();

                item.BindingContext = content;
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
            else if (e.PropertyName.Equals("SelectedContent"))
            {
                AppMainPage.PushAsync(new PlayerView());
            }
        }

        protected void UpdateContentInfo()
        {
            ContentImage.Source = ImageSource.FromFile(FocusedContent.ContentImg);
            ContentTitle.Text = FocusedContent.ContentTitle;
            ContentDesc.Text = FocusedContent.ContentDescription;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ContentListView.SetFocus();
        }
    }
}