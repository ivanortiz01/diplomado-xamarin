using Android.App;
using Android.Widget;
using Android.OS;

namespace Lab08
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/Icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Validate();

            //var ViewGroup = (Android.Views.ViewGroup)Window.DecorView.FindViewById(Android.Resource.Id.Content);

            //var MainLayout = ViewGroup.GetChildAt(0) as LinearLayout;

            //var HeaderImage = new ImageView(this);
            //HeaderImage.SetImageResource(Resource.Drawable.Xamarin_Diplomado_30);
            //MainLayout.AddView(HeaderImage);

            //var UserNameTextView = new TextView(this);
            //UserNameTextView.Text = GetString(Resource.String.UserName);
            //MainLayout.AddView(UserNameTextView);
        }

        private async void Validate()
        {
            SALLab08.ServiceClient ServiceClient = new SALLab08.ServiceClient();

            string StudentEmail = "ivan.ortiz01@gmail.com";
            string Password = "25155033";
            string MyDevice = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);

            SALLab08.ResultInfo Result = await ServiceClient.ValidateAsync(StudentEmail, Password, MyDevice);
            
            var UserNameResult = FindViewById<TextView>(Resource.Id.UserNameResult);
            UserNameResult.Text = Result.Fullname;

            var StatusResult = FindViewById<TextView>(Resource.Id.StatusResult);
            StatusResult.Text = Result.Status.ToString();

            var TokenResult = FindViewById<TextView>(Resource.Id.TokenResult);
            TokenResult.Text = Result.Token;
        }
    }
}

