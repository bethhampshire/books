using BookAppUI.Commands;
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

        public ICommand NavigateHomeCommand { get; }

        public SessionViewModel(NavigationStore navigationStore)
        {
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
        }
    }
}
