using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Views;
using System;
using Android.Graphics;

namespace Lab12
{
    [Activity(Label = "Lab12", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            Validate();

            var ListColors = FindViewById<ListView>(Resource.Id.listView1);
            ListColors.Adapter = new CustomAdapters.ColorAdapter(
                this, Resource.Layout.ListItem, Resource.Id.textView1, Resource.Id.textView2,
                Resource.Id.imageView1);

            //var MyLayout = new MyViewGroup(this);
            //SetContentView(MyLayout);
        }

        private async void Validate()
        {
            SALLab12.ServiceClient ServiceClient = new SALLab12.ServiceClient();

            string StudentEmail = "ivan.ortiz01@gmail.com";
            string Password = "25155033";
            string MyDevice = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);

            SALLab12.ResultInfo Result = await ServiceClient.ValidateAsync(StudentEmail, Password, MyDevice);

            string resultStr = $"{Result.Status}\n{Result.FullName}\n{Result.Token}";

            var TextViewResult = FindViewById<TextView>(Resource.Id.ResultText);
            TextViewResult.Text = resultStr;            
        }
    }

    class MyViewGroup :ViewGroup
    {
        Context ViewGroupContext;

        public MyViewGroup(Context context) : base(context)
        {
            this.ViewGroupContext = context;
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            this.SetBackgroundColor(Color.Fuchsia);

            var MyView = new View(ViewGroupContext);
            MyView.SetBackgroundColor(Color.Blue);
            MyView.Layout(20, 20, 150, 150);
            AddView(MyView);
        }
    }
}

