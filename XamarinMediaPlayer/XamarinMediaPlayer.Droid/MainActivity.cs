using Android.App;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Runtime;
using Android.Views;
using XamarinMediaPlayer.Services;

namespace XamarinMediaPlayer.Droid
{
    [Activity(Label = "XamarinMediaPlayer.Droid", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.Transparent", 
        ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation)]
    public class MainActivity : FormsApplicationActivity, IKeyEventSender
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            RequestedOrientation = Android.Content.PM.ScreenOrientation.Landscape;

            Forms.Init(this, bundle);

            LoadApplication(new App());
        }

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            Xamarin.Forms.MessagingCenter.Send<IKeyEventSender, string>(this, "KeyDown", keyCode.ToString());

            return base.OnKeyDown(keyCode, e);
        }
    }
}

