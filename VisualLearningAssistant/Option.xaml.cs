#region License and File Info

// 
// Copyright (C) 2013  Olawale Egbeyemi
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>
// 
// //========================================================================//
// //========================================================================//
// //	Author:			Olawale Egbeyemi
// //	Solution Name:	VisualLearningAssistant	
// //	File Name:		Option.xaml.cs
// //	Created:		06-04-2013 Time: 00:11
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

#region

using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using VLAPluginLib;

#endregion

namespace VisualLearningAssistant
{
    /// <summary>
    ///     Interaction logic for Option.xaml
    /// </summary>
    public partial class Option
    {
        private readonly MainWindow _main;


        /// <summary>
        ///     Option Constructor
        /// </summary>
        /// <param name="main">The MainWindow instance</param>
        public Option(MainWindow main)
        {
            _main = main;
            InitializeComponent();

            _main.SetLog("Option menu displayed.", LogType.Info);
            switch (main.CurrentTheme)
            {
                case "Default":
                    ThemeComboBox.Text = "Default";
                    SetBrush("#FF7272C2", "Brush2Trans", "Brush1");
                    break;
                case "Light":
                    ThemeComboBox.Text = "Light";
                    SetBrush("#FF77CBD6", "Brush4", "Brush4Flat");
                    break;
            }
        }

        #region Handler

        private void SaveOptionButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string selectedTheme = ThemeComboBox.Text;

                switch (selectedTheme)
                {
                    case "Default":
                        _main.SetLog("Default Theme Selected.", LogType.Info);
                        _main.CurrentTheme = "Default";
                        _main.SetBrush("#FF7272C2", "Brush2Trans", "Brush2", "Brush1");
                        SetBrush("#FF7272C2", "Brush2Trans", "Brush2");
                        break;
                    case "Light":
                        _main.SetLog("Light Theme Selected.", LogType.Info);
                        _main.CurrentTheme = "Light";
                        _main.SetBrush("#FF77CBD6", "Brush4", "Brush4Thick", "Brush4Flat");
                        SetBrush("#FF77CBD6", "Brush4", "Brush4Thick");
                        break;
                }

                _main.SetLog("Config.ini file saved", LogType.Info);
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "config.ini";
                if (!(File.Exists(filePath)))
                {
                    _main.SetLog("Config.ini File Missing!", LogType.Info);
                    _main.SetLog("Config.ini File Created!", LogType.Info);
                    {
                        FileStream str = File.Create(filePath);
                        str.Close();
                    }
                }

                //Save
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("#Theme#");
                stringBuilder.AppendLine(selectedTheme);

                string line = stringBuilder.ToString();
                using (StreamWriter file = new StreamWriter(filePath))
                {
                    file.Write(line);
                }
                Close();
            }
            catch (Exception ex)
            {
                _main.SetLog(ex.Message, LogType.Error);
                MessageBoxEvent(ex.Message, "Visual Learning Assistant", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ApplyOptionButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string selectedTheme = ThemeComboBox.Text;

                switch (selectedTheme)
                {
                    case "Default":
                        _main.SetLog("Default Theme Selected.", LogType.Info);
                        _main.CurrentTheme = "Default";
                        _main.SetBrush("#FF7272C2", "Brush2Trans", "Brush2", "Brush1");
                        SetBrush("#FF7272C2", "Brush2Trans", "Brush2");
                        break;
                    case "Light":
                        _main.SetLog("Light Theme Selected.", LogType.Info);
                        _main.CurrentTheme = "Light";
                        _main.SetBrush("#FF77CBD6", "Brush4", "Brush4Thick", "Brush4Flat");
                        SetBrush("#FF77CBD6", "Brush4", "Brush4Thick");
                        break;
                }

                _main.SetLog("Config.ini file saved", LogType.Info);
                if (Functions.CheckAndCreateFile("config.ini")) return;
                _main.SetLog("Config.ini File Missing!", LogType.Info);
                _main.SetLog("Config.ini File Created!", LogType.Info);
            }
            catch (Exception ex)
            {
                _main.SetLog(ex.Message, LogType.Error);
                MessageBoxEvent(ex.Message, "Visual Learning Assistant", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DragMove(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        #endregion

        #region Function

        private void SetBrush(string brush, string background, string buttonBackground)
        {
            try
            {
                object convertFromString = ColorConverter.ConvertFromString(brush);
                if (OptionBorder.Dispatcher.CheckAccess())
                {
                    //Border
                    if (convertFromString != null)
                    {
                        Color color = (Color) convertFromString;
                        OptionBorder.BorderBrush = new SolidColorBrush(color);
                    }
                    //Main
                    Brush br = (Brush) FindResource(background);
                    MainOptionGrid.Background = br;

                    br = (Brush) FindResource(buttonBackground);
                    ApplyOptionButton.Background = br;
                    saveOptionButton.Background = br;
                }
                else
                    OptionBorder.Dispatcher.Invoke(DispatcherPriority.Normal, new SetBrushDelegate(SetBrush),
                                                   brush, background, buttonBackground);
            }
            catch (Exception e)
            {
                _main.SetLog(e.Message, LogType.Error);
                MessageBoxEvent(e.Message, "Visual Learning Assistant", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        /// <summary>
        ///     The event handler for the MessageBoxDelegate pointer
        /// </summary>
        internal event MessageBoxDelegate MessageBoxEvent;
    }
}