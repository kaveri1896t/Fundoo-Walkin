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


namespace FundooWalkin.Model
{
    public class Candidate
    {
        public string Name { get; set; }

       // public int DrawableId { get; set; }


        public string Email { get; set; }

        public string Location { get; set; }

        public string Date { get; set; }


        public string  ReferredBy { get; set; }

    }
}