
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using WPFT_Training.Helper;

namespace WPFT_Training.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private List<TradableCoinsModel> coinList;
        private bool isLoadingCoins;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand LoadCoinsCommand { get; set; }
        private bool canLoadCoins => !IsLoadingCoins;
        public bool IsLoadingCoins
        {
            get { return isLoadingCoins; }
            set 
            {
                isLoadingCoins = value;
                propertyChanged("IsLoadingCoins");
            }
        }
        
        public List<TradableCoinsModel> CoinList 
        {
            get { return coinList; }
            set 
            {
                coinList = value;
                propertyChanged("CoinList");
            }
        }

        public void propertyChanged(string propertyName) 
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainViewModel()
        {
            LoadCoinsCommand = new RelayCommand(obj => loadCoins(), obj => canLoadCoins);
        }
        private async void loadCoins() 
        {
            IsLoadingCoins = true;
            CoinList = await TradableCoinsLoader.LoadTradableCoins();
            IsLoadingCoins = false;
        }
    }

    
}
