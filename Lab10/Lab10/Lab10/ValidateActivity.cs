using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Lab10
{
    [Activity(Label = "@string/ApplicationName")]
    public class ValidateActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Validate);

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
            SALLab10.ServiceClient ServiceClient = new SALLab10.ServiceClient();

            string MyDevice = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);

            SALLab10.ResultInfo Result = await ServiceClient.ValidateAsync(StudentEmail, Password, MyDevice);

            string resultStr = $"{Result.Status}\n{Result.Fullname}\n{Result.Token}";

            var TextViewResult = FindViewById<TextView>(Resource.Id.ResultText);
            TextViewResult.Text = resultStr;
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
    }
}