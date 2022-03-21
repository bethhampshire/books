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
        public async Task<ZiffitModel> GetPrice(string barcode)
        {
            var url = new Uri("https://ziffit-recommerce-gateway-eu.ziffit.com/scan");
            var newPost = new Post()
            {
                ean = barcode,
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
                    ZiffitModel priceModel = JsonConvert.DeserializeObject<ZiffitModel>(resp);
                    return priceModel;
                };
            }
        }
    }
}
