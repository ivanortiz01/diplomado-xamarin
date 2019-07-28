using Android.App;
using Android.Widget;
using Android.OS;

namespace PhoneApp
{
    [Activity(Label = "Phone App", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Validate();

            // Set our view from the "main" layout resource
             SetContentView (Resource.Layout.Main);

            var PhoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
            var TranslateButton = FindViewById<Button>(Resource.Id.TranslateButton);
            var CallButton = FindViewById<Button>(Resource.Id.CallButton);

            CallButton.Enabled = false;

            var TranslateNumber = string.Empty;

            TranslateButton.Click += (object sender, System.EventArgs e) =>
            {
                var Translator = new PhoneTranslator();
                TranslateNumber = Translator.ToNumber(PhoneNumberText.Text);
                if (string.IsNullOrWhiteSpace(TranslateNumber))
                {
                    CallButton.Text = "Llamar";
                    CallButton.Enabled = false;
                }
                else
                {
                    CallButton.Text = $"Llamar al {TranslateNumber}";
                    CallButton.Enabled = true;
                }
            };

            CallButton.Click += (object sender, System.EventArgs e) =>
            {
                var CallDialog = new AlertDialog.Builder(this);
                CallDialog.SetMessage($"Llamar al número {TranslateNumber}?");
                CallDialog.SetNeutralButton("Llamar", delegate
                {
                    var CallIntent = new Android.Content.Intent(Android.Content.Intent.ActionCall);
                    CallIntent.SetData(Android.Net.Uri.Parse($"tel:{TranslateNumber}"));
                    StartActivity(CallIntent);
                });
                CallDialog.SetNegativeButton("Cancelar", delegate { });
                CallDialog.Show();
            };
        }

        private async void Validate()
        {
            SALLab05.ServiceClient ServiceClient = new SALLab05.ServiceClient();

            string StudentEmail = "ivan.ortiz01@gmail.com";
            string Password = "25155033";
            string MyDevice = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);

            SALLab05.ResultInfo Result = await ServiceClient.ValidateAsync(StudentEmail, Password, MyDevice);

            string resultStr = $"{Result.Status}\n{Result.Fullname}\n{Result.Token}";

            var TextViewResult = FindViewById<TextView>(Resource.Id.TextViewResult);
            TextViewResult.Text = resultStr;
        }
    }
}

