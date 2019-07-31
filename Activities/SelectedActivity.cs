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
using Android.Support.V7.Widget;
using Android.Text;
using Android.Views;
using Android.Widget;
using FundooWalkin.helper;
using EditText = Android.Widget.EditText;
using SearchView = Android.Support.V7.Widget.SearchView;

namespace FundooWalkin.Activities

{
    [Activity(Label = "SelectedActivity")]
    public class SelectedActivity : AppCompatActivity
    {
       
        private SearchView _searchView;
      
        private RecyclerViewAdapter _adapter;
        private RecyclerView _recyclerView;
        RecyclerView.LayoutManager _LayoutManager;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SelectedPage);
           
           
          //  SupportActionBar.NavigationMode{ SetContentView(Resource.Layout.LoginPage); };
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = "Selected";
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            ColorDrawable colorDrawable = new ColorDrawable(Color.ParseColor("#FF8C00"));
            SupportActionBar.SetBackgroundDrawable(colorDrawable);

           
            _recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            var products = new List<Candidate>
            {
                new Candidate {Name = "Poonam Yadav",Email="Poonamyadav@bridgelabz.com",Location="Mumbai",Date="22 March 19"},
                new Candidate {Name = "Riya Patil", Email="Poonamyadav@bridgelabz.com",Location="Mumbai",Date="22 March 19"},
                new Candidate {Name = "Teena Agrawal",Email="Poonamyadav@bridgelabz.com",Location="Mumbai",Date="22 March 19"},
                new Candidate {Name = "Heena Chopra", Email="Poonamyadav@bridgelabz.com",Location="Mumbai",Date="22 March 19"},
                new Candidate {Name = "Kanchan Mehta", Email="Poonamyadav@bridgelabz.com",Location="Mumbai",Date="22 March 19"},
                new Candidate {Name = "Riya Patil", Email="Poonamyadav@bridgelabz.com",Location="Mumbai",Date="22 March 19"},
                new Candidate {Name = "Teena Agrawal",Email="Poonamyadav@bridgelabz.com",Location="Mumbai",Date="22 March 19"},
                new Candidate {Name = "Heena Chopra", Email="Poonamyadav@bridgelabz.com",Location="Mumbai",Date="22 March 19"},
                new Candidate {Name = "Kanchan Mehta", Email="Poonamyadav@bridgelabz.com",Location="Mumbai",Date="22 March 19"},

            };
           
            _adapter = new RecyclerViewAdapter(this, products);
            _adapter.ItemClick += OnItemClick;
            _LayoutManager = new LinearLayoutManager(this);
            _recyclerView.SetLayoutManager(_LayoutManager);
            _recyclerView.SetAdapter(_adapter);
            
           // _adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, products);
            //_listView.Adapter = _adapter;
        }

        private void OnItemClick(object sender, int e)
        {
            StartActivity(typeof(CandidateDetails));
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