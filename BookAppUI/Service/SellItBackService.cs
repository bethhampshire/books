using BookAppUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookAppUI.Service
{
    public class SellItBackService
    {
        public async Task<SellItBackModel> GetPrice(string barcode, CancellationToken ct)
        {
            string url = "";
            url = $"https://www.sellitback.com/Sellitback.svc/SearchItem?UserID=&CartID=132922782359545213&EAN={ barcode }";

            HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url, ct);
            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();
            resp = resp.TrimStart('\"');
            resp = resp.TrimEnd('\"');
            resp = resp.Replace("\\", "");
            SellItBackModel priceModel = new SellItBackModel();
            try
            {
                if (resp.Contains("Accepted\":1"))
                {
                    priceModel = JsonConvert.DeserializeObject<SellItBackModel>(resp);
                    priceModel.Status = StatusEnum.ItemAccepted;
                }
                else if (resp.Contains("Sorry, we are not currently offering anything for this item"))
                {
                    priceModel.Status = StatusEnum.ItemNotAccepted;
                }
                else
                {
                    priceModel.Status = StatusEnum.ItemNotFound;
                }
            }
            catch
            {
                priceModel.Status = StatusEnum.ItemNotFound;
            }
            

            return priceModel;
        }
    }
}
