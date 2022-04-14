using BookAppUI.Models;
using BookAppUI.Service;
using BookAppUI.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookAppUI.Views
{
    /// <summary>
    /// Logika interakcji dla klasy SessionView.xaml
    /// </summary>
    public partial class SessionView : UserControl
    {
        public SessionView()
        {
            InitializeComponent();
            _sellItBackService = new SellItBackService();
            _musicMagpieService = new MusicMagpieService();
            _ziffitService = new ZiffitService();
            _weBuyBooksService = new WeBuyBooksService();
            AddToActivityLog(DateTime.Now.ToShortTimeString() + ": Hello Nigel");
            AddToActivityLog(DateTime.Now.ToShortTimeString() + ": Your session has started");
            PrintActivityLog(ActivityLog);
            authTokens = new AuthTokenModel();
            GetAuthTokens();
        }

        private readonly SellItBackService _sellItBackService;
        private readonly ZiffitService _ziffitService;
        private readonly MusicMagpieService _musicMagpieService;
        private readonly WeBuyBooksService _weBuyBooksService;

        public ICommand NavigateHomeCommand { get; }
        public ICommand NavigateEndSessionCommand { get; }
        public PriceModel musicMagpiePrice { get; set; }
        public ZiffitModel ziffitPrice { get; set; }
        public SellItBackModel sellItBackPrice { get; set; }
        public WeBuyBooksModel weBuyBooksPrice { get; set; }
        // public WeBuyBooksServicecs weBuyBooksPrice { get; set; }

        public string title = "";
        public string barcode = "";

        private AuthTokenModel authTokens;

        public List<string> ActivityLog = new List<string>();

        private void AddToActivityLog(string message)
        {
            ActivityLog.Add(message);
        }

        private void PrintActivityLog(List<string> ActivityLog)
        {
            ActivityLogView.Text = "";
            if (ActivityLog.Count == 1)
            {
                ActivityLogView.Text += ActivityLog[0];
            }
            if (ActivityLog.Count > 1)
            {
                foreach (var activityLog in ActivityLog)
                {
                    ActivityLogView.Text += activityLog;
                    ActivityLogView.Text += System.Environment.NewLine;
                }
            }
        }
        // this returns the models
        private async Task GetPrices(string barcode)
        {
            AddToActivityLog(DateTime.Now.ToShortTimeString() + ": Ziffit searching...");
            PrintActivityLog(ActivityLog);
            ziffitPrice = await _ziffitService.GetPrice(barcode, authTokens.ziffit_token);
            AddToActivityLog(DateTime.Now.ToShortTimeString() + ": Sell it back searching...");
            PrintActivityLog(ActivityLog);
            sellItBackPrice = await _sellItBackService.GetPrice(barcode);
            AddToActivityLog(DateTime.Now.ToShortTimeString() + ": We buy books searching...");
            PrintActivityLog(ActivityLog);
            weBuyBooksPrice = await _weBuyBooksService.GetPrice(barcode, authTokens.weBuyBooks_token);
            AddToActivityLog(DateTime.Now.ToShortTimeString() + ": Music magpie searching...");
            PrintActivityLog(ActivityLog);
            musicMagpiePrice = await _musicMagpieService.GetPrice(barcode);
            if (weBuyBooksPrice.Status == StatusEnum.ItemAccepted)
            {
                await _weBuyBooksService.Delete(weBuyBooksPrice.Item.Id, authTokens.weBuyBooks_token);
            }
            if (ziffitPrice.Status == StatusEnum.ItemAccepted)
            {
                await _ziffitService.Delete(ziffitPrice.Value.CartItemId, authTokens.ziffit_token);
            }
        }

        public string ReadBarcode(RoutedEventArgs e)
        {
            barcode = BarcodeInput.Text;
            AddToActivityLog(DateTime.Now.ToShortTimeString() + ": Searched " + barcode);
            PrintActivityLog(ActivityLog);
            return barcode;
        }

        private async void Search_Button_Click(object sender, RoutedEventArgs e)
        {

            BarcodeValue.Text = BarcodeInput.Text;
            string barcode = ReadBarcode(e);
            BarcodeInput.Text = "";
            BookTitle.Text = "Searching...";
            await GetPrices(barcode);
            AssignValues();
        }

        public void GetAuthTokens()
        {
            using (StreamReader r = new StreamReader("../../Tokens/AuthTokens.json"))
            {
                string json = r.ReadToEnd();
                authTokens = JsonConvert.DeserializeObject<AuthTokenModel>(json);
            }
        }

        private void AssignValues()
        {
            BookTitle.Text = "";

            if (musicMagpiePrice.Status == StatusEnum.ItemAccepted)
            {
                MMPrice.Text = "£" + musicMagpiePrice.Price.ToString("F");
                if (BookTitle.Text == "")
                {
                    BookTitle.Text = musicMagpiePrice.Album.ToString();
                }
            }
            else
            {
                MMPrice.Text = " - - ";
                if (musicMagpiePrice.Status == StatusEnum.DuplicateItem)
                {
                    AddToActivityLog(DateTime.Now.ToShortTimeString() + ": Music magpie No duplicate item");
                }
                else if (musicMagpiePrice.Status == StatusEnum.Unauthenticated)
                {
                    AddToActivityLog(DateTime.Now.ToShortTimeString() + ": Music magpie: Update auth token");
                }
                else if (musicMagpiePrice.Status == StatusEnum.ItemNotFound)
                {
                    AddToActivityLog(DateTime.Now.ToShortTimeString() + ": Music magpie: Item not found");
                }
                else if(musicMagpiePrice.Status == StatusEnum.InvalidBarcode)
                {
                    AddToActivityLog(DateTime.Now.ToShortTimeString() + ": Music magpie: This barcode is invalid");
                }
                else if(musicMagpiePrice.Status == StatusEnum.ItemNotAccepted)
                {
                    AddToActivityLog(DateTime.Now.ToShortTimeString() + ": Music magpie: Item not accepted");
                }
                else if (musicMagpiePrice.Status == StatusEnum.ItemNotAccepted)
                {
                    AddToActivityLog("Music magpie: Item not accepted");
                }
            }

            if (sellItBackPrice.Status == StatusEnum.ItemAccepted)
            {
                SIBPrice.Text = "£" + sellItBackPrice.Price.ToString("F");
                BookTitle.Text = sellItBackPrice.Title;
            }
            else
            {
                SIBPrice.Text = " - - ";
                if (sellItBackPrice.Status == StatusEnum.DuplicateItem)
                {
                    AddToActivityLog(DateTime.Now.ToShortTimeString() + ": Sell it back: No duplicate item");
                }
                else if (sellItBackPrice.Status == StatusEnum.Unauthenticated)
                {
                    AddToActivityLog(DateTime.Now.ToShortTimeString() + ": Sell it back: Update auth token");
                }
                else if (sellItBackPrice.Status == StatusEnum.ItemNotFound)
                {
                    AddToActivityLog(DateTime.Now.ToShortTimeString() + ": Sell it back: Item not found");
                }
                else if (sellItBackPrice.Status == StatusEnum.InvalidBarcode)
                {
                    AddToActivityLog(DateTime.Now.ToShortTimeString() + ": Sell it back: This barcode is invalid");
                }
                else if (sellItBackPrice.Status == StatusEnum.ItemNotAccepted)
                {
                    AddToActivityLog(DateTime.Now.ToShortTimeString() + ": Sell it back: Item not accepted");
                }
                else if (sellItBackPrice.Status == StatusEnum.ItemNotAccepted)
                {
                    AddToActivityLog("Sell it back: Item not accepted");
                }
            }

            if (ziffitPrice.Status == StatusEnum.ItemAccepted)
            {
                ZFPrice.Text = "£" + ziffitPrice.Value.Offer.ToString("F");

                if (BookTitle.Text == "")
                {
                    BookTitle.Text = ziffitPrice.Value.Title.ToString();
                }
            }
            else
            {
                ZFPrice.Text = " - - ";
                if (ziffitPrice.Status == StatusEnum.DuplicateItem)
                {
                    AddToActivityLog(DateTime.Now.ToShortTimeString() + ": Ziffit: No duplicate item");
                }
                else if (ziffitPrice.Status == StatusEnum.Unauthenticated)
                {
                    AddToActivityLog(DateTime.Now.ToShortTimeString() + ": Ziffit: Update auth token");
                }
                else if (ziffitPrice.Status == StatusEnum.ItemNotFound)
                {
                    AddToActivityLog(DateTime.Now.ToShortTimeString() + ": Ziffit: Item not found");
                }
                else if (ziffitPrice.Status == StatusEnum.InvalidBarcode)
                {
                    AddToActivityLog(DateTime.Now.ToShortTimeString() + ": Ziffit: This barcode is invalid");
                }
                else if (ziffitPrice.Status == StatusEnum.ItemNotAccepted)
                {
                    AddToActivityLog(DateTime.Now.ToShortTimeString() + ": Ziffit: Item not accepted");
                }
                else if (ziffitPrice.Status == StatusEnum.ItemNotAccepted)
                {
                    AddToActivityLog("Ziffit: Item not accepted");
                }
            }

            if (weBuyBooksPrice.Status == StatusEnum.ItemAccepted)
            {
                WeBuyBooksPrice.Text = "£" + weBuyBooksPrice.Price.ToString("F");

                if (BookTitle.Text == "")
                {
                    BookTitle.Text = weBuyBooksPrice.Item.Title.ToString();
                }
            }
            else
            {
                WeBuyBooksPrice.Text = " - -";
                if (weBuyBooksPrice.Status == StatusEnum.DuplicateItem)
                {
                    AddToActivityLog(DateTime.Now.ToShortTimeString() + ": We buy books: No duplicate item");
                }
                else if (weBuyBooksPrice.Status == StatusEnum.Unauthenticated)
                {
                    AddToActivityLog(DateTime.Now.ToShortTimeString() + ": We buy books: Update auth token");
                }
                else if (weBuyBooksPrice.Status == StatusEnum.ItemNotFound)
                {
                    AddToActivityLog(DateTime.Now.ToShortTimeString() + ": We buy books: Item not found");
                }
                else if (weBuyBooksPrice.Status == StatusEnum.InvalidBarcode)
                {
                    AddToActivityLog(DateTime.Now.ToShortTimeString() + ": We buy books: This barcode is invalid");
                }
                else if (weBuyBooksPrice.Status == StatusEnum.ItemNotAccepted)
                {
                    AddToActivityLog(DateTime.Now.ToShortTimeString() + ": We buy books: Item not accepted");
                }
                else if (weBuyBooksPrice.Status == StatusEnum.ItemNotAccepted)
                {
                    AddToActivityLog("We buy books: Item not accepted");
                }
            }

            if (BookTitle.Text == "")
            {
                BookTitle.Text = "Book not found";
            }

            PrintActivityLog(ActivityLog);
        }

    }
}
