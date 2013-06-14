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
// //	File Name:		AnimationDisplay.xaml.cs
// //	Created:		06-04-2013 Time: 00:08
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

#region

using System;
using System.Windows;
using System.Windows.Threading;
using VLAControlLib;
using VLAPluginLib;
using Array = VLAArrays.Core.Array;

#endregion

namespace VLAArrays
{
    /// <summary>
    ///     Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class AnimationDisplay
    {
        private Array _array;

        public AnimationDisplay()
        {
            InitializeComponent();
        }

        #region Event Handler

        private void BackToMainMenuClick(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Main());
        }

        private void NewArrayButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                EntryPoint.HostLogger("New Array Instance Ready.", LogType.Info);
                InformationTextBox.Clear();
                DisplayCanvas.Children.Clear();
                ArrayValuesTextBox.IsEnabled = true;
                AllocateMemoryButton.IsEnabled = true;
                BuildArrayButton.IsEnabled = false;
                FixScroll();
            }
            catch (Exception ex)
            {
                EntryPoint.HostLogger(ex.Message, LogType.Error);
                MessageBox.Show(ex.Message, "VLA Array", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AllocateMemoryButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ArrayValuesTextBox.Text == "")
                    throw new Exception("No values!" + Environment.NewLine +
                                        "Please Enter a string value into the set value textbox");
                string[] words = ArrayValuesTextBox.Text.Split(',');

                SetText("New FIXED memory allocation.");
                DisplayCanvas.Children.Clear();
                _array = new Array();
                _array.SetText += SetText;

                _array.Add(words);
                _array.AnimateMemory(DisplayCanvas);
                BuildArrayButton.IsEnabled = true;
                FixScroll();
            }
            catch (Exception ex)
            {
                EntryPoint.HostLogger(ex.Message, LogType.Error);
                MessageBox.Show(ex.Message, "VLA Arrays", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BuildArrayButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                AllocateMemoryButton.IsEnabled = false;
                DisplayCanvas.Children.Clear();
                _array.AnimateMemory(DisplayCanvas);
                _array.Animate(DisplayCanvas);
            }
            catch (Exception ex)
            {
                EntryPoint.HostLogger(ex.Message, LogType.Error);
                MessageBox.Show(ex.Message, "VLA Arrays", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GridSizeChanged(object sender, SizeChangedEventArgs e)
        {
            FixScroll();
        }

        #endregion

        #region Function

        private void SetText(string text)
        {
            try
            {
                if (InformationTextBox.Dispatcher.CheckAccess())
                {
                    InformationTextBox.Text += text + Environment.NewLine;
                }
                else
                    InformationTextBox.Dispatcher.Invoke(DispatcherPriority.Normal, new SetText(SetText), text);
            }
            catch (Exception e)
            {
                EntryPoint.HostLogger(e.Message, LogType.Error);
                throw new Exception(e.Message);
            }
        }

        private void FixScroll()
        {
            if (_array == null) return;
            if (_array.Count == 0) return;

            if (DisplayCanvas.ActualHeight < _array.DrawHeight)
                DisplayCanvas.Height = _array.DrawHeight;
            if (DisplayCanvas.ActualWidth < _array.DrawWidth)
                DisplayCanvas.Width = _array.DrawWidth;
        }

        #endregion
    }
}