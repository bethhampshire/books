﻿using BookAppUI.Commands;
using BookAppUI.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookAppUI.ViewModels
{
    public class HomeViewModel: ViewModelBase
    {
        public string WelcomeMessage => "Welcome Beth";

        public ICommand NavigateStartSessionCommand { get; }

        public HomeViewModel(NavigationStore navigationStore)
        {
            NavigateStartSessionCommand = new NavigateCommand<SessionViewModel>(navigationStore, () => new SessionViewModel(navigationStore));
        }
    }
}