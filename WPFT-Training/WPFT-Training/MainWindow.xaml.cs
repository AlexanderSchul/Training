using System.Collections.Generic;
using System.Windows;
using Newtonsoft.Json;
using System.Net.Http;
using WPFT_Training.Helper;




namespace WPFT_Training
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
 
    public partial class MainWindow : Window
    {
     
        public MainWindow()
        {
            InitializeComponent();

           
            
                
            
        }

        private async void loadCoinsButton_Click(object sender, RoutedEventArgs e)
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://api.coinmarketcap.com/v1/ticker/");
            var content = await response.Content.ReadAsStringAsync();
            var coinList = JsonConvert.DeserializeObject<List<TradableCoins>>(content);
            
            
            

            coinsDataGrid.ItemsSource = coinList;
        }
    }
    
}
