using GmailAppClone.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GmailAppClone
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ListEmailsPage()) {
                BarBackgroundColor = Color.FromHex("#CB4335")
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
