using Android.App;
using Android.Widget;
using Android.OS;

namespace Lab14
{
    [Activity(Label = "Lab14", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            var EmailText = FindViewById<EditText>(Resource.Id.editTextEmail);
            var PasswordText = FindViewById<EditText>(Resource.Id.editTextPassword);
            var ValidateButton = FindViewById<Button>(Resource.Id.buttonValidate);
            ValidateButton.Click += (s, e) =>
            {
                var emailToValidate = EmailText.Text;
                var passwordToValidate = PasswordText.Text;
                if (!string.IsNullOrWhiteSpace(emailToValidate) && !string.IsNullOrWhiteSpace(passwordToValidate))
                {
                    Validate(emailToValidate, passwordToValidate);
                }
                else
                {
                    ShowMessage(Resources.GetString(Resource.String.ErrorValidarActividadTitulo), Resources.GetString(Resource.String.ErrorValidarActividadMensaje));
                }
            };
        }

        private async void Validate(string StudentEmail, string Password)
        {
            SALLab14.ServiceClient ServiceClient = new SALLab14.ServiceClient();

            string MyDevice = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);

            SALLab14.ResultInfo Result = await ServiceClient.ValidateAsync(this);

            string resultStr = $"{Result.Status}\n{Result.FullName}\n{Result.Token}";

            ShowMessage(Resources.GetString(Resource.String.ResutadoValidacion), resultStr);
        }

        private void ShowMessage(string title, string message)
        {
            Android.App.AlertDialog.Builder Builder = new AlertDialog.Builder(this);
            AlertDialog Alert = Builder.Create();
            Alert.SetTitle(title);
            Alert.SetIcon(Resource.Drawable.Icon);
            Alert.SetMessage(message);
            Alert.SetButton("OK", (s, ev) => { });
            Alert.Show();
        }
    }
}

