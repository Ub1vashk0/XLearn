using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using XLearn.ContentCourse;
using Xamarin.Forms;

using XLearn.DataConn;
using Android.Telecom;
using Java.Nio.Channels;
using static Java.Text.Normalizer;
using static Java.Util.Jar.Attributes;
using System.Data;

namespace XLearn
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            ConnectionDBcs.CheckDB();
            ConnectionDBcs.OpenDB();

           ConnectionDBcs.CloseDB();
        }

        private async void IntroButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContentCoursePage("Вступ C#", 1));
        }

        private async void OOPButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContentCoursePage("Основи ООП", 2));
        }
        
        private async void XAMLButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContentCoursePage("Вступ SQLite", 3));
        }
    }
}
