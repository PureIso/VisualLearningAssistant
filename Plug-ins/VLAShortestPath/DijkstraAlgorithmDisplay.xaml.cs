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
// //	File Name:		DijkstraAlgorithmDisplay.xaml.cs
// //	Created:		06-04-2013 Time: 00:10
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

#region

using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.VisualBasic;
using VLAControlLib;
using VLAShortestPath.Core_Logic;

#endregion

namespace VLAShortestPath
{
    /// <summary>
    ///     Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class DijkstraAlgorithmDisplay
    {
        internal DijkstraAlgorithm DijkstraAlgorithm;

        public DijkstraAlgorithmDisplay()
        {
            InitializeComponent();
        }

        private void CanvasMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (DijkstraAlgorithm.Count >= DijkstraAlgorithm.Vertices.Capacity) return;
            double x = e.GetPosition(mainCanvas).X;
            double y = e.GetPosition(mainCanvas).Y;
            string input = Interaction.InputBox("Enter Vertex Name", "Enter Vertex Info", "A");
            if (input == "") return;
            if (DijkstraAlgorithm == null) return;
            DijkstraAlgorithm.Insert(mainCanvas, input, x, y);
            fromVertexComboBox.Items.Add(input);
            toVertexComboBox.Items.Add(input);
            fromVertexSPComboBox.Items.Add(input);
            toVertexSPComboBox.Items.Add(input);
        }

        private void AmountOfVertexButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                uint number;
                if (!uint.TryParse(amountOfVertexTextBox.Text, out number))
                    throw new Exception("Invalid Value");
                DijkstraAlgorithm = new DijkstraAlgorithm(number);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void SetConnectionButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                uint number;
                if (!uint.TryParse(weightTextBox.Text, out number))
                    throw new Exception("Invalid Value"); //making sure weight is an unsigned integer
                if (fromVertexComboBox.SelectedItem.ToString() == toVertexComboBox.SelectedItem.ToString())
                    throw new Exception("Cannot connect same vertex.");
                DijkstraAlgorithm.Connect(mainCanvas, fromVertexComboBox.SelectedItem.ToString(),
                                          toVertexComboBox.SelectedItem.ToString(), number);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void FindShortestPathButtonClick(object sender, RoutedEventArgs e)
        {
            DijkstraAlgorithm.ReDraw(mainCanvas);
            if (fromVertexSPComboBox.SelectedItem.ToString() == toVertexSPComboBox.SelectedItem.ToString())
                throw new Exception("Cannot find same vertex.");
            DijkstraAlgorithm.FindShortestPath(mainCanvas, fromVertexSPComboBox.SelectedItem.ToString(),
                                               toVertexSPComboBox.SelectedItem.ToString());
        }

        private void NewAlgorithmsButtonClick(object sender, RoutedEventArgs e)
        {
            DijkstraAlgorithm = null;
            amountOfVertexTextBox.Text = "";
            mainCanvas.Children.Clear();
            fromVertexComboBox.Items.Clear();
            toVertexComboBox.Items.Clear();
            fromVertexSPComboBox.Items.Clear();
            toVertexSPComboBox.Items.Clear();
            EnableUtil();
        }

        private void BackToMainMenuClick(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Main());
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveMenu saveMenu = new SaveMenu(this);
            saveMenu.ShowDialog();
        }

        internal void EnableUtil()
        {
            amountOfVertexTextBox.IsEnabled = true;
            amountOfVertexButton.IsEnabled = true;
            ConnectGroupBox.IsEnabled = true;
            FindGroupBox.IsEnabled = true;
        }
    }
}