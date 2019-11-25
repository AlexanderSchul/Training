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
            loadCoinsButton.IsEnabled = false;
            loadingStatusPB.IsIndeterminate = true;
            
            coinsDataGrid.ItemsSource = await TradableCoinsLoader.LoadTradableCoins();
            
            loadingStatusPB.IsIndeterminate = false;
            loadCoinsButton.IsEnabled = true;
        }
    }
    
}
