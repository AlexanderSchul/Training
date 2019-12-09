
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Linq;

namespace WPFT_Training.Helper
{
    class TradableCoinsLoader
    {
        public static async Task<List<TradableCoinsModel>> LoadTradableCoins(int Rankfrom, int Rankto) 
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://api.coinmarketcap.com/v1/ticker/");
            var content = await response.Content.ReadAsStringAsync();
            var fullTradableCoinsViewModel = JsonConvert.DeserializeObject<List<TradableCoinsModel>>(content);

            if (!(Rankfrom == 0 & Rankto == 0))
            {
                List<TradableCoinsModel> partialTradableCoinsViewModel = fullTradableCoinsViewModel.Where(o => int.Parse(o.Rank) >= Rankfrom && int.Parse(o.Rank) <= Rankto).ToList();
                return partialTradableCoinsViewModel;
            }
            else return fullTradableCoinsViewModel;
            
        }
    }
}
