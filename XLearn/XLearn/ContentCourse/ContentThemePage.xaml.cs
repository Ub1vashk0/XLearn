using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLearn.DataConn;

namespace XLearn.ContentCourse
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContentThemePage : ContentPage
	{
        private int _idTheme;
		public ContentThemePage (string title, int IDTheme)
		{
			InitializeComponent ();
			TitleView.Text = title;
            _idTheme = IDTheme;
			
		}

		private void UpdateSubtheme(int IDSubtheme)
		{
            containerSubtheme.Children.Clear ();   
			ConnectionDBcs.OpenDB();
			var db = ConnectionDBcs.GetDataTable("select Name, Status from Subthemes where IDTheme = "+ IDSubtheme);
            if (db.Rows.Count != 0 && db != null)
                foreach (DataRow row in db.Rows)
                    containerSubtheme.Children.Add(new SubThemeView(row["Name"].ToString(), Int32.Parse(row["Status"].ToString())));
            ConnectionDBcs.CloseDB();
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateSubtheme(_idTheme);
            Focus();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}