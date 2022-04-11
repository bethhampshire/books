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
    public class WeBuyBooksService
    {
        public async Task<WeBuyBooksModel> GetPrice(string barcode, string authToken)
        {
            var url = new Uri("https://api2temp.revivalbooks.co.uk/basket/item");
            var newPost = new WeBuyBooksPayloadModel()
            {
                barcode = barcode,
            };
            var newPostJson = JsonConvert.SerializeObject(newPost);
            var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, url))
                {
                    requestMessage.Headers.Add("x-revival-site", "1");
                    requestMessage.Headers.Add("x-revival-web", "1");
                    requestMessage.Headers.Add("authorization", authToken);

                    requestMessage.Content = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var foo = await httpClient.SendAsync(requestMessage);
                    var resp = await foo.Content.ReadAsStringAsync();
                    WeBuyBooksModel priceModel = JsonConvert.DeserializeObject<WeBuyBooksModel>(resp);
                    if (priceModel.Error != null && priceModel.Error.Contains("invalid_token"))
                    {
                        priceModel.Status = StatusEnum.Unauthenticated;
                    }
                    else if (priceModel.Error != null && priceModel.Error.Contains("in_basket"))
                    {
                        priceModel.Status = StatusEnum.DuplicateItem;
                    }
                    else
                    {
                        priceModel.Status = StatusEnum.ItemAccepted;
                    }
                    return priceModel;
                };
            }
        }

        // TODO MAGDA
        //public async Task<ZiffitModel> Delete(string barcode)
        //{
            
        //}
    }
}
