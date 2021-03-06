﻿using _7dtd_svmanager_fix_mvvm.Setup.Models;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using _7dtd_svmanager_fix_mvvm.Setup.ViewModels;

namespace _7dtd_svmanager_fix_mvvm.Setup.Views
{
    /// <summary>
    /// Page2.xaml の相互作用ロジック
    /// </summary>
    public partial class Page2 : UserControl
    {
        public Page2(NavigationWindowService<InitializeData> service)
        {
            InitializeComponent();

            var model = new Page2Model(service.Share);
            model.CanChenged += Model_CanChenged;
            DataContext = new ViewModels.Page2ViewModel(service, model);
        }

        private void Model_CanChenged(object sender, CanChangedEventArgs e)
        {
            OnCanChenged(this, e.CanChanged);
        }

        public event CanChangedEventHandler CanChenged;
        public void OnCanChenged(object sender, bool canChanged)
        {
            CanChenged?.Invoke(sender, new CanChangedEventArgs(canChanged));
        }
    }
}
