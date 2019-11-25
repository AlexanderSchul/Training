using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WPFT_Training.Helper;

namespace WPFT_Training.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<TradableCoinsModel> tradableCoins;
        private bool isLoadingCoins;

        public List<TradableCoinsModel> TradableCoins
        {
            get
            {
                return this.tradableCoins;
            }
            set
            {
                this.tradableCoins = value;
                this.propertyChanged("TradableCoins");
            }
        }

        public bool IsLoadingCoins
        {
            get
            {
                return this.isLoadingCoins;
            }
            set
            {
                this.isLoadingCoins = value;
                this.propertyChanged("IsLoadingCoins");
            }
        }

        public ICommand LoadCoinsCommand { get; set; }

        private bool canLoadCoins => !this.IsLoadingCoins;

        public MainViewModel()
        {
            this.LoadCoinsCommand = new RelayCommand(o => this.loadCoins(), o => this.canLoadCoins);
        }


        private async void loadCoins()
        {
            this.IsLoadingCoins = true;
            this.TradableCoins = await TradableCoinsLoader.LoadTradableCoins();
            this.IsLoadingCoins = false;
        }

        private void propertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
