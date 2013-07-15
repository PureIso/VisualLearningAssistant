
using System;
using System.Threading;
using System.Windows.Media;
using System.Windows.Threading;
using VLAGenetics.Logic;
using VLAPluginLib;

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
        private Chromosome fitness;

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GeneticTextBox.Clear();
            fitness = new Chromosome(
                Encoding.Dimples.Ignore,
                Encoding.Handedness.RightHanded,
                Encoding.Freckles.Ignore,
                Encoding.Hair.Ignore,
                Encoding.Height.Tall,
                Encoding.Eyes.Ignore,
                Encoding.BloodType.Ignore,
                Encoding.AbilityOne.Speed,
                Encoding.AbilityTwo.Strength,
                Encoding.AbilityThree.ThunderStrike) {Fitness = 5};

            gen = new Genetics(10, fitness);
            GeneticTextBox.Clear();
            HT.Clear();
            HT2.Clear();

            GeneticTextBox.Text = "Fitness:" ;
            GeneticTextBox.Text += "===============================================:" ;
            GeneticTextBox.Text += fitness.StringTraitEncoding ;
            GeneticTextBox.Text += "===============================================:" ;
            GeneticTextBox.Text += "Number: "+fitness.Fitness ;
            GeneticTextBox.Text += "===============================================:" ;
            foreach (Chromosome individual in gen.CurrentGeneration)
            {
                GeneticTextBox.Text += individual.StringTraitEncoding + " Fitness: " + individual.Fitness
                    +Environment.NewLine;
            }
            GeneticTextBox.Text += "===============================================:" ;


            Thread ot = new Thread(It);
            ot.Start();
        }

        private void It()
        {
            int count = 999;
            int goal = 0;
            int overall = 0;
            for (int index = 0; index <= count; index++)
            {
                if (goal == 30) break;
                goal = 0;
                gen.Selection();
                

                SetText("Match: "+index +" Highest Fitness: ");
                SetText( "===============================================:"+gen.IndividualPairs.Count );
                foreach (Individual individual in gen.IndividualPairs)
                {
                    SetText("------------------------------------------" );
                    SetText( individual.ParentOne.StringBinaryEncoding + " :Match: " + individual.ParentTwo.StringBinaryEncoding
                        );
                    SetText( individual.ParentOne.Fitness + " :Match: " + individual.ParentTwo.Fitness
                        );
                    SetText( "------------------------------------------" );
                }
                SetText( "===============================================:" );

                gen.Crossover();
                SetText("Before Mutation : " + index );
                SetText("===============================================:" + gen.CrossoverPair.Count +Environment.NewLine);
                foreach (Chromosome individual in gen.CrossoverPair)
                {
                    SetText( individual.StringBinaryEncoding );
                }
                SetText("===============================================:" );

                gen.Mutation();
                SetText("After Mutation : " + index );
                SetText("===============================================:" + gen.MutatedPair.Count );
                int iz = 0;
                
                foreach (Chromosome individual in gen.MutatedPair)
                {
                    string star = "";
                    if (individual.Fitness == fitness.Fitness)
                    {
                        star += "***";
                        goal++;
                    }

                    if (gen.MutatedPair[iz].StringBinaryEncoding == gen.CrossoverPair[iz].StringBinaryEncoding)
                        star += " No Mute";
                    SetText(individual.StringBinaryEncoding + " " + star);

                    iz++;
                }
                SetText("===============================================:");

                SetText2(gen.currentHighestFitness + " :: " + index);

                if (gen.currentHighestFitness > overall)
                {
                    overall = gen.currentHighestFitness;
                    SetText3(overall.ToString());
                }
            }


        }

        private void SetText(string text)
        {
            try
            {
                if (GeneticTextBox.Dispatcher.CheckAccess())
                {
                    GeneticTextBox.Text += text + Environment.NewLine;
                }
                else
                    GeneticTextBox.Dispatcher.Invoke(DispatcherPriority.Normal, new SetText(SetText), text);
            }
            catch (Exception e)
            {
                EntryPoint.HostLogger(e.Message, LogType.Error);
                throw new Exception(e.Message);
            }
        }

        private void SetText2(string text)
        {
            try
            {
                if (GeneticTextBox.Dispatcher.CheckAccess())
                {
                    HT.Text = text + Environment.NewLine;
                }
                else
                    GeneticTextBox.Dispatcher.Invoke(DispatcherPriority.Normal, new SetText(SetText2), text);
            }
            catch (Exception e)
            {
                EntryPoint.HostLogger(e.Message, LogType.Error);
                throw new Exception(e.Message);
            }
        }

        private void SetText3(string text)
        {
            try
            {
                if (GeneticTextBox.Dispatcher.CheckAccess())
                {
                    HT2.Text = text + Environment.NewLine;
                }
                else
                    GeneticTextBox.Dispatcher.Invoke(DispatcherPriority.Normal, new SetText(SetText3), text);
            }
            catch (Exception e)
            {
                EntryPoint.HostLogger(e.Message, LogType.Error);
                throw new Exception(e.Message);
            }
        }

    }
}
