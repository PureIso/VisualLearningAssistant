using System.Windows;
using VLAControlLib;

namespace VLAGenetics
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
            //Switch the page to the AnimationDisplay Page
            Switcher.Switch(new AnimationDisplay(), EntryPoint.HostDefaultPage);
        }

        private void AboutButtonClick(object sender, RoutedEventArgs e)
        {
            //Switch the page to the Info Page
            //Switcher.Switch(new Info(), EntryPoint.HostDefaultPage);
        }
    }
}
