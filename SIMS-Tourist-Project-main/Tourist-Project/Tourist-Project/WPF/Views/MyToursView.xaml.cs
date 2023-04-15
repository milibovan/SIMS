﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tourist_Project.Domain.Models;
using Tourist_Project.WPF.ViewModels;

namespace Tourist_Project.WPF.Views
{
    /// <summary>
    /// Interaction logic for MyToursView.xaml
    /// </summary>
    public partial class MyToursView : Window
    {
        public MyToursView(User user)
        {
            InitializeComponent();
            DataContext = new MyToursViewModel(user);
        }
    }
}
