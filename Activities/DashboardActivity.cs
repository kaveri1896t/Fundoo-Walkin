using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FundooWalkin.Model;
using SearchView = Android.Support.V7.Widget.SearchView;
using FundooWalkin.helper;
using Android.Support.V4.View;
using Newtonsoft.Json;

namespace FundooWalkin.Activities
{
    /// <summary>
    /// Dashboard showing the candidates selected, to be determined and rejected 
    /// </summary>
    [Activity(Label = "DashboardActivity", Theme = "@style/NoActionBarTheme")]
    public class DashboardActivity : AppCompatActivity
    {
        public List<Candidate> candidates;
        private DateTime currentDate;
        private Button _dateSelectButton;
        private Spinner spinner;
        private Button SelectedButton;
        private Button TBDButton;
        private Button RejectedButton;
        private TextView TxtViewCurrentDate;
        private SearchView searchView;
        private LinearLayout statusLayout;
        private TextView TxtCandidateOne;
        private TextView TxtCandidateTwo;
        private TextView TxtTimeOne;
        private TextView TxtTimeOne1;
        private RecyclerView recycler;
        private RecyclerViewAdapter _adapter;
        //private RecyclerView.LayoutManager _LayoutManager;

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
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            ////Set the actions to the date picker
            this._dateSelectButton = FindViewById<Button>(Resource.Id.BtnCalendar);
            this._dateSelectButton.Click += DateSelect_OnClick;
            this._dateSelectButton.Text = currentDate.ToShortDateString();

            ////set the actions to the spinner
            this.spinner = FindViewById<Spinner>(Resource.Id.spinnerLocation);
            this.spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.LocationArray, Resource.Layout.Spinner_Item);
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

            ////set the actions to the current date textview
            this.TxtViewCurrentDate = FindViewById<TextView>(Resource.Id.TxtCurrentDate);
            this.TxtViewCurrentDate.Text = this.currentDate.ToLongDateString();

            ////set the actions to the search view
            this.searchView = FindViewById<SearchView>(Resource.Id.CandidateSearchView);
            this.searchView.SetIconifiedByDefault(true);
            //this.searchView.SetQuery("Search Candidate", false);

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

            ////set the candidate recycler view 
            this.recycler = FindViewById<RecyclerView>(Resource.Id.dashboardRecyclerView);
            this.candidates = new List<Candidate>
           {
               new Candidate {Name = "Poonam Yadav",Email="Poonamyadav@bridgelabz.com",Location="Mumbai",Date=""},
               new Candidate {Name = "Riya Patil", Email="Poonamyadav@bridgelabz.com",Location="Mumbai",Date=""},
               new Candidate {Name = "Teena Agrawal",Email="Poonamyadav@bridgelabz.com",Location="Mumbai",Date=""},
               new Candidate {Name = "Heena Chopra", Email="Poonamyadav@bridgelabz.com",Location="Mumbai",Date=""},
               new Candidate {Name = "Kanchan Mehta", Email="Poonamyadav@bridgelabz.com",Location="Mumbai",Date=""},
               new Candidate {Name = "Riya Patil", Email="Poonamyadav@bridgelabz.com",Location="Mumbai",Date=""},
               new Candidate {Name = "Minesh Agrawal",Email="Poonamyadav@bridgelabz.com",Location="Mumbai",Date=""},
               new Candidate {Name = "parmeshwar Raut", Email="Poonamyadav@bridgelabz.com",Location="Mumbai",Date=""},
               new Candidate {Name = "Mahesh Mehta", Email="Poonamyadav@bridgelabz.com",Location="Mumbai",Date=""}
            };

            ////add adapter to the recycler view
            this._adapter = new RecyclerViewAdapter(this, this.candidates);
            this._adapter.ItemClick += OnItemClick;
            LinearLayoutManager _LayoutManager = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
            this.recycler.SetLayoutManager(_LayoutManager);
            this.recycler.SetAdapter(this._adapter);
        }

        /// <summary>
        /// handling the event of item click from the recycler view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemClick(object sender, int e)
        {
            // RecyclerViewAdapter adapter = new RecyclerViewAdapter(this, candidates);
            List<Candidate> item = candidates.OrderBy(s => s.Name).ToList();
            var candidate = item[e];
            Intent intent = new Intent(this, typeof(CandidateDetails));
            intent.PutExtra("Candidate", JsonConvert.SerializeObject(candidate));
            this.StartActivity(intent);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            //return base.OnCreateOptionsMenu(menu);
           //MenuInflater.Inflate(Resource.Menu.main, menu);
            var item = menu.FindItem(Resource.Id.search_go_btn);
            var searchview1 = MenuItemCompat.GetActionView(item);
            this.searchView = searchview1.JavaCast<SearchView>();

            this.searchView.QueryTextChange += (s, e) => _adapter.Filter.InvokeFilter(e.NewText);
            this.searchView.QueryTextSubmit += (s, e) =>
            {
                Toast.MakeText(this, "Search for :", ToastLength.Short).Show();
                e.Handled = true;
            };

            MenuItemCompat.SetOnActionExpandListener(item, new SearchViewExpandListener(this._adapter));
            return true;
        }

        public class SearchViewExpandListener : Java.Lang.Object, MenuItemCompat.IOnActionExpandListener
        {
            private readonly IFilterable _adapter;

            public SearchViewExpandListener(IFilterable adapter)
            {
                _adapter = adapter;
            }

            public bool OnMenuItemActionCollapse(IMenuItem item)
            {
                _adapter.Filter.InvokeFilter("");
                return true;
            }

            public bool OnMenuItemActionExpand(IMenuItem item)
            {
                return true;
            }
        }

        /// <summary>
        /// handles the event of selected candidate clicked
        /// </summary>
        /// <param name="sender">selected button</param>
        /// <param name="e">event arguments</param>
        private void RejectedButton_OnClick(object sender, EventArgs e)
        {
            StartActivity(typeof(RejectedActivity));
        }

        /// <summary>
        /// handles the event of to be determined candidate clicked
        /// </summary>
        /// <param name="sender">to be determined button</param>
        /// <param name="e">event arguments</param>
        private void TBDButton_OnClick(object sender, EventArgs e)
        {
            StartActivity(typeof(TBDActivity));
        }

        /// <summary>
        /// handles the event of rejected candidate clicked
        /// </summary>
        /// <param name="sender">rejected button</param>
        /// <param name="e">event arguments</param>
        private void SelectedButton_OnClick(object sender, EventArgs e)
        {
            StartActivity(typeof(SelectedActivity));
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
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}