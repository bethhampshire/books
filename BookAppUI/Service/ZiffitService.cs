using BookAppUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookAppUI.Service
{
    public class ZiffitService
    {
        public async Task<PriceModel> GetPrice(string barcode)
        {
            var url = new Uri("https://ziffit-recommerce-gateway-eu.ziffit.com/scan");
            //StringContent content = new StringContent("{ ean: \"9780099448822\", scanOrigin: \"ZIFFIT\"}");
            var newPost = new Post()
            {
                ean = "9780099448822",
                scanOrigin = "ZIFFIT"
            };
            var newPostJson = JsonConvert.SerializeObject(newPost);
            var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, url))
                {
                    requestMessage.Headers.Add("x-region-id", "GB");
                    requestMessage.Headers.Add("accept-language", "en-GB");
                    requestMessage.Headers.Add("authorization", "Bearer eyJhbGciOiJIUzUxMiJ9.eyJ2aXNpdG9ySWQiOiIwMUZZSEZTMEI2SlcxWlZCRTRWWERXTllWWSIsImN1c3RvbWVySWQiOiIiLCJiYXNrZXRJZCI6IjAxRllIRlMwQjhUWE5RUEhONENZQjUzVkZLIiwibG9jYWxlIjoiZW5fR0IiLCJhdXRob3JpdGllcyI6IlJPTEVfQU5PTllNT1VTLFJPTEVfR1VFU1QiLCJleHAiOjE2NTAzOTQyODV9.ol7aw_OaVIqr9Y-rjNI9uF-U8zz170ryuhy3pqbmFv5D0x5C1UsHfmzM0uIR9eIWGWhWGHEP_YsqwmI8DiQX9w");

                    requestMessage.Content = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var foo = await httpClient.SendAsync(requestMessage);
                    var resp = await foo.Content.ReadAsStringAsync();
                    PriceModel priceModel = JsonConvert.DeserializeObject<PriceModel>(resp);
                    return priceModel;
                };
            }
            //string test = "{\"ean\":\"9780099448822\",\"scanOrigin\":\"ZIFFIT\"}";

            //t
            //var content = new FormUrlEncodedContent(data);
            //var response1 = await ApiHelper.ApiClient.PostAsync(url, content);


            //response.EnsureSuccessStatusCode();

            //t
            //var resp = await response1.Content.ReadAsStringAsync();


            //resp = resp.TrimStart('\"');
            //resp = resp.TrimEnd('\"');
            //resp = resp.Replace("\\", "");
            // JsonConvert.DeserializeObject<List<Contributor>>(resp);

            //t
            //PriceModel priceModel = JsonConvert.DeserializeObject<PriceModel>(resp);
            //return priceModel;
        }
    }
}
