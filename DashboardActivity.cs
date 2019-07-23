using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FundooWalkin
{
    /// <summary>
    /// Dashboard showing the candidates selected, to be determined and rejected 
    /// </summary>
    [Activity(Label = "DashboardActivity")]
    public class DashboardActivity : Activity
    {
        private DateTime currentDate;
        private Button _dateSelectButton;
        private Spinner spinner;
        private Button SelectedButton;
        private Button TBDButton;
        private Button RejectedButton;
        private TextView TxtViewCurrentDate;
        private LinearLayout EveryDayLinearLayout;

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
            _dateSelectButton = FindViewById<Button>(Resource.Id.BtnCalendar);
            _dateSelectButton.Click += DateSelect_OnClick;
            _dateSelectButton.Text = currentDate.ToShortDateString();

            ////set the actions to the spinner
            spinner = FindViewById<Spinner>(Resource.Id.spinnerLocation);
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.LocationArray, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            ////set the actions to the selected button
            SelectedButton = FindViewById<Button>(Resource.Id.BtnSelected);
            SelectedButton.Click += SelectedButton_OnClick;
            //SelectedButton.Text = "25&#10;" + Resources.GetString(Resource.String.SelectedCandidate);

            ////set the actions to the To be determined button
            TBDButton = FindViewById<Button>(Resource.Id.BtnTBD);
            TBDButton.Click += TBDButton_OnClick;
            //TBDButton.Text = "02&#10;" + Resources.GetString(Resource.String.TBDCandidate);

            ////set the actions to the rejected button
            RejectedButton = FindViewById<Button>(Resource.Id.BtnRejected);
            RejectedButton.Click += RejectedButton_OnClick;
            //RejectedButton.Text = "0&#10;" + Resources.GetString(Resource.String.RejectedCandidate);

            //EveryDayLinearLayout = FindViewById<LinearLayout>(Resource.Id)

            ////set the actions to the current date textview
            TxtViewCurrentDate = FindViewById<TextView>(Resource.Id.TxtCurrentDate);
            TxtViewCurrentDate.Text = currentDate.ToLongDateString();
        }

        /// <summary>
        /// handles the event of selected candidate clicked
        /// </summary>
        /// <param name="sender">selected button</param>
        /// <param name="e">event arguments</param>
        private void RejectedButton_OnClick(object sender, EventArgs e)
        {
            //StartActivity(typeof(SelectedActivity));
        }

        /// <summary>
        /// handles the event of to be determined candidate clicked
        /// </summary>
        /// <param name="sender">to be determined button</param>
        /// <param name="e">event arguments</param>
        private void TBDButton_OnClick(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// handles the event of rejected candidate clicked
        /// </summary>
        /// <param name="sender">rejected button</param>
        /// <param name="e">event arguments</param>
        private void SelectedButton_OnClick(object sender, EventArgs e)
        {
            
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