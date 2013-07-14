
using System;
using VLAGenetics.Logic;

namespace VLAGenetics
{
    /// <summary>
    /// Interaction logic for AnimationDisplay.xaml
    /// </summary>
    public partial class AnimationDisplay
    {
        public AnimationDisplay()
        {
            InitializeComponent();
        }

        private Genetics gen;
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GeneticTextBox.Clear();
            Chromosome fitness = new Chromosome(
                Encoding.Dimples.Ignore,
                Encoding.Handedness.LeftHanded,
                Encoding.Freckles.Ignore,
                Encoding.Hair.NaturallyCurlyHair,
                Encoding.Height.Short,
                Encoding.Eyes.Ignore,
                Encoding.BloodType.Ignore,
                Encoding.AbilityOne.Speed,
                Encoding.AbilityTwo.Strength,
                Encoding.AbilityThree.ThunderStrike);

            gen = new Genetics(10, fitness);
            GeneticTextBox.Text = "Fitness:" + Environment.NewLine;
            GeneticTextBox.Text += "===============================================:" + Environment.NewLine;
            GeneticTextBox.Text += fitness.StringEncoding+Environment.NewLine;
            GeneticTextBox.Text += "===============================================:" + Environment.NewLine;
            foreach (Chromosome individual in gen.CurrentGeneration)
            {
                GeneticTextBox.Text += individual.StringEncoding+" Fitness: "+individual.Fitness
                    +Environment.NewLine;
            }
            GeneticTextBox.Text += "===============================================:" + Environment.NewLine;


            gen.Selection();
            GeneticTextBox.Text += "Match:" + Environment.NewLine;
            GeneticTextBox.Text += "===============================================:" + Environment.NewLine;
            foreach (Genetics.Individuals individual in gen.IndividualsPairs)
            {
                GeneticTextBox.Text += "------------------------------------------" + Environment.NewLine;
                GeneticTextBox.Text += individual.ParentOne.StringEncodingOneAndZeros + " :Match: " + individual.ParentTwo.StringEncodingOneAndZeros
                    + Environment.NewLine;
                GeneticTextBox.Text += individual.ParentOne.Fitness + " :Match: " + individual.ParentTwo.Fitness
                    + Environment.NewLine;
                GeneticTextBox.Text += "------------------------------------------" + Environment.NewLine;
            }
            GeneticTextBox.Text += "===============================================:" + Environment.NewLine;

            gen.Crossover();
            GeneticTextBox.Text += "Before Mutation :" + Environment.NewLine;
            GeneticTextBox.Text += "===============================================:" + Environment.NewLine;
            foreach (Chromosome individual in gen.CrossoverPair)
            {
                GeneticTextBox.Text += individual.StringEncodingOneAndZeros + Environment.NewLine;
            }
            GeneticTextBox.Text += "===============================================:" + Environment.NewLine;
        }

    }
}
