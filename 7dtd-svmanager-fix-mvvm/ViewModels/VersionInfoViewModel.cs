﻿using CommonLib.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Models;
using System.Windows;
using _7dtd_svmanager_fix_mvvm.Models;
using System.Windows.Input;
using Prism.Commands;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace _7dtd_svmanager_fix_mvvm.ViewModels
{
    public class VersionInfoViewModel : ViewModelBase
    {
        VersionInfoModel model;
        public VersionInfoViewModel(Window view, VersionInfoModel model) : base(view, model)
        {
            this.model = model;

            Loaded = new DelegateCommand(Window_Loaded);
            DonateBtClicked = new DelegateCommand<string>(DonateBt_Clicked);

            VersionLabel = model.ObserveProperty(m => m.Version).ToReactiveProperty();
        }

        #region Properties
        public ReactiveProperty<string> VersionLabel { get; set; }
        #endregion

        #region EventProperties
        public ICommand Loaded { get; set; }
        public ICommand DonateBtClicked { get; set; }
        #endregion

        #region EventMethods
        public void Window_Loaded()
        {
            model.SetVersion();
        }
        public void DonateBt_Clicked(string e)
        {
            model.SetAddressToClipboard(e);
        }
        #endregion
    }
}
