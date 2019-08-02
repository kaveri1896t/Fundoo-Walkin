using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using FundooWalkin.helper;
using Android.Widget;
using System;

using System.Text.RegularExpressions;


namespace FundooWalkin.Activities

{
    [Activity(Label = "@string/app_name", Theme = "@style/NoActionBarThemeForLogin", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

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
                /* if (username.Text != null && password.Text != null)
                 {
                     if (Regex.IsMatch(username.Text, @"^[a-z][a-z0-9]+" + "@gmail.com"))
                     {

                         User user = new User();

                         user.email = username.Text;
                         user.password = password.Text;
                         var httpResponse = await Helper.LoginUser(user);

                         if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                         {
                             Toast.MakeText(this, "You are Logged in successfully", ToastLength.Long).Show();
                             //
                             // StartActivity(typeof(DashBoardActivity));
                             username.Text = string.Empty;
                             password.Text = string.Empty;
                         }
                         else
                         {
                             Toast.MakeText(this, "Log in failed", ToastLength.Long).Show();
                         }
                     }
                     else
                     {
                         Toast.MakeText(this, "You May Have Entered Invalid Mail", ToastLength.Long).Show();
                     }
                 }
                 else
                 {
                     Toast.MakeText(this, "All Fields Must Be Filled", ToastLength.Long).Show();
                 }*/

                StartActivity(typeof(DashboardActivity));

            };
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        
    }
}
