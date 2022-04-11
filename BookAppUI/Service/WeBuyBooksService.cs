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
        public async Task<WeBuyBooksModel> GetPrice(string barcode)
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
                    requestMessage.Headers.Add("authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpZCI6ImE3OWJhNDJhODIxOTlmMWZkY2FiZjIzNGQzMWY5YmNjOWU4ODg4ZjQiLCJqdGkiOiJhNzliYTQyYTgyMTk5ZjFmZGNhYmYyMzRkMzFmOWJjYzllODg4OGY0IiwiaXNzIjoiIiwiYXVkIjoiMSIsInN1YiI6bnVsbCwiZXhwIjoxNjQ5NzIyOTA4LCJpYXQiOjE2NDk3MDEzMDgsInRva2VuX3R5cGUiOiJiZWFyZXIiLCJzY29wZSI6IkJBU0tFVF82MjUzMmNmNmNjYzJkMC4wMjAwMzM0NCJ9.dmLwO6XT2RWTs2JKdMN4mSm8xMNeUaYob02kAAUdlqp1ZRVPeR2GFj-GxLvrJ-5YToAWn4AID4mt1tOkEv1IhSnXJrjAVH57WsVNrDdSLUSo1JCeC0cbt1rMqP_yGHCkY9ICOgOu3fHAk4f8j6VhOInHi6vH9zkGR8UvotDpYri704i1ue6dnhuQb6_ZHQ_glaPBeEN3zi1bG3cCD0EblDfhl2rjcFvFHRUFXq3kgiGGNccCXgVbJXw60gngQib7cicu059JvpDO4PMfdHNqfLXyhW9xpbn27lxZrOYn13a3pCm860Vy9-TPIe7tsblEOuaa81maHz6GaIB3qxs3ig");

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

        //public async Task<WeBuyBooksModel> Delete(string barcode)
        //{

        //    var url = new Uri("https://api2temp.revivalbooks.co.uk/basket/item/");
        //    var newPost = new WeBuyBooksPayloadModel()
        //    {
        //        barcode = barcode,
        //    };
        //    var newPostJson = JsonConvert.SerializeObject(newPost);
        //    var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
        //    using (var httpClient = new HttpClient())
        //    {
        //        using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, url))
        //        {
        //            requestMessage.Headers.Add("x-revival-site", "1");
        //            requestMessage.Headers.Add("x-revival-web", "1");
        //            requestMessage.Headers.Add("authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpZCI6ImE3OWJhNDJhODIxOTlmMWZkY2FiZjIzNGQzMWY5YmNjOWU4ODg4ZjQiLCJqdGkiOiJhNzliYTQyYTgyMTk5ZjFmZGNhYmYyMzRkMzFmOWJjYzllODg4OGY0IiwiaXNzIjoiIiwiYXVkIjoiMSIsInN1YiI6bnVsbCwiZXhwIjoxNjQ5NzIyOTA4LCJpYXQiOjE2NDk3MDEzMDgsInRva2VuX3R5cGUiOiJiZWFyZXIiLCJzY29wZSI6IkJBU0tFVF82MjUzMmNmNmNjYzJkMC4wMjAwMzM0NCJ9.dmLwO6XT2RWTs2JKdMN4mSm8xMNeUaYob02kAAUdlqp1ZRVPeR2GFj-GxLvrJ-5YToAWn4AID4mt1tOkEv1IhSnXJrjAVH57WsVNrDdSLUSo1JCeC0cbt1rMqP_yGHCkY9ICOgOu3fHAk4f8j6VhOInHi6vH9zkGR8UvotDpYri704i1ue6dnhuQb6_ZHQ_glaPBeEN3zi1bG3cCD0EblDfhl2rjcFvFHRUFXq3kgiGGNccCXgVbJXw60gngQib7cicu059JvpDO4PMfdHNqfLXyhW9xpbn27lxZrOYn13a3pCm860Vy9-TPIe7tsblEOuaa81maHz6GaIB3qxs3ig");

        //            requestMessage.Content = new StringContent(newPostJson, Encoding.UTF8, "application/json");
        //            var foo = await httpClient.SendAsync(requestMessage);
        //            var resp = await foo.Content.ReadAsStringAsync();
        //            WeBuyBooksModel priceModel = JsonConvert.DeserializeObject<WeBuyBooksModel>(resp);
        //            try
        //            {
        //                if (priceModel.Error != null && priceModel.Error.Contains("invalid_token"))
        //                {
        //                    priceModel.Status = StatusEnum.Unauthenticated;
        //                }
        //                else if (priceModel.Error != null && priceModel.Error.Contains("in_basket"))
        //                {
        //                    priceModel.Status = StatusEnum.DuplicateItem;
        //                }
        //                else if (priceModel.Error != null && priceModel.Error.Contains("not_accepted"))
        //                {
        //                    priceModel.Status = StatusEnum.ItemNotAccepted;
        //                }
        //                else if (resp.Contains("Product group is permitted"))
        //                {
        //                    priceModel.Status = StatusEnum.ItemAccepted;
        //                }
        //            }
        //            catch
        //            {
        //                priceModel.Status = StatusEnum.ItemNotAccepted;
        //            }


        //            return priceModel;
        //        };
        //    }
        //}
    }
}
