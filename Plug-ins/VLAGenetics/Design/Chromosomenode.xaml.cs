using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace VLAGenetics.Design
{
    /// <summary>
    /// Interaction logic for ChromosomeNode.xaml
    /// </summary>
    public partial class Chromosomenode
    {
        public Chromosomenode()
        {
            InitializeComponent();
        }

        private void FillTrait(int index, int section)
        {
            switch (index)
            {
                case 0:
                    Trait1.Fill = section == 1 ? Brushes.Green : Brushes.Red;
                    break;
                case 1:
                    Trait2.Fill = section == 1 ? Brushes.Green : Brushes.Red;
                    break;
                case 2:
                    Trait3.Fill = section == 1 ? Brushes.Green : Brushes.Red;
                    break;
                case 3:
                    Trait4.Fill = section == 1 ? Brushes.Green : Brushes.Red;
                    break;
                case 4:
                    Trait5.Fill = section == 1 ? Brushes.Green : Brushes.Red;
                    break;
                case 5:
                    Trait6.Fill = section == 1 ? Brushes.Green : Brushes.Red;
                    break;
                case 6:
                    Trait7.Fill = section == 1 ? Brushes.Green : Brushes.Red;
                    break;
                case 7:
                    Trait8.Fill = section == 1 ? Brushes.Green : Brushes.Red;
                    break;
                case 8:
                    Trait9.Fill = section == 1 ? Brushes.Green : Brushes.Red;
                    break;
                case 9:
                    Trait10.Fill = section == 1 ? Brushes.Green : Brushes.Red;
                    break;
            }
        }


        public void DrawNode(Canvas canvas, int xaxis, int yaxis, byte[] binary)
        {

            for (int i = 0; i < binary.Length; i++)
            {
                FillTrait(i, binary[i]);
            }
            Margin = new Thickness(xaxis, yaxis, 0, 0);

            Canvas.SetLeft(this, xaxis); // Set x position
            Canvas.SetTop(this, yaxis); // Set y position

            canvas.Children.Add(this);
        }
    }
}
