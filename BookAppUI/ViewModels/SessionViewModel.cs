using BookAppUI.Commands;
using BookAppUI.Models;
using BookAppUI.Service;
using BookAppUI.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookAppUI.ViewModels
{
    public class SessionViewModel: ViewModelBase
    {
        public string StartSessionMessage => "Session Started you go Beth";
        public string barcode = "9780099448822";

        private readonly SellItBackService _sellItBackService;
        private readonly ZiffitService _ziffitService;
        private readonly MusicMagpieService _musicMagpieService;
        public string PriceMM => "£0.20";
        public string PriceZF => "£0.50";
        public string PriceSIB => "£0.30";

        public ICommand NavigateHomeCommand { get; }
        public ICommand NavigateEndSessionCommand { get; }
        public PriceModel musicMagpiePrice { get; set; }
        public PriceModel ziffitPrice { get; set; }
        public PriceModel sellItBackPrice { get; set; }

        public SessionViewModel(NavigationStore navigationStore)
        {
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
            NavigateEndSessionCommand = new NavigateCommand<EndSessionViewModel>(navigationStore, () => new EndSessionViewModel(navigationStore));
            //_sellItBackService = sellItBackService;
            //_ziffitService = ziffitService;
            //_musicMagpieService = musicMagpieService;

            //new MusicMagpieService().GetPrice(barcode).ContinueWith((task) =>
            //{
            //    var musicMagpiePrice = task.Result;
            //});
        }

        //private async Task GetPrices()
        //{
        //    musicMagpiePrice = await _musicMagpieService.GetPrice(barcode);
        //    ziffitPrice = await _ziffitService.GetPrice(barcode);
        //    sellItBackPrice = await _sellItBackService.GetPrice(barcode);
        //    new ZiffitService().GetPrice(barcode).ContinueWith((task) =>
        //    {
        //        var musicMagpiePrice = task.Result;
        //    });
        //}



    }
}
