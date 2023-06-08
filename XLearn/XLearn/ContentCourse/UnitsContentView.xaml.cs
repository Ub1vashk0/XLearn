using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XLearn.ContentCourse
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UnitsContentView : ContentView
	{
		public UnitsContentView (string text)
		{
			InitializeComponent ();
			PlaceText.Text = text;

            CodeListing.IsVisible = false;
            ImageContainer.IsVisible = false;   
		}

        public UnitsContentView(string text, string code)
        {
            InitializeComponent();
            PlaceText.Text = text;
            CodeListing.Text = code;

            ImageContainer.IsVisible = false;
        }

        public UnitsContentView(string text, ImageSource imageSource)
        {
            InitializeComponent();
            PlaceText.Text = text;
            ImageContainer.Source = imageSource;

            CodeListing.IsVisible = false;
        }

        public UnitsContentView(string text, string code, ImageSource imageSource)
        {
            InitializeComponent();
            PlaceText.Text = text;
            CodeListing.Text = code;
            ImageContainer.Source = imageSource;
        }
    }
}