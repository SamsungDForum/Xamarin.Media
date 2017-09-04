using ElmSharp;
using Xamarin.Forms.Platform.Tizen;
using XamarinMediaPlayer.Services;

namespace XamarinMediaPlayer.Tizen
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication, IKeyEventSender, ITapEventSender
    {
        protected override void OnCreate()
        {
            base.OnCreate();

            var keyDown = new EcoreEvent<EcoreKeyEventArgs>(EcoreEventType.KeyDown, EcoreKeyEventArgs.Create);
            keyDown.On += (s, e) =>
            {
                Xamarin.Forms.MessagingCenter.Send<IKeyEventSender, string>(this, "KeyDown", e.KeyName);
            };

            var gestureLayer = new GestureLayer(Forms.Context.MainWindow);
            gestureLayer.SetTapCallback(GestureLayer.GestureType.Tap, GestureLayer.GestureState.End, (e) =>
            {
                Xamarin.Forms.MessagingCenter.Send<ITapEventSender>(this, "Tap");
            });

            LoadApplication(new App());
        }

        static void Main(string[] args)
        {
            var app = new Program();
            global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
            app.Run(args);
        }
    }
}
