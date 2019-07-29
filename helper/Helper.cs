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
using Newtonsoft.Json;

namespace FundooWalkin.helper
{
    public class Helper
    {
        HttpClient client = new HttpClient();
        string url = "";

       /* public async Task<HttpResponseMessage> LoginUser(User user)
        {

            var uri = new Uri(url + "user/login");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response;
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            response = await client.PostAsync(uri, content);
            return response;
        }*/

    }
}