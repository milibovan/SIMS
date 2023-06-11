﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tourist_Project.WPF.Commands;
using Tourist_Project.WPF.Stores;

namespace Tourist_Project.WPF.ViewModels
{
    class RequestComplexTourHelpViewModel : ViewModelBase
    {
        public ICommand BackCommand { get; set; }
        public RequestComplexTourHelpViewModel(NavigationStore navigationStore, RequestComplexTourViewModel previousViewModel)
        {
            BackCommand = new NavigateCommand<RequestComplexTourViewModel>(navigationStore, () => previousViewModel);
        }
    }
}
