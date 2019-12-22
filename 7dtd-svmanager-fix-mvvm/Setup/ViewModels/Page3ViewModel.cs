﻿using _7dtd_svmanager_fix_mvvm.Setup.Models;
using _7dtd_svmanager_fix_mvvm.Setup.Views;
using CommonStyleLib.ViewModels;
using Prism.Commands;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Windows.Input;

namespace _7dtd_svmanager_fix_mvvm.Setup.ViewModels
{
    public class Page3ViewModel
    {
        Page3 page;
        Page3Model model;
        public Page3ViewModel(Page3 page, Page3Model model)
        {
            this.page = page;
            this.model = model;

            GetPathBtClick = new DelegateCommand(GetPathBt_Click);
            AutoSearchBtClick = new DelegateCommand(AutoSearchBt_Click);

            ServerConfigPathText = model.ToReactivePropertyAsSynchronized(m => m.ServerConfigPathText);
        }

        public ReactiveProperty<string> ServerConfigPathText { get; set; }

        public ICommand GetPathBtClick { get; set; }
        public ICommand AutoSearchBtClick { get; set; }

        public void GetPathBt_Click()
        {
            model.SelectAndGetFilePath();
        }
        public void AutoSearchBt_Click()
        {
            model.AutoSearchAndGetFilePath();
        }
    }
}
