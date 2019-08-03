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
using Android.Views;
using Android.Widget;
using FundooWalkin.helper;
using FundooWalkin.Model;
using Newtonsoft.Json;
using SearchView = Android.Support.V7.Widget.SearchView;

namespace FundooWalkin.Activities
{
    [Activity(Label = "TBDActivity")]
    public class TBDActivity : AppCompatActivity
    {
        List<Candidate> candidates;
        private SearchView _searchView;
        private RecyclerViewAdapter _adapter;
        private RecyclerView _recyclerView;
        RecyclerView.LayoutManager _LayoutManager;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.TBDPage);
            

            //  SupportActionBar.NavigationMode{ SetContentView(Resource.Layout.LoginPage); };
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = "Selected";
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            ColorDrawable colorDrawable = new ColorDrawable(Color.ParseColor("#FF8C00"));
            SupportActionBar.SetBackgroundDrawable(colorDrawable);


            _recyclerView = FindViewById<RecyclerView>(Resource.Id.TBDrecyclerView);
            var products = new List<Candidate>
            {
                new Candidate {Name = "Pooja Mehtre",Email="Poonamyadav@bridgelabz.com",Location="Mumbai",Date="22 March 19",ReferredBy="Email"},
                new Candidate {Name = "Priyanka Patil", Email="Priyankapatil@bridgelabz.com",Location="Mumbai",Date="22 March 19",ReferredBy="Email"},
                new Candidate {Name = "Shubham Agrawal",Email="Shubhamagrawal@bridgelabz.com",Location="Mumbai",Date="22 March 19",ReferredBy="Online"},
                new Candidate {Name = "Tanuja Nadaf", Email="Tanujanadaf@bridgelabz.com",Location="Mumbai",Date="22 March 19",ReferredBy="Online"},
                new Candidate {Name = "Priyanka Salunkhe", Email="Priyankasalunkhe@bridgelabz.com",Location="Mumbai",Date="22 March 19",ReferredBy="Online"},
                new Candidate {Name = "Sachin Mishra", Email="Sachinmishra@bridgelabz.com",Location="Mumbai",Date="22 March 19",ReferredBy="Email"},
                new Candidate {Name = "Divya Rane",Email="Divyarane@bridgelabz.com",Location="Mumbai",Date="22 March 19",ReferredBy="Email"},
                new Candidate {Name = "Shraddha Kandi", Email="Shraddhakandi@bridgelabz.com",Location="Mumbai",Date="22 March 19",ReferredBy="Online"},
                new Candidate {Name = "Nipun Shaha", Email="Nipunshaha@bridgelabz.com",Location="Mumbai",Date="22 March 19",ReferredBy="Online"},
                new Candidate {Name = "Prajakta Shaha", Email="Prajaktashaha@bridgelabz.com",Location="Mumbai",Date="22 March 19",ReferredBy="Email"},
                new Candidate {Name = "Ashwin Shaha", Email="Ashwinshaha@bridgelabz.com",Location="Mumbai",Date="22 March 19",ReferredBy="Email"},

            };

            _adapter = new RecyclerViewAdapter(this, products);
            _adapter.ItemClick += OnItemClick;
            _LayoutManager = new LinearLayoutManager(this);
            _recyclerView.SetLayoutManager(_LayoutManager);
            _recyclerView.SetAdapter(_adapter);
        }

        private void OnItemClick(object sender, int e)
        {
            List<Candidate> item = candidates.OrderBy(s => s.Name).ToList();
            var candidate = item[e];
            Intent intent = new Intent(this, typeof(CandidateDetails));
            intent.PutExtra("Candidate", JsonConvert.SerializeObject(candidate));
            this.StartActivity(intent);
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