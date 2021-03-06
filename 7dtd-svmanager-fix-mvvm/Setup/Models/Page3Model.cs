﻿using CommonStyleLib.File;
using Microsoft.Win32;
using Prism.Mvvm;
using SvManagerLibrary.SteamLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonStyleLib.ExMessageBox;

namespace _7dtd_svmanager_fix_mvvm.Setup.Models
{
    public class Page3Model : BindableBase
    {
        public event CanChangedEventHandler CanChenged;
        private void OnCanChenged(object sender, bool canChanged)
        {
            CanChenged?.Invoke(sender, new CanChangedEventArgs(canChanged));
        }

        public Page3Model(InitializeData initializeData)
        {
            this.initializeData = initializeData;

            ServerConfigPathText = initializeData.ServerConfigFilePath;
        }

        #region PropertiesForViewModel
        private string serverConfigPathText = string.Empty;
        public string ServerConfigPathText
        {
            get => serverConfigPathText;
            set
            {
                SetProperty(ref serverConfigPathText, value);
                initializeData.ServerConfigFilePath = value;
                bool canChanged = false;
                if (!string.IsNullOrEmpty(value)) canChanged = true;
                OnCanChenged(this, canChanged);
            }
        }
        #endregion

        InitializeData initializeData;

        public void SelectAndGetFilePath()
        {
            string filter = LangResources.SetupResource.Filter_XmlFile;
            string directoryPath = ConstantValues.DefaultDirectoryPath;
            string filename = FileSelector.GetFilePath(directoryPath, filter, "serverconfig.xml", FileSelector.FileSelectorType.Read);

            if (!string.IsNullOrEmpty(filename))
                ServerConfigPathText = filename;
        }
        public void AutoSearchAndGetFilePath()
        {
            string steamPath = string.Empty;
            using (var rKey = Registry.CurrentUser.OpenSubKey(ConstantValues.RegSteamPath))
            {
                if (rKey == null)
                {
                    ExMessageBoxBase.Show(LangResources.SetupResource.UI_SteamNotInstalled, "", ExMessageBoxBase.MessageType.Exclamation);
                    return;
                }
                steamPath = (string)rKey.GetValue(ConstantValues.RegSteamKey);
            }

            string filename = GetFileName(steamPath);

            if (!string.IsNullOrEmpty(filename))
            {
                ServerConfigPathText = filename;
            }
        }

        private string GetFileName(string steamPath)
        {
            string filename = GetFileName(steamPath, ConstantValues.ServerClientPath, ConstantValues.ServerConfigName);

            if (string.IsNullOrEmpty(filename))
                filename = GetFileName(steamPath, ConstantValues.GameClientPath, ConstantValues.ServerConfigName);

            return filename;
        }
        private string GetFileName(string steamPath, string target, string name)
        {
            string filename = GetSvPath(steamPath + target, name);

            if (string.IsNullOrEmpty(filename))
            {
                try
                {
                    var slLoader = new SteamLibraryLoader(steamPath + ConstantValues.SteamLibraryPath);
                    var dirPaths = slLoader.SteamLibraryPathList;
                    foreach (SteamLibraryPath dirPath in dirPaths)
                        filename = GetSvPath(dirPath.SteamDirPath + target, name);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return filename;
        }
        private string GetSvPath(string dirPath, string exeName)
        {
            string filename = string.Empty;
            if (Directory.Exists(dirPath))
            {
                var fi = new FileInfo(dirPath + @"\" + exeName);
                if (fi.Exists)
                    filename = fi.FullName;
            }
            return filename;
        }
    }
}
