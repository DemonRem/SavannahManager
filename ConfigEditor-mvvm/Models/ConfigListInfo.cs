﻿using Prism.Mvvm;
using System;

namespace ConfigEditor_mvvm.Models
{
    /// <summary>
    /// Information for Confit List.
    /// </summary>
    public class ConfigListInfo : BindableBase, ICloneable
    {
        public string Property { get; set; }
        private string value;
        public string Value
        {
            get => value;
            set => SetProperty(ref this.value, value);
        }
        public string[] Selection { get; set; }
        public ConfigType Type { get; set; }
        public string Description { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
