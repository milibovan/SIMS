﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Applications.UseCases;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.WPF.ViewModels
{
    public class VouchersViewModel : ViewModelBase
    {
        private readonly VoucherService voucherService;
        public ObservableCollection<Voucher> Vouchers { get; set; }

        public VouchersViewModel(User user)
        {
            voucherService = new VoucherService();
            Vouchers = new ObservableCollection<Voucher>(voucherService.GetAllForUser(user.Id));
        }
    }
}
