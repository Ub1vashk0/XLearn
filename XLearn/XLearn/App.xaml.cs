using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLearn.ContentCourse.PopUp;
using XLearn.TestContent;
namespace XLearn
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

            //if (!Directory.Exists(Environment.SpecialFolder.Personal.ToString()))
            //{
            //    Directory.CreateDirectory(Environment.SpecialFolder.Personal.ToString());
            //}
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
