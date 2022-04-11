﻿using BookAppUI.Models;
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
        public async Task<ZiffitModel> GetPrice(string barcode, string authToken)
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
                    requestMessage.Headers.Add("authorization", authToken);

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
