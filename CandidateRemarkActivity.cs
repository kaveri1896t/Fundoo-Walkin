using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace FundooWalkin
{
    [Activity(Label = "CandidateRemarkActivity")]
    public class CandidateRemarkActivity : AppCompatActivity
    {
        private Spinner spinnerAttitude;
        private Spinner spinnerCommunication;
        private Spinner spinnerKnowledge;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RemarkCandidatePage);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = "Poonam Yadav";
            //spinnerAttitude = FindViewById<Spinner>(Resource.Id.)
            
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