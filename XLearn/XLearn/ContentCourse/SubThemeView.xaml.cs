using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLearn.DataConn;

namespace XLearn.ContentCourse
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SubThemeView : ContentView
	{
		public SubThemeView (string title, int status)
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
			await Navigation.PushAsync(new ContentSubThemePage(NameTitle.Text, GetIDSubtheme()));
        }

        private int GetIDSubtheme()
        {
            int buf = 0;
            ConnectionDBcs.OpenDB();
            var reader = ConnectionDBcs.GetDataReader("select IDSubtheme from Subthemes where Name = '" + NameTitle.Text + "'");
            if(reader.Read())
                buf = reader.GetInt32(0);
            ConnectionDBcs.CloseDB();
            return buf;
        }
    }
}