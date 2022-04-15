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
        public ICommand NavigateHomeCommand { get; }
        public ICommand NavigateEndSessionCommand { get; }

        public SessionViewModel(NavigationStore navigationStore)
        {
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
        }
    }
}
