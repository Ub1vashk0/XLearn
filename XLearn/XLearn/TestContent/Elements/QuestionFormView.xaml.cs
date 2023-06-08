using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLearn.ContentCourse.PopUp;
using XLearn.DataConn;

namespace XLearn.TestContent
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionFormView : ContentView
    {
        public event EventHandler AnswerSelected;

        private int _idQuestion;

        public QuestionFormView(int IDQuestion):base()
        {
            InitializeComponent();
            _idQuestion = IDQuestion;

            GetTextQuestion();
            CreateAnswerButtons();
            SubscribeToAnswerSelectedEvent();
        }

        private void GetTextQuestion()
        {
            ConnectionDBcs.OpenDB();
            var dr = ConnectionDBcs.GetDataReader("SELECT Text, Listing from Question where IDQuestion = " + _idQuestion);
            if (dr.Read())
            {
                QuestionText.Text = dr.GetString(0);
                if (!dr.IsDBNull(1))
                {
                    CodeListing.IsVisible = true;
                    CodeListing.Text = dr.GetString(1);
                }
            }
            ConnectionDBcs.CloseDB();
        }
        private void CreateAnswerButtons()
        {
            bool buf = false;
            ConnectionDBcs.OpenDB();
            var dt = ConnectionDBcs.GetDataTable("SELECT Text, Status from Answers where IDQuestion = " + _idQuestion);
            if (dt.Rows.Count != 0 && dt != null)
                foreach (DataRow row in dt.Rows)
                {
                    if (Int32.Parse(row["Status"].ToString()) == 1)
                        buf = true;
                    if (Int32.Parse(row["Status"].ToString()) == 0)
                        buf = false;
                    AnswersContainer.Children.Add(new AnswerButton(row["Text"].ToString(), buf));
                }
            ConnectionDBcs.CloseDB();
        }
        private void SubscribeToAnswerSelectedEvent()
        {
            foreach (var answerButton in AnswersContainer.Children.OfType<AnswerButton>())
            {
                answerButton.AnswerSelected += OnAnswerSelected;
            }
        }

        private void OnAnswerSelected(object sender, bool Status)
        {
            var butt = sender as AnswerButton;

            if (Status)
            {
                // Відповідь правильна
                butt.BackgroundColor = Color.Green;
                StartTest.TestInfo.AddScore(_idQuestion);
            }
            else
            {
                // Відповідь неправильна
                butt.BackgroundColor = Color.Red;
            }
            AnswersContainer.IsEnabled = false;

            AnswersContainer.IsEnabled = false;
            AnswerSelected?.Invoke(this, EventArgs.Empty);
        }
    }
    public partial class AnswerButton : Button
    {
        public event EventHandler<bool> AnswerSelected;
        private bool Status;

        public AnswerButton(string text, bool status)
        {
            Text = text;
            Status = status;

            Style = (Style)Application.Current.Resources["Ans"];

            Clicked += OnAnswerButtonClicked;
        }

        private void OnAnswerButtonClicked(object sender, EventArgs e)
        {
            AnswerSelected?.Invoke(this, Status);
        }
    }
}