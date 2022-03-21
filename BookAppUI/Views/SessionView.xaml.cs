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
        public string PriceMM => "£0.20";
        public string PriceZF => "£0.50";
        public string PriceSIB => "£0.30";

        public ICommand NavigateHomeCommand { get; }
        public ICommand NavigateEndSessionCommand { get; }
        public PriceModel musicMagpiePrice { get; set; }
        public ZiffitModel ziffitPrice { get; set; }
        public SellItBackModel sellItBackPrice { get; set; }


        private string barcode = "9780099448822";

        // this returns the models
        private async Task GetPrices()
        {
            musicMagpiePrice = await _musicMagpieService.GetPrice(barcode);
            ziffitPrice = await _ziffitService.GetPrice(barcode);
            sellItBackPrice = await _sellItBackService.GetPrice(barcode);
        }


        private async void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            await GetPrices();

            Console.WriteLine(ziffitPrice);
            Console.WriteLine(musicMagpiePrice);
            Console.WriteLine(sellItBackPrice);
        }
    }
}
