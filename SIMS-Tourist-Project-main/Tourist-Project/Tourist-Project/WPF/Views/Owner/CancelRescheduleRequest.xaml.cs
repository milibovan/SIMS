﻿using System.Windows;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.ViewModels.Owner;

namespace Tourist_Project.WPF.Views.Owner
{
    /// <summary>
    /// Interaction logic for CancelRescheduleRequest.xaml
    /// </summary>
    public partial class CancelRescheduleRequest : Window
    {
        public CancelRescheduleRequest(OwnerMainWindowViewModel ownerMainWindowViewModel, ReschedulingReservationViewModel reschedulingReservationViewModel)
        {
            InitializeComponent();
            DataContext = new CancelRescheduleViewModel(this, ownerMainWindowViewModel, reschedulingReservationViewModel);
        }
    }
}
