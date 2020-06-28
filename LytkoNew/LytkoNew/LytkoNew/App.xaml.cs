using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LytkoNew
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage()) { 
                BackgroundColor = Color.FromHex("#2f2f2f"),
                BarBackgroundColor = Color.FromHex("#2f2f2f")
            };
        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        
    }
}
