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
// //	Created:		06-04-2013 Time: 00:09
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

#region

using System;
using System.Windows;
using System.Windows.Threading;
using VLAControlLib;
using VLALinkedList.Core;
using VLAPluginLib;

#endregion

namespace VLALinkedList
{
    /// <summary>
    ///     Interaction logic for AnimationDisplay.xaml
    /// </summary>
    public partial class AnimationDisplay
    {
        private const double MaxDrawHeight = 236;
        private LinkedList _mainLinkedList;

        public AnimationDisplay()
        {
            InitializeComponent();
        }

        #region Event Handlers

        private void BackToMainMenuClick(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Main());
        }

        private void NewLinkedListButtonClick(object sender, RoutedEventArgs e)
        {
            FixScroll();
            EntryPoint.HostLogger("New Linked List Instance Ready.", LogType.Info);
            _mainLinkedList = new LinkedList(50, 100, DisplayCanvas, TimeSpan.FromSeconds(1));
            _mainLinkedList.SetText += SetText;
            InformationTextBox.Clear();
            InformationTextBox.Text += "New Linked List Instance Created - Null" + Environment.NewLine;
            InsertLinkedListButton.IsEnabled = true;
            LinkedListValueTextBox.Text = "";
            LinkedListValueTextBox.IsEnabled = true;
        }

        private void InsertLinkedListButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _mainLinkedList.Add(LinkedListValueTextBox.Text);
            }
            catch (Exception ex)
            {
                EntryPoint.HostLogger(ex.Message, LogType.Error);
                MessageBox.Show(ex.Message, "VLA Linked List", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            FixScroll();
        }

        private void SuperGridSizeChanged(object sender, SizeChangedEventArgs e)
        {
            FixScroll();
        }

        #endregion

        private void FixScroll()
        {
            if (_mainLinkedList == null) return;
            if (_mainLinkedList.Count == 0) return;

            if (DisplayCanvas.ActualHeight < MaxDrawHeight)
                DisplayCanvas.Height = MaxDrawHeight;
            if (DisplayCanvas.ActualWidth < _mainLinkedList.DrawWidth)
                DisplayCanvas.Width = _mainLinkedList.DrawWidth;
        }

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
                throw new Exception(e.Message);
            }
        }
    }
}