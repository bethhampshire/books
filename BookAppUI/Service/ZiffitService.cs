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
                    requestMessage.Headers.Add("authorization", "Bearer eyJhbGciOiJIUzUxMiJ9.eyJ2aXNpdG9ySWQiOiIwMUZZS1c0TTczRUtHWkNHME5ZWkY5QzgzOCIsImN1c3RvbWVySWQiOiIiLCJiYXNrZXRJZCI6IjAxRllLVzRNNzZKQTZRNFRBMlc0NzhZSlA2IiwibG9jYWxlIjoiZW5fR0IiLCJhdXRob3JpdGllcyI6IlJPTEVfQU5PTllNT1VTLFJPTEVfR1VFU1QiLCJleHAiOjE2NTIyMDUyMzJ9.0Uk8cM7PRL6bvTLLrO3dKscQjdskKVDEW8RdvxmVfB4NZFfzjqPBf2U_WlQcqUExHsUpOERFLr67RFNFXiDIyw");

                    requestMessage.Content = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var foo = await httpClient.SendAsync(requestMessage);
                    var resp = await foo.Content.ReadAsStringAsync();
                    ZiffitModel priceModel = JsonConvert.DeserializeObject<ZiffitModel>(resp);

                    if (priceModel.Value.RejectionCode != null && priceModel.Value.RejectionCode.Contains("TOO_MANY_DUPLICATES"))
                    {
                        priceModel.Status = StatusEnum.DuplicateItem;
                    }
                    else if (!priceModel.Success)
                    {
                        priceModel.Status = StatusEnum.ItemNotFound;
                    }
                    return priceModel;
                };
            }
        }
    }
}
