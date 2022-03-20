using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookAppUI.Commands;
using BookAppUI.Stores;

namespace BookAppUI.ViewModels
{
    public class EndSessionViewModel: ViewModelBase
    {
        public ICommand NavigateHomeCommand { get; }

        public EndSessionViewModel(NavigationStore navigationStore)
        {
            NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
        }
    }
}
