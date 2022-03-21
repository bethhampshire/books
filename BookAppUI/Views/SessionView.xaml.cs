using BookAppUI.Models;
using BookAppUI.Service;
using BookAppUI.ViewModels;
using System;
using System.Collections.Generic;
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
        }

        private readonly SellItBackService _sellItBackService;
        private readonly ZiffitService _ziffitService;
        private readonly MusicMagpieService _musicMagpieService;

        public ICommand NavigateHomeCommand { get; }
        public ICommand NavigateEndSessionCommand { get; }
        public PriceModel musicMagpiePrice { get; set; }
        public ZiffitModel ziffitPrice { get; set; }
        public SellItBackModel sellItBackPrice { get; set; }

        public string title = "";
        public string barcode = "";
        // this returns the models
        private async Task GetPrices(string barcode)
        {
            musicMagpiePrice = await _musicMagpieService.GetPrice(barcode);
            ziffitPrice = await _ziffitService.GetPrice(barcode);
            sellItBackPrice = await _sellItBackService.GetPrice(barcode);
        }

        public string ReadBarcode(RoutedEventArgs e)
        {
            barcode = BarcodeInput.Text;
            return barcode;
        }

        private async void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            BarcodeValue.Text = BarcodeInput.Text;
            string barcode = ReadBarcode(e);

            await GetPrices(barcode);

            AssignPrices();
        }

        private void AssignPrices()
        {
            if (musicMagpiePrice != null)
            {
                MMPrice.Text = musicMagpiePrice.Price.ToString();
            } else
            {
                MMPrice.Text = " - - ";
            }
            if (sellItBackPrice != null)
            {
                SIBPrice.Text = sellItBackPrice.Price.ToString();
                BookTitle.Text = sellItBackPrice.Title;
            } else
            {
                SIBPrice.Text = " - - ";
            }
            if (ziffitPrice != null)
            {
                ZFPrice.Text = ziffitPrice.Value.Offer.ToString();
            }
            else
            {
                ZFPrice.Text = " - - ";
            }
                
        }

    }
}
