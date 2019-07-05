using MediaSample.View;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace MediaSample
{
  public partial class MediaPage : ContentPage
  {
    public MediaPage()
    {
      InitializeComponent();

			takePhoto.Clicked += async (sender, args) =>
			{

				if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
				{
					await DisplayAlert("ERRORE", ":( Fotocamera non disponibile", "OK");
					return;
				}

				var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
				{
					Directory = "Test",
					SaveToAlbum = true,
					CompressionQuality = 75,
					CustomPhotoSize = 50,
					PhotoSize = PhotoSize.MaxWidthHeight,
					MaxWidthHeight = 2000,
					DefaultCamera = CameraDevice.Rear
				});

				if (file == null)
					return;

				await DisplayAlert("Salvataggio in", file.Path, "OK");

				if (image.Source == null)
				{
					image.Source = ImageSource.FromStream(() =>
					{
						var stream = file.GetStream();
						file.Dispose();
						return stream;
					});
				}
				else if (image.Source != null)
				{
					imageAdded1.Source = ImageSource.FromStream(() =>
					{
						var stream = file.GetStream();
						file.Dispose();
						return stream;
					});
				}
				else if (image.Source != null && imageAdded1.Source != null)
				{
					imageAdded2.Source = ImageSource.FromStream(() =>
					{
						var stream = file.GetStream();
						file.Dispose();
						return stream;
					});
					pickPhoto.IsVisible = false;
				}
			};

			pickPhoto.Clicked += async (sender, args) =>
			{
				if (!CrossMedia.Current.IsPickPhotoSupported)
				{
					await DisplayAlert("ERRORE", ":( Accesso a galleria non consentito", "OK");
					return;
				}
				var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
				{
					PhotoSize =  Plugin.Media.Abstractions.PhotoSize.Medium,
				});
				if (file == null)
					return;

				image.Source = ImageSource.FromStream(() =>
				{
					var stream = file.GetStream();
					file.Dispose();
					return stream;
				});
			};

			takeCode.Clicked += async (sender, args) =>
			{
				var scanner = new ZXingScannerPage();
				await Navigation.PushAsync(scanner);
				scanner.OnScanResult += (result) =>
				{
					Device.BeginInvokeOnMainThread(async () =>
					{
						await Navigation.PopAsync();
						scannedCode.Text = result.Text;
					});
				};
			};

            takeFirma.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new SignaturePadControlPage());
            };
        }
	}
}
