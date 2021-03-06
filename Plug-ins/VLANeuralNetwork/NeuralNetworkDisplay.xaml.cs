﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VLAControlLib;
using VLANeuralNetwork.Logic;

namespace VLANeuralNetwork
{
    /// <summary>
    /// Interaction logic for NeuralNetworkDisplay.xaml
    /// </summary>
    public partial class NeuralNetworkDisplay : UserControl
    {
        public NeuralNetworkDisplay()
        {
            InitializeComponent();
        }

        private void BackToMainMenuClick(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Main());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NeuralNetwork a = new NeuralNetwork(2);
        }
    }
}
