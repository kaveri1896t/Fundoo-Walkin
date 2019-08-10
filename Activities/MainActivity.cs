using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using FundooWalkin.helper;
using Android.Widget;
using System;
using FundooWalkin.helper;
using System.Text.RegularExpressions;
using FundooWalkin.Model;

namespace FundooWalkin.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/NoActionBarThemeForLogin", MainLauncher = true)]

    public class MainActivity : AppCompatActivity
    {
        Helper helper = new Helper();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.LoginPage);
            EditText username = FindViewById<EditText>(Resource.Id.name);
            EditText password = FindViewById<EditText>(Resource.Id.password);
            Button LoginBtn = FindViewById<Button>(Resource.Id.Loginbtn);
            LoginBtn.Click += async (object sender, EventArgs e) =>
            {
                if (username.Text != null && password.Text != null)
                {

                    Admin admin = new Admin();

                    admin.loginId = username.Text;

                    admin.password = password.Text;
                    var httpResponse = await helper.LoginAdmin(admin);

                    if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Toast.MakeText(this, "You are Logged in successfully", ToastLength.Long).Show();
                        StartActivity(typeof(DashboardActivity));
                        username.Text = string.Empty;
                        password.Text = string.Empty;
                    }
                    else
                    {
                        Toast.MakeText(this, "Log in failed", ToastLength.Long).Show();
                        password.Text = string.Empty;
                    }
                }
                else
                {
                    Toast.MakeText(this, "All Fields Must Be Filled", ToastLength.Long).Show();
                }
            };
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
