
using System.Windows;
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

        private async void LoadCoinsButton_Click(object sender, RoutedEventArgs e)
        {
            loadCoinsButton.IsEnabled = false;
            loadingStatusPB.IsIndeterminate = true;
            
            coinsDataGrid.ItemsSource = await TradableCoinsLoader.LoadTradableCoins();
            
            loadingStatusPB.IsIndeterminate = false;
            loadCoinsButton.IsEnabled = true;
        }
    }
    
}
