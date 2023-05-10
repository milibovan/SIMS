﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.Stores;

namespace Tourist_Project.WPF.ViewModels
{
    public class NotificationsViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel => notificationNavigationStore.CurrentViewModel;
        private readonly NavigationStore notificationNavigationStore;
        private readonly NavigationStore mainNavigationStore;
        private readonly NotificationGuestTwoService notificationService = new();
        public ObservableCollection<NotificationGuestTwo> Notifications { get; set; }

        private NotificationGuestTwo selectedNotificationGuestTwo;
        public NotificationGuestTwo SelectedNotification
        {
            get => selectedNotificationGuestTwo;
            set
            {
                if (value != selectedNotificationGuestTwo)
                {
                    notificationNavigationStore.CurrentViewModel = value.NotificationType switch
                    {
                        NotificationType.ConfirmPresence => new ConfirmPresenceViewModel(),
                        _ => notificationNavigationStore.CurrentViewModel
                    };
                }
            }
        }

        public NotificationsViewModel(User user, NavigationStore navigationStore)
        {
            mainNavigationStore = navigationStore;

            Notifications = new ObservableCollection<NotificationGuestTwo>(notificationService.GetAllForUser(user.Id));
            notificationNavigationStore = new NavigationStore();

            notificationNavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
