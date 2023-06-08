using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLearn.ContentCourse.PopUp;

namespace XLearn.TestContent
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResultTest : ContentPage
	{
        private int _idSubtheme;
		public ResultTest (string title, int IDSubtheme)
		{
			InitializeComponent ();
            NameTitle.Text = title;
            _idSubtheme = IDSubtheme;
            //DataConn.ConnectionDBcs.OpenDB();
            StartTest.TestInfo.ReturnResult(MessageText);
            CountQuestion.Text = StartTest.TestInfo.CountQuestion.ToString();
            MaxScore.Text = StartTest.TestInfo.MaxScore.ToString();
            PassingScore.Text = StartTest.TestInfo.PassingScore.ToString();
            ResultScore.Text = StartTest.TestInfo.Result.ToString();
            //DataConn.ConnectionDBcs.CloseDB();
        }

        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            Navigation.RemovePage(this);   
            Navigation.RemovePage(this);
        }
    }
}