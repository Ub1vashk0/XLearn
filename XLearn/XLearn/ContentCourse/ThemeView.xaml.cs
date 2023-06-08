using Android.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

using XLearn.ContentCourse;
using XLearn.DataConn;

namespace XLearn.ContentCourse
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ThemeView : ContentView
	{
        public ThemeView (string title, int status)
        {
			InitializeComponent ();
            NameTitle.Text = title;

            if (status == 0)
                Tick.IsVisible = false;
            else
                Tick.IsVisible = true;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContentThemePage(NameTitle.Text, GetIDTheme()));
        }

        private int GetIDTheme()
        {
            int buf = 0;
            ConnectionDBcs.OpenDB();
            var reader = ConnectionDBcs.GetDataReader("select IDTheme from Themes where Name = '"+ NameTitle.Text+"'");
            if(reader.Read())
                buf = reader.GetInt32(0);
            ConnectionDBcs.CloseDB();
            return buf;
        }
    }
}