
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace WPFT_Training.Helper
{
    class TradableCoinsLoader
    {
        public static async Task<List<TradableCoinsModel>> LoadTradableCoins() 
        {
            
            var client = new HttpClient();
            var response = await client.GetAsync("https://api.coinmarketcap.com/v1/ticker/");
            var content = await response.Content.ReadAsStringAsync();
            var tradableCoinsViewModel = JsonConvert.DeserializeObject<List<TradableCoinsModel>>(content);
            return tradableCoinsViewModel;
        }
    }
}
