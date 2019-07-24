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
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Text;
using Android.Views;
using Android.Widget;
using EditText = Android.Widget.EditText;
using SearchView = Android.Support.V7.Widget.SearchView;

namespace FundooWalkin
{
    [Activity(Label = "SelectedActivity")]
    public class SelectedActivity : AppCompatActivity
    {
        EditText inputSearch;
        TextView textView;
        private SearchView _searchView;
        private ListView _listView;
        private ArrayAdapter _adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SelectedPage);
            Button btn = FindViewById<Button>(Resource.Id.btn1);
            btn.Click += delegate { StartActivity(typeof(CandidateDetails)); };
            //Toolbar toolbar = (Toolbar)FindViewById(Resource.Id.selectedtoolbar);
            // SetSupportActionBar(toolbar);
            //   SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //toolbar.SetNavigationOnClickListener(arrow->onBackPressed());
            //  toolbar.SetNavigationOnClickListener(View.IOnClickListener)
            // toolbar.SetNavigationOnClickListener();
            textView = FindViewById<TextView>(Resource.Id.text);
            /*  getSupportActionBar().setDisplayHomeAsUpEnabled(true);
              getSupportActionBar().setTitle("Home");
              getSupportActionBar().setSubtitle("sairam");*/
           // SupportActionBar.NavigationMode{SetContentView(Resource.Layout.)}
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = "Selected";
            
          //  ColorDrawable colorDrawable = new ColorDrawable(Color.White);
            //ActionBar.SetBackgroundDrawable(colorDrawable);
          //  SupportActionBar.SetBackgroundDrawable(colorDrawable);


            inputSearch = FindViewById<EditText>(Resource.Id.inputSearch);
            
            inputSearch.TextChanged += InputSearch_TextChanged;

            var products = new[]

            {
                "one","two","three","four"
            };

            _listView = FindViewById<ListView>(Resource.Id.listview);
            //_searchView = FindViewById<SearchView>(Resource.Id.searchView);
            _adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, products);
            _listView.Adapter = _adapter;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            //return base.OnCreateOptionsMenu(menu);
            MenuInflater.Inflate(Resource.Menu.main, menu);
            var item = menu.FindItem(Resource.Id.action_search);
            var searchview = MenuItemCompat.GetActionView(item);
            _searchView = searchview.JavaCast<Android.Support.V7.Widget.SearchView>();

            _searchView.QueryTextChange += (s, e) => _adapter.Filter.InvokeFilter(e.NewText);
            _searchView.QueryTextSubmit += (s, e) =>
             {
                 Toast.MakeText(this, "Search for :", ToastLength.Short).Show();
                 e.Handled = true;
             };

            MenuItemCompat.SetOnActionExpandListener(item, new SearchViewExpandListener(_adapter));
            return true;
        }

        public class SearchViewExpandListener
            : Java.Lang.Object, MenuItemCompat.IOnActionExpandListener
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
        private void InputSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

            Toast.MakeText(this, "searched", ToastLength.Short).Show();
            /* //get the text from Edit Text            
             var searchText = inputSearch.Text;

             //Compare the entered text with List  
             List<User> list = (from items in UserData.Users
                                where items.Name.Contains(searchText) ||
                                               items.Department.Contains(searchText) ||
                                               items.Details.Contains(searchText)
                                select items).ToList<User>();

             // bind the result with adapter  
             myList.Adapter = new MyCustomListAdapter(list);*/
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