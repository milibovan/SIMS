﻿using System.Windows;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.ViewModels.Owner;

namespace Tourist_Project.WPF.Views.Owner
{
    /// <summary>
    /// Interaction logic for CreateAccommodation.xaml
    /// </summary>
    public partial class CreateAccommodation : Window
    {
        public CreateAccommodation(User user)
        {
            InitializeComponent();
            DataContext = new CreateAccommodationViewModel(user, this);
        }
    }
}
