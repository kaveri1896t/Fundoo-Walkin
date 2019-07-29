using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace FundooWalkin
{
    class RecyclerViewAdapter : RecyclerView.Adapter, IFilterable
    {
        private List<Candidate> _originalData;
        private List<Candidate> _items;
        private readonly Activity _context;

        public Filter Filter { get; private set; }

        public RecyclerViewAdapter(Activity activity, IEnumerable<Candidate> cndidates)
        {
            _items = cndidates.OrderBy(s => s.Name).ToList();
            _context = activity;

            Filter = new CandidateFilter(this);
        }

        public override long GetItemId(int position)
        {
            return position;
        }


        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Candiate, parent, false);
            CandidateHolder vh = new CandidateHolder(itemView);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            CandidateHolder vh = holder as CandidateHolder;

            var candidate = _items[position];

            vh.Image.SetImageResource(candidate.DrawableId);
            vh.Caption.Text = candidate.Name;
        }

        public override int ItemCount
        {
            get { return _items.Count; }
        }

        public class CandidateHolder : RecyclerView.ViewHolder
        {
            public ImageView Image { get; private set; }
            public TextView Caption { get; private set; }

            public CandidateHolder(View itemView) : base(itemView)
            {
                Image = itemView.FindViewById<ImageView>(Resource.Id.emailImage);
                Caption = itemView.FindViewById<TextView>(Resource.Id.emailtext);
            }
        }

        private class CandidateFilter : Filter
        {
            private readonly RecyclerViewAdapter _adapter;
            public CandidateFilter(RecyclerViewAdapter adapter)
            {
                _adapter = adapter;
            }

            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                var returnObj = new FilterResults();
                var results = new List<Candidate>();
                if (_adapter._originalData == null)
                    _adapter._originalData = _adapter._items;

                if (constraint == null) return returnObj;

                if (_adapter._originalData != null && _adapter._originalData.Any())
                {
                    // Compare constraint to all names lowercased. 
                    // It they are contained they are added to results.
                    results.AddRange(
                        _adapter._originalData.Where(
                            chemical => chemical.Name.ToLower().Contains(constraint.ToString())));
                }

                // Nasty piece of .NET to Java wrapping, be careful with this!
                returnObj.Values = FromArray(results.Select(r => r.ToJavaObject()).ToArray());
                returnObj.Count = results.Count;

                constraint.Dispose();

                return returnObj;
            }

            protected override void PublishResults(ICharSequence constraint, FilterResults results)
            {
                using (var values = results.Values)
                    _adapter._items = values.ToArray<Java.Lang.Object>()
                        .Select(r => r.ToNetObject<Candidate>()).ToList();

                _adapter.NotifyDataSetChanged();

                // Don't do this and see GREF counts rising
                constraint.Dispose();
                results.Dispose();
            }
        }
    }

}