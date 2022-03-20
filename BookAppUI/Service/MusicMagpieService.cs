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
            // JsonConvert.DeserializeObject<List<Contributor>>(resp);
            PriceModel priceModel = JsonConvert.DeserializeObject<PriceModel>(resp);

            return priceModel;
        }
    }
}
