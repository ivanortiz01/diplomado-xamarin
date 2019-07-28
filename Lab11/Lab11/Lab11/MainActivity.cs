using Android.App;
using Android.Widget;
using Android.OS;

namespace Lab11
{
    [Activity(Label = "Lab11", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Complex Data;
        int Counter = 0;
        bool hadValidate = false;
        string resultStr;

        protected override void OnCreate(Bundle bundle)
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnCreate");

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            FindViewById<Button>(Resource.Id.StartActivity).Click += (s, e) =>
            {
                var ActivityIntent = new Android.Content.Intent(this, typeof(SecondActivity));
                StartActivity(ActivityIntent);
            };

            Data = (Complex)this.FragmentManager.FindFragmentByTag("Data");
            if (Data == null)
            {
                Data = new Complex();
                var FragmentTransaction = this.FragmentManager.BeginTransaction();
                FragmentTransaction.Add(Data, "Data");
                FragmentTransaction.Commit();
            }

            if (bundle != null)
            {
                Counter = bundle.GetInt("CounterValue", 0);
                Android.Util.Log.Debug("Lab11Log", "Activity A - Recovered Instance State");

                hadValidate = bundle.GetBoolean("hadValidate");

                var TextViewResult = FindViewById<TextView>(Resource.Id.ResultTeext);
                TextViewResult.Text = bundle.GetString("resultStr"); 
            }

            if (!hadValidate)
            {
                Validate();
            }

            var ClickCounter = FindViewById<Button>(Resource.Id.ClicksCounter);
            ClickCounter.Text = Resources.GetString(Resource.String.ClickCounter_Text, Counter);
            ClickCounter.Text += $"\n{Data.ToString()}";
            ClickCounter.Click += (s, e) =>
            {
                Counter++;
                ClickCounter.Text = Resources.GetString(Resource.String.ClickCounter_Text, Counter);

                Data.Real++;
                Data.Imaginary++;
                ClickCounter.Text += $"\n{Data.ToString()}";
            };
        }

        protected override void OnStart()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnStart");
            base.OnStart();
        }

        protected override void OnResume()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnResume");
            base.OnResume();
        }

        protected override void OnPause()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnPause");
            base.OnPause();
        }

        protected override void OnStop()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnStop");
            base.OnStop();
        }

        protected override void OnDestroy()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnDestroy");
            base.OnDestroy();
        }

        protected override void OnRestart()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnRestart");
            base.OnRestart();
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("CounterValue", Counter);
            outState.PutBoolean("hadValidate", hadValidate);
            outState.PutString("resultStr", resultStr);
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnSaveInstanceState");
            base.OnSaveInstanceState(outState);
        }

        private async void Validate()
        {
            SALLab11.ServiceClient ServiceClient = new SALLab11.ServiceClient();

            string StudentEmail = "ivan.ortiz01@gmail.com";
            string Password = "25155033";
            string MyDevice = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);

            SALLab11.ResultInfo Result = await ServiceClient.ValidateAsync(StudentEmail, Password, MyDevice);

            resultStr = $"{Result.Status}\n{Result.Fullname}\n{Result.Token}";

            var TextViewResult = FindViewById<TextView>(Resource.Id.ResultTeext);
            TextViewResult.Text = resultStr;

            if ("SUCCESS".Equals(Result.Status.ToString().ToUpper()))
            {
                hadValidate = true;
            }
        }
    }
}

