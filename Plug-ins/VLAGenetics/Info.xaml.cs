using System.Windows;
using VLAControlLib;

namespace VLAGenetics
{
    /// <summary>
    /// Interaction logic for Info.xaml
    /// </summary>
    public partial class Info
    {
        public Info()
        {
            InitializeComponent();
        }

        private void BackToMainMenuClick(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Main());
        }
    }
}
