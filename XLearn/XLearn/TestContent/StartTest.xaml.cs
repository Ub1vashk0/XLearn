using Android.App;
using Android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLearn.DataConn;
using XLearn.TestContent;
using Java.Nio.Channels;

namespace XLearn.ContentCourse.PopUp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartTest : ContentPage
	{
        private int _idSubtheme;
		public static TestInfo TestInfo { get; set; }

		public StartTest (string Title,int IDSubtheme)
		{
			InitializeComponent ();
			NameTitle.Text = "ТЕСТ: " + Title;
			_idSubtheme = IDSubtheme;
			TestInfo = new TestInfo(_idSubtheme);

			CountQuestion.Text = TestInfo.CountQuestion.ToString();
			MaxScore.Text = TestInfo.MaxScore.ToString();
			PassingScore.Text = TestInfo.PassingScore.ToString();

			NavigationPage.SetHasBackButton(this, false);
		}

        private void StartButton_Clicked(object sender, EventArgs e)
        {
			Navigation.PushAsync(new TestPage(NameTitle.Text, _idSubtheme));
			Navigation.RemovePage(this);
        }

        private void ExitButton_Clicked(object sender, EventArgs e)
        {
			Navigation.RemovePage(this);
        }
    }
}