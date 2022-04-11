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
                    try
                    {
                        if (priceModel.Error != null && priceModel.Error.Contains("invalid_token"))
                        {
                            priceModel.Status = StatusEnum.Unauthenticated;
                        }
                        else if (priceModel.Error != null && priceModel.Error.Contains("in_basket"))
                        {
                            priceModel.Status = StatusEnum.DuplicateItem;
                        }
                        else if (priceModel.Error != null && priceModel.Error.Contains("not_accepted"))
                        {
                            priceModel.Status = StatusEnum.ItemNotAccepted;
                        }
                        else if (resp.Contains("Product group is permitted"))
                        {
                            priceModel.Status = StatusEnum.ItemAccepted;
                        }
                    }
                    catch
                    {
                        priceModel.Status = StatusEnum.ItemNotAccepted;
                    }
                    

                    return priceModel;
                };
            }
        }

        public async Task Delete(string id, string authToken)
        {
            string url = "";
            url = $"https://api2temp.revivalbooks.co.uk/basket/item/{ id }";
            //var newPost = new WeBuyBooksPayloadModel()
            //{
            //    barcode = barcode,
            //};
            //var newPostJson = JsonConvert.SerializeObject(newPost);
            //var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Delete, url))
                {
                    requestMessage.Headers.Add("x-revival-site", "1");
                    requestMessage.Headers.Add("x-revival-web", "1");
                    requestMessage.Headers.Add("authorization", authToken);

                    //requestMessage.Content = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var foo = await httpClient.SendAsync(requestMessage);
                    var resp = await foo.Content.ReadAsStringAsync();
                };
            }
        }
    }
}
