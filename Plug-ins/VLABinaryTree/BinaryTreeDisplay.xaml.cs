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
// //	Solution Name:	VLABinaryTree	
// //	File Name:		BinaryTreeDisplay.xaml.cs
// //	Created:		16-04-2013 Time: 20:33
// //	Last Edited:	16-04-2013 Time: 20:38
// //=======================================================================//
// //=======================================================================//

#endregion

using System;
using System.Windows;
using VLABinaryTree.Logic;
using VLAControlLib;

namespace VLABinaryTree
{
    /// <summary>
    ///     Interaction logic for AnimationDisplay.xaml
    /// </summary>
    public partial class BinaryTreeDisplay
    {
        private BinaryTree _binaryTree;

        public BinaryTreeDisplay()
        {
            InitializeComponent();
        }

        private void BuildHuffmanTree_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int value;
                if (int.TryParse(ValueTextBox.Text, out value) == false)
                    throw new Exception("Please enter a valid Integer.");
                _binaryTree.Insert(value);
                MainGrid.Children.Clear();
                _binaryTree.DrawTree(MainGrid, 5, 5, TimeSpan.FromSeconds(.5));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "VLA Binary Tree", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NewTreeButton_Click(object sender, RoutedEventArgs e)
        {
            InsertButton.IsEnabled = true;
            _binaryTree = new BinaryTree();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            //Switch the page to the Info Page
            Switcher.Switch(new Main(), EntryPoint.HostDefaultPage);
        }
    }
}