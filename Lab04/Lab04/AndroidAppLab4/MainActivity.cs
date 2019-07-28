using Android.App;
using Android.Widget;
using Android.OS;

namespace AndroidAppLab4
{
    [Activity(Label = "AndroidAppLab4", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var Validator = new PCLProyect.AppValidator(new AndroidDialog(this));
            Validator.Email = "ivan.ortiz01@gmail.com";
            Validator.Password = "25155033";
            Validator.Device = Android.Provider.Settings.Secure.GetString(
                ContentResolver,
                Android.Provider.Settings.Secure.AndroidId);

            Validator.Validate();

            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
        }
    }
}

