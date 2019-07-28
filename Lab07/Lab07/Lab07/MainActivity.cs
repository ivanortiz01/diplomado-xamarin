using Android.App;
using Android.Widget;
using Android.OS;

namespace Lab07
{
    [Activity(Label = "Lab07", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            var EmailText = FindViewById<EditText>(Resource.Id.EmailText);
            var PasswordText = FindViewById<EditText>(Resource.Id.PasswordText);
            var ValidateButton = FindViewById<Button>(Resource.Id.ValidateButton);

            ValidateButton.Click += (sender, e) =>
            {
                var emailToValidate = EmailText.Text;
                var passwordToValidate = PasswordText.Text;

                if (!string.IsNullOrWhiteSpace(emailToValidate) && !string.IsNullOrWhiteSpace(passwordToValidate))
                {
                    Validate(emailToValidate, passwordToValidate);
                }
                else
                {
                    ShowErrorMessage();
                }
            };
        }

        private async void Validate(string StudentEmail, string Password)
        {
            SALLab07.ServiceClient ServiceClient = new SALLab07.ServiceClient();

            string MyDevice = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);

            SALLab07.ResultInfo Result = await ServiceClient.ValidateAsync(StudentEmail, Password, MyDevice);

            string resultStr = $"{Result.Status}\n{Result.Fullname}\n{Result.Token}";

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
            {
                showResultAsNotification(resultStr);
            } else
            {
                showResultInTextView(resultStr);
            }
        }

        private void ShowErrorMessage()
        {
            Android.App.AlertDialog.Builder Builder = new AlertDialog.Builder(this);
            AlertDialog Alert = Builder.Create();
            Alert.SetTitle("Error en Validar Actividad");
            Alert.SetIcon(Resource.Drawable.Icon);
            Alert.SetMessage($"Los campos de Correo y Contraseña son obligatorios, no pueden estar vacios.");
            Alert.SetButton("OK", (s, ev) => { });
            Alert.Show();
        }

        private void showResultInTextView(string resultToShow)
        {
            var TextViewResult = FindViewById<TextView>(Resource.Id.TextViewResult);
            TextViewResult.Text = resultToShow;
        }

        private void showResultAsNotification(string resultToShow)
        {
            var Builder = new Notification.Builder(this)
                .SetContentTitle("Validación de Actividad")
                .SetContentText(resultToShow)
                .SetSmallIcon(Resource.Drawable.Icon);

            Builder.SetCategory(Notification.CategoryMessage);

            var ObjectNotification = Builder.Build();
            var Manager = GetSystemService(
                Android.Content.Context.NotificationService) as NotificationManager;
            Manager.Notify(0, ObjectNotification);
        }
    }
}

