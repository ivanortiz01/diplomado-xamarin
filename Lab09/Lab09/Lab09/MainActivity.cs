using Android.App;
using Android.Widget;
using Android.OS;

namespace Lab09
{
    [Activity(Label = "Lab09", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            Validate();
        }

        private async void Validate()
        {
            SALLab09.ServiceClient ServiceClient = new SALLab09.ServiceClient();

            string StudentEmail = "ivan.ortiz01@gmail.com";
            string Password = "25155033";
            string MyDevice = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);

            SALLab09.ResultInfo Result = await ServiceClient.ValidateAsync(StudentEmail, Password, MyDevice);

            var UserNameResult = FindViewById<TextView>(Resource.Id.UserNameText);
            UserNameResult.Text = Result.Fullname;

            var StatusResult = FindViewById<TextView>(Resource.Id.StatusText);
            StatusResult.Text = Result.Status.ToString();

            var TokenResult = FindViewById<TextView>(Resource.Id.CodeText);
            TokenResult.Text = Result.Token;
        }
    }
}

