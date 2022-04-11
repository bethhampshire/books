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
    public class MusicMagpieService
    {
        public async Task<PriceModel> GetPrice(string barcode)
        {
            string url = "";
            url = $"https://www.musicmagpie.co.uk/Umbraco/Surface/MediaSearch/SearchItemByBarcode?barcode={ barcode }";

            HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();
            resp = resp.TrimStart('\"');
            resp = resp.TrimEnd('\"');
            resp = resp.Replace("\\", "");
            resp = resp.Replace("\"\"", "");
           PriceModel priceModel = new PriceModel();
            try
            {
                if (resp.Contains("Unfortunately we don't seem to recognise this barcode. Please either try again or enter another title."))
                {
                    priceModel.Status = StatusEnum.ItemNotFound;
                }
                else if (resp.Contains("Sorry, we don’t accept multiple copies of the same item. Please enter a different item."))
                {
                    priceModel.Status = StatusEnum.DuplicateItem;
                }
                else if (resp.Contains("Unfortunately we can’t accept this title at the moment. Do you have another title you’d like to try?"))
                {
                    priceModel.Status = StatusEnum.ItemNotAccepted;
                }
                else if (resp.Contains("\"status\":0"))
                {
                    priceModel = JsonConvert.DeserializeObject<PriceModel>(resp);
                    priceModel.Status = StatusEnum.ItemAccepted;
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
