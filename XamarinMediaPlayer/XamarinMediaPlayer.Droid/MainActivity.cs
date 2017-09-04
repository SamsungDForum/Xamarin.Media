using Android.App;
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
        private GestureDetector _gestureDetector;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            RequestedOrientation = Android.Content.PM.ScreenOrientation.Landscape;
            _gestureDetector = new GestureDetector(new GestureTap());

            Forms.Init(this, bundle);

            LoadApplication(new App());
        }

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            Xamarin.Forms.MessagingCenter.Send<IKeyEventSender, string>(this, "KeyDown", keyCode.ToString());

            return base.OnKeyDown(keyCode, e);
        }

        public override bool DispatchTouchEvent(MotionEvent e)
        {
            _gestureDetector.OnTouchEvent(e);

            return base.DispatchTouchEvent(e);
        }

        class GestureTap : GestureDetector.SimpleOnGestureListener, ITapEventSender
        {
            public override bool OnSingleTapUp(MotionEvent e)
            {
                Xamarin.Forms.MessagingCenter.Send<ITapEventSender>(this, "Tap");

                return true;
            }
        }
    }
}

