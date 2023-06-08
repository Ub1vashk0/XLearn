using Android.Service.Voice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using XLearn.DataConn;

namespace XLearn.ContentCourse
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContentCoursePage : ContentPage
	{
		private int _idCourse;
		public ContentCoursePage (string title, int IDCourse)
		{
			InitializeComponent ();
            TitleView.Text = title;

            UpdateTheme(_idCourse);
            _idCourse = IDCourse;
		}

		private void UpdateTheme(int IDCourse)
		{
			containerThemes.Children.Clear();
			ConnectionDBcs.OpenDB();
			var db = ConnectionDBcs.GetDataTable("select Name, Status from Themes where IDCourse = " + IDCourse);
			if (db.Rows.Count != 0 && db != null)
				foreach ( DataRow row in db.Rows )
					containerThemes.Children.Add(new ThemeView(row["Name"].ToString(), Int32.Parse(row["Status"].ToString())));
			ConnectionDBcs.CloseDB();
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateTheme(_idCourse);
			Focus();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            UpdateTheme(_idCourse);
        }
    }
}