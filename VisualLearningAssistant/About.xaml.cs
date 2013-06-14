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
// //	File Name:		About.xaml.cs
// //	Created:		06-04-2013 Time: 00:11
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

#region Reference

using System;
using System.Reflection;
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
    ///     Interaction logic for About.xaml
    /// </summary>
    public partial class About
    {
        /// <summary>
        ///     About Constructor
        /// </summary>
        public About(MainWindow main)
        {
            InitializeComponent();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("The aim of Visual Learning Assistant (VLA) is to aid learning.  " +
                          "Visual Learning Assistant hopes to contribute to the development " +
                          "of the education system through the use of technology, to open the " +
                          "possibility of seeing lots of possible answers to a question, lots " +
                          "of possible ways of interpreting a question and to think laterally.  " +
                          "We all process information differently either visually, aurally or physically.");
            InfoRichTextBox.AppendText(sb.ToString());

            TextBlockVersion.Text += " " + Assembly.GetExecutingAssembly().GetName().Version;
            switch (main.CurrentTheme)
            {
                case "Default":
                    SetBrush("#FF7272C2", "Brush2Trans", "Brush2");
                    break;
                case "Light":
                    SetBrush("#FF77CBD6", "Brush4", "Brush4Thick");
                    break;
            }
        }

        #region Handler

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DragMove(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        #endregion

        #region Functions

        /// <summary>
        ///     Set the brush for the about page
        /// </summary>
        /// <param name="brush">The brush aka colour</param>
        /// <param name="background">The background brush</param>
        /// <param name="buttonBackground">The button background colour</param>
        private void SetBrush(string brush, string background, string buttonBackground)
        {
            try
            {
                object convertFromString = ColorConverter.ConvertFromString(brush);
                if (MainBorder.Dispatcher.CheckAccess())
                {
                    if (convertFromString != null)
                    {
                        MainBorder.BorderBrush = new SolidColorBrush((Color) convertFromString);
                    }
                    MainGrid.Background = (Brush) FindResource(background);
                    closeButton.Background = (Brush) FindResource(buttonBackground);
                }
                else
                    MainBorder.Dispatcher.Invoke(DispatcherPriority.Normal, new SetBrushDelegate(SetBrush),
                                                 brush, background, buttonBackground);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion
    }
}