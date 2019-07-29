using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using SearchView = Android.Widget.SearchView;

namespace FundooWalkin
{
    /// <summary>
    /// Dashboard showing the candidates selected, to be determined and rejected 
    /// </summary>
    [Activity(Label = "DashboardActivity")]
    public class DashboardActivity : AppCompatActivity
    {
        private DateTime currentDate;
        private Button _dateSelectButton;
        private Spinner spinner;
        private Button SelectedButton;
        private Button TBDButton;
        private Button RejectedButton;
        private TextView TxtViewCurrentDate;
        //private LinearLayout EveryDayLinearLayout;
        private SearchView searchView;
        //private ListView candidateList;
        private LinearLayout statusLayout;
        private TextView TxtCandidateOne;
        private TextView TxtCandidateTwo;
        private TextView TxtTimeOne;
        private TextView TxtTimeOne1;

        /// <summary>
        /// Overriding on create method of activity to custumize the dashboard behaviour
        /// </summary>
        /// <param name="savedInstanceState">saved intent from activity</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            this.currentDate = DateTime.Today;

            //// Set our view from the "DashboardPage" layout resource
            SetContentView(Resource.Layout.DashboardPage);

            ////Set the actions to the date picker
            this._dateSelectButton = FindViewById<Button>(Resource.Id.BtnCalendar);
            this._dateSelectButton.Click += DateSelect_OnClick;
            this._dateSelectButton.Text = currentDate.ToShortDateString();

            ////set the actions to the spinner
            this.spinner = FindViewById<Spinner>(Resource.Id.spinnerLocation);
            this.spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.LocationArray, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            this.spinner.Adapter = adapter;

            ////set the actions to the selected button
            this.SelectedButton = FindViewById<Button>(Resource.Id.BtnSelected);
            this.SelectedButton.Click += SelectedButton_OnClick;
            //SelectedButton.Text = "25&#10;" + Resources.GetString(Resource.String.SelectedCandidate);

            ////set the actions to the To be determined button
            this.TBDButton = FindViewById<Button>(Resource.Id.BtnTBD);
            this.TBDButton.Click += TBDButton_OnClick;
            //TBDButton.Text = "02&#10;" + Resources.GetString(Resource.String.TBDCandidate);

            ////set the actions to the rejected button
            this.RejectedButton = FindViewById<Button>(Resource.Id.BtnRejected);
            this.RejectedButton.Click += RejectedButton_OnClick;
            //RejectedButton.Text = "0&#10;" + Resources.GetString(Resource.String.RejectedCandidate);

            //EveryDayLinearLayout = FindViewById<LinearLayout>(Resource.Id)

            ////set the actions to the current date textview
            this.TxtViewCurrentDate = FindViewById<TextView>(Resource.Id.TxtCurrentDate);
            this.TxtViewCurrentDate.Text = this.currentDate.ToLongDateString();

            ////set the actions to the search view
            this.searchView = FindViewById<SearchView>(Resource.Id.CandidateSearchView);
            this.searchView.SetVerticalGravity(GravityFlags.Bottom);

            ////set the candidate status in the list view
            /*candidateList = FindViewById<ListView>(Resource.Id.CandidateStatus);
            string[] candidates = new string[]{ "abc", "def", "ghi" };
            candidateList.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, candidates);*/

            ////set the status layout
            this.statusLayout = FindViewById<LinearLayout>(Resource.Id.EveryDayLayout);

            ////set the candidate status 
            this.TxtCandidateOne = FindViewById<TextView>(Resource.Id.TxtCandidate1);
            this.TxtCandidateOne.Text = "Aniket Chile selected";

            ////set the candidate status
            this.TxtCandidateTwo = FindViewById<TextView>(Resource.Id.TxtCandidate2);
            this.TxtCandidateTwo.Text = "Dipu Pillai rejected";

            ////set the time when the candidate status is displayed
            this.TxtTimeOne = FindViewById<TextView>(Resource.Id.TxtTime);
            this.TxtTimeOne.Text = "1:20 PM";

            ////set the time when the candidate status is displayed
            this.TxtTimeOne1 = FindViewById<TextView>(Resource.Id.TxtTime1);
            this.TxtTimeOne1.Text = "1:25 PM";

        }

        /// <summary>
        /// handles the event of selected candidate clicked
        /// </summary>
        /// <param name="sender">selected button</param>
        /// <param name="e">event arguments</param>
        private void RejectedButton_OnClick(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Reject button Clicked", ToastLength.Short).Show();
        }

        /// <summary>
        /// handles the event of to be determined candidate clicked
        /// </summary>
        /// <param name="sender">to be determined button</param>
        /// <param name="e">event arguments</param>
        private void TBDButton_OnClick(object sender, EventArgs e)
        {
            Toast.MakeText(this, "TBD button Clicked", ToastLength.Short).Show();
        }

        /// <summary>
        /// handles the event of rejected candidate clicked
        /// </summary>
        /// <param name="sender">rejected button</param>
        /// <param name="e">event arguments</param>
        private void SelectedButton_OnClick(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Select button Clicked", ToastLength.Short).Show();
            StartActivity(typeof(CandidateRemarkActivity));
        }

        /// <summary>
        /// handles the event of date selection from the date picker
        /// </summary>
        /// <param name="sender">date picker</param>
        /// <param name="e">event arguments</param>
        private void DateSelect_OnClick(object sender, EventArgs e)
        {
            ////create instance of date picker fragment and pass the selected date to the front end
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime date)
            {
                _dateSelectButton.Text = date.ToShortDateString();
            });

            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        /// <summary>
        /// handling the event of spinner item selected
        /// </summary>
        /// <param name="sender">sender spinner</param>
        /// <param name="e">item selected event arguments</param>
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            ////set the selected item to the spinner prompt
            spinner.Prompt = spinner.GetItemAtPosition(e.Position).ToString();
        }

        /// <summary>
        /// class to set the calendar dialog to select the date 
        /// </summary>
        private class DatePickerFragment : DialogFragment, DatePickerDialog.IOnDateSetListener
        {
            //// Initialize this value to prevent NullReferenceExceptions.  
            Action<DateTime> _dateSelectedHandler = delegate { };

            public static readonly string TAG = "X:" + typeof(DatePickerFragment).Name.ToUpper();

            /// <summary>
            /// set the date selection handler
            /// </summary>
            /// <param name="onDateSelected">date selected</param>
            /// <returns>returns fragment</returns>
            public static DatePickerFragment NewInstance(Action<DateTime> onDateSelected)
            {
                DatePickerFragment frag = new DatePickerFragment();
                frag._dateSelectedHandler = onDateSelected;
                return frag;
            }

            /// <summary>
            /// on create dialog to choose the date
            /// </summary>
            /// <param name="savedInstanceState">bundle from previous activity</param>
            /// <returns>date dialog</returns>
            public override Dialog OnCreateDialog(Bundle savedInstanceState)
            {
                ////get current date
                DateTime currently = DateTime.Today;

                ////pass to open the dialog on current date
                //// Note: monthOfYear is a value between 0 and 11, not 1 and 12! 
                DatePickerDialog dialog = new DatePickerDialog(Activity, this, currently.Year, currently.Month - 1, currently.Day);
                return dialog;
            }

            /// <summary>
            /// set the date to the picker
            /// </summary>
            /// <param name="view">the calendar</param>
            /// <param name="year">year of the selected date</param>
            /// <param name="monthOfYear">month of selected date</param>
            /// <param name="dayOfMonth">day of selected date</param>
            public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
            {
                //// Note: monthOfYear is a value between 0 and 11, not 1 and 12!  
                DateTime selectedDate = new DateTime(year, monthOfYear + 1, dayOfMonth);
                _dateSelectedHandler(selectedDate);
            }
        }
    }
}