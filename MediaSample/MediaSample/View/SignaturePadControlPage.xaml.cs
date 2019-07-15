using System;
using System.IO;
using System.Linq;
using SignaturePad.Forms;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MediaSample.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignaturePadControlPage : ContentPage
    {
        private Point[] points;
        public SignaturePadControlPage()
        {
            InitializeComponent();
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            var imageStream = await padView.GetImageStreamAsync(SignatureImageFormat.Png);

            // this is actually memory-stream so convertible to it
            var mstream = (MemoryStream)imageStream;

            //Unfortunately above mstream is not valid until you take it as byte array
            //mstream = new MemoryStream(mstream.ToArray());

            //Now you can
            img_result.Source = ImageSource.FromStream(() =>
            {
                var file = new MemoryStream(mstream.ToArray());
                mstream.Dispose();
                return file;
            });
            padView.StrokeColor = Color.Transparent;
        }

        private async void SaveVectorClicked(object sender, EventArgs e)
        {
            points = padView.Points.ToArray();
            await DisplayAlert("Conferma firma", "Firma vettoriale salvata.", "OK");
            btnLoad.IsVisible = true;
        }

        private void LoadVectorClicked(object sender, EventArgs e)
        {
                padView.Points = points;
        }

        private void PadView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (padView.IsBlank)
            {
                btnSaveImage.IsEnabled = false;
                btnSave.IsEnabled = false;
                btnLoad.IsEnabled = false;
            }
            else if (!padView.IsBlank)
            {
                btnSaveImage.IsEnabled = true;
                btnSave.IsEnabled = true;
                btnLoad.IsEnabled = true;
            }
        }

        private void PadView_Cleared(object sender, EventArgs e)
        {
            DisplayAlert("ATTENZIONE", "Per recuperare la firma cancellata premere il campo firma e poi CARICA VETTORIALE", "OK");
            padView.StrokeColor = Color.Black;
        }

        private void PadView_StrokeCompleted(object sender, EventArgs e)
        {
            btnSaveImage.IsEnabled = true;
            btnSave.IsEnabled = true;
            btnLoad.IsEnabled = true;
        }
    }
}