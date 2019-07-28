using Android.App;
using Android.Widget;
using Android.OS;

namespace Lab13
{
    [Activity(Label = "Lab13", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var ImageView = FindViewById<ImageView>(Resource.Id.imageView1);
            ImageView.Click += (s, e) =>
            {
                Validate();
            };            
        }

        private async void Validate()
        {
            var ServiceClient = new SALLab13.ServiceClient();

            string StudentEmail = "ivan.ortiz01@gmail.com";
            string Password = "25155033";
            
            var Result = await ServiceClient.ValidateAsync(this, StudentEmail, Password);

            Android.App.AlertDialog.Builder Builder = new AlertDialog.Builder(this);
            AlertDialog Alert = Builder.Create();
            Alert.SetTitle("Resultado de la verificación");
            Alert.SetIcon(Resource.Drawable.Icon);
            Alert.SetMessage($"{Result.Status}\n{Result.FullName}\n{Result.Token}");
            Alert.SetButton("OK", (s, ev) => { });
            Alert.Show();
        }
    }
}

