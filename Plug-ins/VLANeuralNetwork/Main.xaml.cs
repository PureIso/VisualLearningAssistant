using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace VLANeuralNetwork
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main 
    {
        public Main()
        {
            InitializeComponent();
        }

        private void StartButtonClick(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new NeuralNetworkDisplay(), EntryPoint.HostDefaultPage);
        }

        private void AboutButtonClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
