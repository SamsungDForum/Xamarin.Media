using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XamarinMediaPlayer.Views;

namespace XamarinMediaPlayer
{
    public class App : Application
    {
        public static NavigationPage AppMainPage { get; private set; }

        public App()
        {
            MainPage = new NavigationPage(new ContentListPage());
            AppMainPage = MainPage as NavigationPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
