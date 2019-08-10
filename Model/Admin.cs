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
    /// <summary>
    /// admin class
    /// </summary>
   public class Admin
    {
        /// <summary>
        /// to get and set id of admin
        /// </summary>
       public string loginId { set; get; }

        /// <summary>
        /// to get and set password
        /// </summary>
       public string password { set; get; }
    }
}