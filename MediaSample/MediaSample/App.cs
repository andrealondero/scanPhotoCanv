using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MediaSample
{
  public class App : Application
  {
        private readonly Func<Stream, string, Task<bool>> saveSignatureDelegate;
        public App()
    {
      // The root page of your application
      MainPage = new NavigationPage (new MediaPage());
    }

        public static Task<bool> SaveSignature(Stream bitmap, string filename)
        {
            return ((App)Application.Current).saveSignatureDelegate(bitmap, filename);
        }

        protected override void OnStart()
    {
      // Handle when your app starts
    }

    protected override void OnSleep()
    {
      // Handle when your app sleeps
    }

    protected override void OnResume()
    {
      // Handle when your app resumes
    }
  }
}
