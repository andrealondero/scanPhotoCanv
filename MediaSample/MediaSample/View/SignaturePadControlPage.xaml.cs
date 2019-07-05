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

        /*private async void Save_Clicked(object sender, EventArgs e)
        {
            bool saved;
            using (var bitmap = await padView.GetImageStreamAsync(SignatureImageFormat.Png, Color.Black, Color.White, 1f))
            {
                saved = await App.SaveSignature(bitmap, "signature.png");
            }

            if (saved)
                await DisplayAlert("Signature Pad", "Raster signature saved to the photo library.", "OK");
            else
                await DisplayAlert("Signature Pad", "There was an error saving the signature.", "OK");
        }
        /*{
        Stream image = await padView.GetImageStreamAsync(SignatureImageFormat.Jpeg);

        await Navigation.PopAsync();
    }*/

        private async void SaveVectorClicked(object sender, EventArgs e)
        {
            points = padView.Points.ToArray();
            await DisplayAlert("Signature Pad", "Vector signature saved to memory.", "OK");
        }

        private void LoadVectorClicked(object sender, EventArgs e)
        {
            padView.Points = points;
        }

        private void PadView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (padView.IsBlank)
            {
                btnSave.IsEnabled = false;
                btnLoad.IsEnabled = false;
            }
            else if (!padView.IsBlank)
            {
                btnSave.IsEnabled = true;
                btnLoad.IsEnabled = true;
            }
        }

        private void PadView_Cleared(object sender, EventArgs e)
        {
            DisplayAlert("ATTENZIONE", "Per recuperare la firma cancellata premere il campo firma e poi CARICA VETTORIALE", "OK");
        }
    }
}