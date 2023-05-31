﻿using System.Windows;
using Tourist_Project.WPF.ViewModels.Owner;

namespace Tourist_Project.WPF.Views.Owner
{
    /// <summary>
    /// Interaction logic for Forums.xaml
    /// </summary>
    public partial class Forums : Window
    {
        public Forums()
        {
            InitializeComponent();
            DataContext = new ShowForumsViewModel();
        }
    }
}
