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

        public string PriceMM => "£0.20";
        public string PriceZF => "£0.50";
        public string PriceSIB => "£0.30";

        public ICommand NavigateHomeCommand { get; }
        public ICommand NavigateEndSessionCommand { get; }

        public SessionViewModel(NavigationStore navigationStore)
        {
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
            NavigateEndSessionCommand = new NavigateCommand<EndSessionViewModel>(navigationStore, () => new EndSessionViewModel(navigationStore));
        }
    }
}
