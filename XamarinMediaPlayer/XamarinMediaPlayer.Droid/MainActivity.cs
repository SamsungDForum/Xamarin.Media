using Android.App;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;

namespace XamarinMediaPlayer.Droid
{
    [Activity(Label = "XamarinMediaPlayer.Droid", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.Transparent", 
        ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation)]
    public class MainActivity : FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            RequestedOrientation = Android.Content.PM.ScreenOrientation.Landscape;

            Forms.Init(this, bundle);

            LoadApplication(new App());
        }
    }
}

