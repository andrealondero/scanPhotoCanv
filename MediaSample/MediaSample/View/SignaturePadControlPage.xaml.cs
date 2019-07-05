using System;
using System.IO;

using SignaturePad.Forms;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MediaSample.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignaturePadControlPage : ContentPage
    {
        public SignaturePadControlPage()
        {
            InitializeComponent();
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            Stream image = await padView.GetImageStreamAsync(SignatureImageFormat.Png);        
            await Navigation.PopAsync();
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            padView.Clear();
        }
    }
}