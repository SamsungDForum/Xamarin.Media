using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinMediaPlayer.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContentList : ScrollView
    {
        public static readonly BindableProperty FocusedContentProperty = BindableProperty.Create("FocusedContent", typeof(ContentItem), typeof(ContentList), default(ContentItem));
        public ContentItem FocusedContent
        {
            get { return (ContentItem)GetValue(FocusedContentProperty); }
            set { SetValue(FocusedContentProperty, value); }
        }

        public ContentList()
        {
            InitializeComponent();

            PropertyChanged += ContentFocusedChanged;
        }

        public bool SetFocus()
        {
            ContentItem item = ContentLayout.Children.First() as ContentItem;
            return item.SetFocus();
        }

        private void OnFocused(object sender, FocusEventArgs e)
        {
        }

        private void OnUnfocused(object sender, FocusEventArgs e)
        {
        }

        private void ContentFocusedChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("FocusedContent"))
            {
            }
        }
    }
}