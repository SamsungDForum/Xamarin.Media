using ElmSharp;
using Xamarin.Forms.Platform.Tizen;
using XamarinMediaPlayer.Services;

namespace XamarinMediaPlayer.Tizen
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication, IKeyEventSender, ITapEventSender
    {
        EcoreEvent<EcoreKeyEventArgs> _keyDown;
        GestureLayer _gestureLayer;
        protected override void OnCreate()
        {
            base.OnCreate();

            _keyDown = new EcoreEvent<EcoreKeyEventArgs>(EcoreEventType.KeyDown, EcoreKeyEventArgs.Create);
            _keyDown.On += (s, e) =>
            {
                // Send key event to the portable project using MessagingCenter
                Xamarin.Forms.MessagingCenter.Send<IKeyEventSender, string>(this, "KeyDown", e.KeyName);
            };

            _gestureLayer = new GestureLayer(Forms.Context.MainWindow);
            _gestureLayer.SetTapCallback(GestureLayer.GestureType.Tap, GestureLayer.GestureState.End, (e) =>
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
