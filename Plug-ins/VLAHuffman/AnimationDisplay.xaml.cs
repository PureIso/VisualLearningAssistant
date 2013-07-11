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
// //	Solution Name:	VLAHuffman	
// //	File Name:		AnimationDisplay.xaml.cs
// //	Created:		14-04-2013 Time: 18:47
// //	Last Edited:	16-04-2013 Time: 20:45
// //=======================================================================//
// //=======================================================================//

#endregion

using System.Windows;
using VLAControlLib;
using VLAHuffman.Logic;

namespace VLAHuffman
{
    /// <summary>
    ///     Interaction logic for AnimationDisplay.xaml
    /// </summary>
    public partial class AnimationDisplay
    {
        private HuffmanTree _ht;

        public AnimationDisplay()
        {
            InitializeComponent();
        }

        private void BuildHuffmanTree_Click(object sender, RoutedEventArgs e)
        {
            _ht = new HuffmanTree();
            _ht.Build(BuildTreeTextBox.Text);
            MainCanvas.Children.Clear();
            _ht.DrawTree(MainCanvas, 5, 5, 1);
        }

        private void NewTreeButton_Click(object sender, RoutedEventArgs e)
        {
            BuildHuffmanTreeButton.IsEnabled = true;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            //Switch the page to the Info Page
            Switcher.Switch(new Main(), EntryPoint.HostDefaultPage);
        }
    }
}