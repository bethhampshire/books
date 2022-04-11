using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BookAppUI.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace BookAppUI.Service
{
    public class PriceService : IPriceService
    {
        public async Task<PriceModel> GetPrice(StoreType storeType)
        {
            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();

                client.BaseAddress = new Uri("https://www.musicmagpie.co.uk/");
                client.DefaultRequestHeaders.Add("User-Agent", "C# console program");
                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                var url = "/Umbraco/Surface/MediaSearch/SearchItemByBarcode?barcode=9780099448822";
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var resp = await response.Content.ReadAsStringAsync();

                string jsonResponse = await response.Content.ReadAsStringAsync();
                PriceModel priceModel = JsonConvert.DeserializeObject<PriceModel>(jsonResponse);
                return priceModel;
            }
        }
    }
}
