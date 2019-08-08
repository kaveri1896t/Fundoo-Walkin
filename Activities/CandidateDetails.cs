using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Views;
using Newtonsoft.Json;
using Android.Widget;
using FundooWalkin.Model;

namespace FundooWalkin.Activities
{
    [Activity(Label = "CandidateDetails")]
    public class CandidateDetails : AppCompatActivity
    {
        Candidate candidate;
        TextView nameText;
        TextView dateText;
        TextView emailText;
        TextView onlineText;
        TextView locationText;
        TextView attitude;
        TextView communication;
        TextView knowledge;
        RadioButton selected;
        RadioButton TBD;
        RadioButton rejected;
        TextView remarkText;
        Button cancel;
        Button edit;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.CandidateDetails);

            candidate = JsonConvert.DeserializeObject<Candidate>(Intent.GetStringExtra("Candidate"));
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            ColorDrawable colorDrawable = new ColorDrawable(Color.ParseColor("#FF8C00"));
            //ActionBar.SetBackgroundDrawable(colorDrawable);
            SupportActionBar.SetBackgroundDrawable(colorDrawable);

            //SupportActionBar.SetDisplayShowCustomEnabled(true);
            SupportActionBar.Title = "CANDIDATE DETAILS";
            //SupportActionBar.NavigationMode { SetContentView(Resource.Layout.SelectedPage) };
            // nameText=FindViewById<TextView>(Resource.Id)

            nameText = FindViewById<TextView>(Resource.Id.nameText);

            // nameText.Text = "Poonam Yadav";
            nameText.Text = candidate.Name;
            dateText = FindViewById<TextView>(Resource.Id.dateText);
            // dateText.Text = "22 March 19";
            dateText.Text = candidate.Date;

            emailText = FindViewById<TextView>(Resource.Id.emailText);
            // emailText.Text = "Poonamyadav@bridgelabz.com";
            emailText.Text = candidate.Email;
            onlineText = FindViewById<TextView>(Resource.Id.onlineText);
            onlineText.Text = candidate.ReferredBy;
            locationText = FindViewById<TextView>(Resource.Id.locationText);
            locationText.Text = candidate.Location;
            attitude = FindViewById<TextView>(Resource.Id.attitudeText);
            attitude.Text = "OK";
            communication = FindViewById<TextView>(Resource.Id.communicationText);
            communication.Text = "Fine";
            knowledge = FindViewById<TextView>(Resource.Id.knowledgeText);
            knowledge.Text = "Good";
            selected = FindViewById<RadioButton>(Resource.Id.rb_selected);

            //selected.Click += selectedClicked;
            TBD = FindViewById<RadioButton>(Resource.Id.rb_tbd);

            // TBD.Click += Tbdclicked;
            rejected = FindViewById<RadioButton>(Resource.Id.rb_rejected);
            // rejected.Click += rejectedClicked;

            if (selected.Checked = true)
            {
                TBD.Checked = false;
                rejected.Checked = false;
            }

            remarkText = FindViewById<TextView>(Resource.Id.remarkText);
            remarkText.Text = "remark will be displayed here...";
            cancel = FindViewById<Button>(Resource.Id.cancelBtn);
            cancel.Click += CancelClicked;
            edit = FindViewById<Button>(Resource.Id.editBtn);
            edit.Click += EditClicked;
        }

        private void EditClicked(object sender, EventArgs e)
        {
            edit.SetBackgroundColor(Color.Orange);
            StartActivity(typeof(CandidateRemarkActivity));
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            cancel.SetBackgroundColor(Color.Orange);
            StartActivity(typeof(SelectedActivity));
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

        public void onRadioButtonClicked(View v)
        {

        }
    }
}