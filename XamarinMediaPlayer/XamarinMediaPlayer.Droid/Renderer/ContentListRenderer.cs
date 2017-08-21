using System.ComponentModel;
using Xamarin.Forms;
using XamarinMediaPlayer.Controls;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ContentList), typeof(ContentListRenderer))]
namespace XamarinMediaPlayer.Controls
{
    public class ContentListRenderer : ScrollViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            e.NewElement.PropertyChanged += OnElementPropertyChanged;
        }

        protected void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (ChildCount > 0)
            {
                GetChildAt(0).HorizontalScrollBarEnabled = false;
                GetChildAt(0).VerticalScrollBarEnabled = false;
            }
        }
    }
}