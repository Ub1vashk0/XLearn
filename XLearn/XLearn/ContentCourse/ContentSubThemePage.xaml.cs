using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLearn.DataConn;
using XLearn.TestContent;
using Android.Media;
using XLearn.ContentCourse.PopUp;

namespace XLearn.ContentCourse
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContentSubThemePage : ContentPage
	{
        private List<UnitsContentView> listContent;
        private int currentIndex = 0;
        private int _idSubtheme;

		public ContentSubThemePage (string title, int IDSubtheme)
		{
			InitializeComponent ();
            NameTitle.Text = title;
            _idSubtheme = IDSubtheme;

            listContent = GetUnits();
            SetContent(listContent[currentIndex]);

            BackButton.IsVisible = false;
            ChangeProgres();
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                SetContent(listContent[currentIndex]);
                if (currentIndex == 0)
                    BackButton.IsVisible = false;
                else
                    BackButton.IsVisible = true;
            }
            NextButton.IsVisible = true;

            ChangeProgres();
        }

        private async void NextButton_Clicked(object sender, EventArgs e)
        {
            if (currentIndex < listContent.Count)
            {
                currentIndex++;


                if (currentIndex == listContent.Count)
                {
                    await Navigation.PushAsync(new StartTest(NameTitle.Text, _idSubtheme));
                    currentIndex = 3;
                }
                else
                {
                    SetContent(listContent[currentIndex]);
                    NextButton.IsVisible = true;
                    NextButton.Focus();
                }    
                    
            }
            BackButton.IsVisible = true;
            ChangeProgres();
        }
        private List<UnitsContentView> GetUnits()
        {
            List<UnitsContentView> list = new List<UnitsContentView>();
            ConnectionDBcs.OpenDB();
            var db = ConnectionDBcs.GetDataTable("select Text, ListingCode, Image from Contents where IDSubtheme = " + _idSubtheme);
            if(db.Rows.Count!=0 && db != null)
                foreach(DataRow row in db.Rows)
                {
                    if (!row.IsNull("Image") && !row.IsNull("ListingCode"))
                    {
                        byte[] imageBytes = (byte[])row["Image"];
                        ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(imageBytes));

                        list.Add(new UnitsContentView(row["Text"].ToString(), row["ListingCode"].ToString(), imageSource));
                    }
                    if (!row.IsNull("Image") && row.IsNull("ListingCode"))
                    {
                        byte[] imageBytes = (byte[])row["Image"];
                        ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(imageBytes));

                        list.Add(new UnitsContentView(row["Text"].ToString(), imageSource));
                    }
                    if (row.IsNull("Image") && !row.IsNull("ListingCode"))
                        list.Add(new UnitsContentView(row["Text"].ToString(), row["ListingCode"].ToString()));
                    if (row.IsNull("Image") && row.IsNull("ListingCode"))
                        list.Add(new UnitsContentView(row["Text"].ToString()));
                }
            ConnectionDBcs.CloseDB();
            return list;
        }

        private void SetContent(UnitsContentView units)
        {
            container.Children.Clear();
            container.Children.Add(units);
            NextButton.Focus();
        }

        private void ChangeProgres()
        {
            progressBar.Progress = (float)(currentIndex+1)/listContent.Count;
        }




        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Додаткові дії при активації сторінки (при поверненні назад)
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            // Додаткові дії при деактивації сторінки (при переході на іншу сторінку)
        }
    }
}