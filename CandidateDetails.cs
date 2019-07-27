using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
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
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //SupportActionBar.SetDisplayShowCustomEnabled(true);
            SupportActionBar.Title = "CANDIDATE DETAILS";
            //SupportActionBar.NavigationMode { SetContentView(Resource.Layout.SelectedPage) };
            // nameText=FindViewById<TextView>(Resource.Id)

            nameText = FindViewById<TextView>(Resource.Id.nameText);
            nameText.Text = "Poonam Yadav";
            dateText = FindViewById<TextView>(Resource.Id.dateText);
            dateText.Text = "22 March 19";

            emailText = FindViewById<TextView>(Resource.Id.emailText);
            emailText.Text = "Poonamyadav@bridgelabz.com";
            onlineText = FindViewById<TextView>(Resource.Id.onlineText);
            onlineText.Text = "Online";
            locationText = FindViewById<TextView>(Resource.Id.locationText);
            locationText.Text = "Mumbai";

            attitude = FindViewById<TextView>(Resource.Id.attitudeText);
            attitude.Text = "OK";
            communication = FindViewById<TextView>(Resource.Id.communicationText);
            communication.Text = "Fine";
            knowledge = FindViewById<TextView>(Resource.Id.knowledgeText);
            knowledge.Text = "Good";

            selected = FindViewById<RadioButton>(Resource.Id.rb_selected);
            selected.Click += selectedClicked;
            TBD = FindViewById<RadioButton>(Resource.Id.rb_tbd);

            TBD.Checked = true;

            TBD.Click += Tbdclicked;
            rejected = FindViewById<RadioButton>(Resource.Id.rb_rejected);
            rejected.Click += rejectedClicked;

            remarkText = FindViewById<TextView>(Resource.Id.remarkText);
            remarkText.Text = "remark will be displayed here...";

            cancel = FindViewById<Button>(Resource.Id.cancelBtn);
            cancel.Click += CancelClicked;
            edit = FindViewById<Button>(Resource.Id.editBtn);
            edit.Click  += EditClicked;
        }

        private void EditClicked(object sender, EventArgs e)
        {
            edit.SetBackgroundColor(Color.Orange);
            StartActivity(typeof(MainActivity));
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            cancel.SetBackgroundColor(Color.Orange);
        }

        private void rejectedClicked(object sender, EventArgs e)
        {
            rejected.SetTextColor(Color.Green);
        }

        private void Tbdclicked(object sender, EventArgs e)
        {
            TBD.SetTextColor(Color.Green);
        }

        private void selectedClicked(object sender, EventArgs e)
        {
            selected.SetTextColor(Color.Green);
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
         /*   RadioGroup rg = (RadioGroup)FindViewById(Resource.Id.rb_selected);
            RadioButton rb_coldfusion = (RadioButton)FindViewById(R.id.rb_coldfusion);
            RadioButton rb_flex = (RadioButton)FindViewById(R.id.rb_flex);

            // Is the current Radio Button checked?
            bool checked = ((RadioButton)v).isChecked();

            switch (v.getId())
            {
                case R.id.rb_coldfusion:
                    if (checked)
                        rb_coldfusion.setTextColor(Color.RED);
                    rb_flex.setTextColor(Color.GRAY);
                    break;

                case R.id.rb_flex:
                    if (checked)
                        rb_flex.setTextColor(Color.RED);
                    rb_coldfusion.setTextColor(Color.GRAY);
                    break;
            }

            }*/
        }
    }
}