using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace FundooWalkin
{
    [Activity(Label = "CandidateDetails")]
    public class CandidateDetails : AppCompatActivity
    {
        TextView nameText;
        TextView dateText;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.CandidateDetails);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //SupportActionBar.SetDisplayShowCustomEnabled(true);
            SupportActionBar.Title = "CANDIDATE DETAILS";
            //SupportActionBar.NavigationMode { SetContentView(Resource.Layout.SelectedPage) };
            // nameText=FindViewById<TextView>(Resource.Id)

            nameText = FindViewById<TextView>(Resource.Id.nameText);
            nameText.Text = "Poonam Yadav";
            dateText = FindViewById<TextView>(Resource.Id.dateText);
            dateText.Text = "22 March 19";

        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    OnBackPressed();
                    Finish();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}