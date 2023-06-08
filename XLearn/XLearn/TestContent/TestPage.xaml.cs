using Java.Util.Concurrent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLearn.ContentCourse.PopUp;
using XLearn.DataConn;

namespace XLearn.TestContent
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TestPage : ContentPage
	{
		private List<QuestionFormView> _questions;
		private int currentIndex = 0;
		private int _idSubtheme;

		public TestPage (string title,int IDSubtheme)
		{
			InitializeComponent ();
			_idSubtheme = IDSubtheme;
			_questions = GetQuestion();

			NameTitle.Text = title;

			SetContent(_questions[currentIndex]);
			ChangeProgress();

            foreach (var question in _questions)
            {
                question.AnswerSelected += QuestionFormView_AnswerSelected;
            }

			NavigationPage.SetHasBackButton(this, false);
        }

        private List<QuestionFormView> GetQuestion()
        {
            List<QuestionFormView> list = new List<QuestionFormView>();
            ConnectionDBcs.OpenDB();
            var dt = ConnectionDBcs.GetDataTable("select IDQuestion from Question WHERE IDSubtheme = " + _idSubtheme);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new QuestionFormView(Int32.Parse(row["IDQuestion"].ToString())));
            }
            ConnectionDBcs.CloseDB();
            return list;
        }

        private async void NextButton_Clicked(object sender, EventArgs e)
        {
			if(currentIndex < _questions.Count)
			{
				currentIndex++;
				if (currentIndex == _questions.Count)
				{
					await Navigation.PushAsync(new ResultTest(NameTitle.Text, _idSubtheme));
					Navigation.RemovePage(this);
				}
				else
					SetContent(_questions[currentIndex]);
			}
			ChangeProgress();
        }

		private void SetContent(QuestionFormView form)
		{
			container.Children.Clear ();
			container.Children.Add(form);
			NextButton.IsVisible = false;
		}

		private void ChangeProgress()
		{
			progress.Progress = (float)(currentIndex + 1) / _questions.Count;
			IndexLabel.Text = (currentIndex + 1).ToString() + "/" + _questions.Count.ToString();
		}

        private void QuestionFormView_AnswerSelected(object sender, EventArgs e)
        {
            NextButton.IsVisible = true; // Зробити кнопку "NextButton" видимою
        }
    }
}