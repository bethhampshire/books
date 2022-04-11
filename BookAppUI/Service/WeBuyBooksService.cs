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
                    requestMessage.Headers.Add("authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpZCI6IjFkYjM0YWY1NzE3NmEyYWU4ZDVmODUxYWFhZTc2NDQwNDZiMDgzMGMiLCJqdGkiOiIxZGIzNGFmNTcxNzZhMmFlOGQ1Zjg1MWFhYWU3NjQ0MDQ2YjA4MzBjIiwiaXNzIjoiIiwiYXVkIjoiMSIsInN1YiI6bnVsbCwiZXhwIjoxNjQ5NjM5Nzg1LCJpYXQiOjE2NDk2MTgxODUsInRva2VuX3R5cGUiOiJiZWFyZXIiLCJzY29wZSI6IkJBU0tFVF82MjUzMmNmNmNjYzJkMC4wMjAwMzM0NCJ9.GwCADVHxdpJDlj5axSz5ITZ-97YqSyg3kaAKEZz3aaURy0O6ot3nah3FQ-ewp2TGlOMDILGKTtto5bY6EqlvFxqb-u1EUDqoixTY9-G8bgKSltpjsuhzLiX9pHG07VfNesjazh1NZ4bB1pPUYgXdzlrjp2BF9jsb9p9kNycGDHBDgFJdPTSymSIPZoXbKGN73yxnBlRWkZvCqC7norARIURplrKw9sX4tFeZtpOY4iYdQga9NqT2V-CWAe4Cj88hvsc5i-gILFQujJkLfi-PNwJtE4MqPL6ilLOo9UBTjqWveNzOH58PD-pikN0p619b-9I-WfV100IE2IzY-5eSFQ");

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
