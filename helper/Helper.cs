using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FundooWalkin.Model;
using Newtonsoft.Json;

namespace FundooWalkin.helper
{
    public class Helper
    {
        HttpClient client = new HttpClient();
        string url = "https://fundoowalkin-backend-stg.incubation.bridgelabz.com:443/";

         public async Task<HttpResponseMessage> LoginAdmin(Admin admin)
         {

             var uri = new Uri(url + "walkin/admin");
             client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

             HttpResponseMessage response;
             var json = JsonConvert.SerializeObject(admin);
             var content = new StringContent(json, Encoding.UTF8, "application/json");

             response = await client.PostAsync(uri, content);
             return response;
         }

    }
}