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
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace FundooWalkin.Activities
{
    [Activity(Label = "CandidateRemarkActivity")]
    public class CandidateRemarkActivity : AppCompatActivity
    {
        private Spinner spinnerAttitude;
        private Spinner spinnerCommunication;
        private Spinner spinnerKnowledge;
        private TextView txtCandidateEmail;
        private TextView txtCandidateLocation;
        private TextView txtCandidateStatus;


        private Button remarkCancelButton;
        private Button remarkSaveButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RemarkCandidatePage);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            ColorDrawable colorDrawable = new ColorDrawable(Color.ParseColor("#FF8C00"));
            SupportActionBar.SetBackgroundDrawable(colorDrawable);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);


            SupportActionBar.Title = "Poonam Yadav";

            ////set the actions to the spinner
            this.spinnerAttitude = FindViewById<Spinner>(Resource.Id.SpinnerAttitude);
            this.spinnerAttitude.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinnerAttitude_ItemSelected);
            var adapterAttitude = ArrayAdapter.CreateFromResource(this, Resource.Array.RemarkArray, Android.Resource.Layout.SimpleSpinnerItem);
            adapterAttitude.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            this.spinnerAttitude.Adapter = adapterAttitude;

            ////set the actions to the spinner
            this.spinnerCommunication = FindViewById<Spinner>(Resource.Id.SpinnerCommunication);
            this.spinnerCommunication.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinnerCommunication_ItemSelected);
            var adapterCommunication = ArrayAdapter.CreateFromResource(this, Resource.Array.RemarkArray, Android.Resource.Layout.SimpleSpinnerItem);
            adapterCommunication.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            this.spinnerCommunication.Adapter = adapterCommunication;

            ////set the actions to the spinner
            this.spinnerKnowledge = FindViewById<Spinner>(Resource.Id.SpinnerKnowledge);
            this.spinnerKnowledge.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinnerKnowledge_ItemSelected);
            var adapterKnowledge = ArrayAdapter.CreateFromResource(this, Resource.Array.RemarkArray, Android.Resource.Layout.SimpleSpinnerItem);
            adapterKnowledge.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            this.spinnerKnowledge.Adapter = adapterKnowledge;

            ////set the text of text view
            this.txtCandidateEmail = FindViewById<TextView>(Resource.Id.TxtCandidateEmail);
            this.txtCandidateEmail.Text = "poonamyadav@gmail.com";

            ////set the text of text view
            this.txtCandidateLocation = FindViewById<TextView>(Resource.Id.TxtCandidateLocation);
            this.txtCandidateLocation.Text = "mumbai";

            ////set the text of text view
            this.txtCandidateStatus = FindViewById<TextView>(Resource.Id.TxtCandidateStatus);
            this.txtCandidateStatus.Text = "online";



            ////set the actions to the cancel button
            this.remarkCancelButton = FindViewById<Button>(Resource.Id.BtnCancel);
            this.remarkCancelButton.Click += remarkCancelButton_Clicked;

            ////set the actions to the save butoon
            this.remarkSaveButton = FindViewById<Button>(Resource.Id.BtnSave);
            this.remarkSaveButton.Click += remarkSaveButton_Clicked;
        }

        private void remarkSaveButton_Clicked(object sender, EventArgs e)
        {
            StartActivity(typeof(DashboardActivity));
        }

        private void remarkCancelButton_Clicked(object sender, EventArgs e)
        {
            StartActivity(typeof(DashboardActivity));


        }

        private void spinnerKnowledge_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            ////set the selected item to the spinner prompt
            spinner.Prompt = spinner.GetItemAtPosition(e.Position).ToString();
        }

        private void spinnerCommunication_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            ////set the selected item to the spinner prompt
            spinner.Prompt = spinner.GetItemAtPosition(e.Position).ToString();
        }

        private void spinnerAttitude_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            ////set the selected item to the spinner prompt
            spinner.Prompt = spinner.GetItemAtPosition(e.Position).ToString();
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