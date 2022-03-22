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
                    requestMessage.Headers.Add("authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpZCI6ImFlZWQxZTkyNWFmYzA5OThlNTZlMGQ2MzA2NjRhMDY2NGQyMjY3NzciLCJqdGkiOiJhZWVkMWU5MjVhZmMwOTk4ZTU2ZTBkNjMwNjY0YTA2NjRkMjI2Nzc3IiwiaXNzIjoiIiwiYXVkIjoiMSIsInN1YiI6bnVsbCwiZXhwIjoxNjQ3OTI2MDgzLCJpYXQiOjE2NDc5MDQ0ODMsInRva2VuX3R5cGUiOiJiZWFyZXIiLCJzY29wZSI6IkJBU0tFVF82MjM1ZjlhNTI2ZTExOS4xMjYwMzUyNCJ9.kki1A_jJpuVSNGrJEXp-XR7uZmgo4D5kD8FFXBRT5ii90Yvl-otEYNLxODcMByAXX1x2miD-UBPs0Y6Q-ZY0Pt_F4GiaqtIC78mzrKkHGohCUWPy9m7R5L6JwvW_fEjpcmlZEj3qJPyoAWJQDdvuYUFCI6Bkf-n7U1iZWXKDWmybYJaYhAUs4XJ8c3GjgOrocsatdrhTbfQ3-f1KcXYcbh17zJNX56Utr5X6C9ixeRK28XXpocoRImZ2DevJApshs96wbD6yrohsPAK0rH7T9R5P8oIzi0kpQfm_OJxTwYmoSN1Fkd6p6rEXohXS2_vbhbEn36pEQOi48S1ArvCD-w");

                    requestMessage.Content = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var foo = await httpClient.SendAsync(requestMessage);
                    var resp = await foo.Content.ReadAsStringAsync();
                    WeBuyBooksModel priceModel = JsonConvert.DeserializeObject<WeBuyBooksModel>(resp);
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
