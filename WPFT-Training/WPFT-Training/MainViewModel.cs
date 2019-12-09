
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using WPFT_Training.Helper;
using System.Data.SqlClient;

namespace WPFT_Training.MainViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        SqlConnection cnnCoinNotesDB;
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Alex\Repositories\WPFT-Training\WPFT-Training\CoinNotesDatabase.mdf;Integrated Security=True";
        SqlCommand command;
        SqlDataReader dataReader;
        SqlDataAdapter adapter = new SqlDataAdapter();
        private string sql = "";
        

        private string getCoinNote;
        private List<TradableCoinsModel> coinList;
        private bool isLoadingCoins;
        private int rankFrom;
        private int rankTo;
        private string coinNote;
        private TradableCoinsModel selectedCoin;
        
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand LoadCoinsCommand { get; set; }
        public ICommand SaveNoteCommand { get; set; }
        private bool canLoadCoins => !IsLoadingCoins;

        public TradableCoinsModel SelectedCoin
        {
            get { return selectedCoin; }
            set 
            { 
                selectedCoin = value;
                propertyChanged("SelectedCoin");
            }
        }
        public string CoinNote 
        {
            get { return coinNote; }
            set 
            {
                coinNote = value;
                propertyChanged("CoinNote");
            }
        }
        public int RankFrom 
        {
            get { return rankFrom; }
            set 
            {
                rankFrom = value;
                propertyChanged("RankFrom");
            }
        }
        public int RankTo
        {
            get { return rankTo; }
            set
            {
                rankTo = value;
                propertyChanged("RankTo");
            }
        }
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
        public string GetCoinNote 
        {
            get 
            {
                if (SelectedCoin == null)
                {
                    return "abc";

                }
                else
                {
                    //string coinRank = SelectedCoin.Rank;
                    //string CoinNote = GetCoinNoteDB(int.Parse(SelectedCoin.Rank));
                    //return GetCoinNoteDB(int.Parse(SelectedCoin.Rank));
                    return "selected something";
                }
            }
            set 
            {
                getCoinNote = value;
                propertyChanged("GetCoinNote");
            }
        }
        

        public void propertyChanged(string propertyName) 
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainViewModel()
        {
            LoadCoinsCommand = new RelayCommand(obj => loadCoins(), obj => canLoadCoins);
            SaveNoteCommand = new RelayCommand(obj => saveNoteToDB(CoinNote, int.Parse(SelectedCoin.Rank)), obj => canLoadCoins);
        }
        private async void loadCoins() 
        {
            IsLoadingCoins = true;
            CoinList = await TradableCoinsLoader.LoadTradableCoins(RankFrom, RankTo);
            IsLoadingCoins = false;
        }
        private void saveNoteToDB(string coinNote, int coinRank)
        {
            cnnCoinNotesDB = new SqlConnection(connectionString);
            sql = "Insert into dbo.CoinNotes (CoinNote,CoinId) values ('" + coinNote + "' , '" + coinRank + "')";
            cnnCoinNotesDB.Open();

            command = new SqlCommand(sql, cnnCoinNotesDB);
            adapter.InsertCommand = new SqlCommand(sql, cnnCoinNotesDB);
            adapter.InsertCommand.ExecuteNonQuery();

            command.Dispose();
            cnnCoinNotesDB.Close();

        }
        public string GetCoinNoteDB(int coinId)
        {
            var note="";
            
            cnnCoinNotesDB = new SqlConnection(connectionString);
            cnnCoinNotesDB.Open();
            sql = "Select CoinNote from dbo.CoinNotes";
            command = new SqlCommand(sql, cnnCoinNotesDB);
            dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    note = dataReader.GetString(coinId-1);
                    
                }
                
            }

            dataReader.Close();
            command.Dispose();
            cnnCoinNotesDB.Close();
            return note;
        }
    }

    
}
