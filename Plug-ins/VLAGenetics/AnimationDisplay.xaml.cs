
using System;
using System.Threading;
using System.Windows.Controls;
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

        private Genetics _genetics;
        private int _maxGeneration;
        private int _maxPopulation;
        private int _targetPopulation;
        private int _targetFitnessPoint;
        private Chromosome _fitness;
        private bool _stop;

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            StopButton.IsEnabled = true;

            Thread worker = new Thread(Train);
            worker.Start();
        }

        private void Train()
        {
            int overall = 0;
            for (int index = 0; index <= _maxGeneration; index++)
            {
                Thread.Sleep(1);
                if (_stop) break;

                _genetics.Selection();
                _genetics.Crossover();
                _genetics.Mutation();

                SetControlText(GeneticTextBox, "======================");
                SetControlText(GeneticTextBox, "Generation: ( " + index + " ) Post Mutation");
                SetControlText(GeneticTextBox, "======================");

                int currentFitnessPoint = 0;
                for (int i = 0; i < _genetics.MutatedPair.Count; i++)
                {
                    string results = "";
                    Chromosome chromosome = _genetics.MutatedPair[i];

                    if (chromosome.Fitness == _fitness.Fitness)
                    {
                        currentFitnessPoint += _fitness.Fitness;
                        results += " *";
                    }
                    if (chromosome.StringBinaryEncoding == _genetics.CrossoverPair[i].StringBinaryEncoding)
                        results += " No Mutation";

                    SetControlText(GeneticTextBox, chromosome.StringBinaryEncoding
                                                   + " " + chromosome.Fitness + results);
                }

                ClearText(CurrentFitnessPointTextBox);
                SetControlText(CurrentFitnessPointTextBox, currentFitnessPoint + " :: " + index);

                if (currentFitnessPoint <= overall)
                    continue;

                overall = currentFitnessPoint;
                ClearText(CurrentHighestFitnessPointTextBox);
                SetControlText(CurrentHighestFitnessPointTextBox, overall.ToString());
                if (currentFitnessPoint >= _targetFitnessPoint)
                    break;
            }
        }

        private void ClearText(TextBox textbox)
        {
            try
            {
                if (textbox.Dispatcher.CheckAccess())
                {
                    textbox.Clear();
                }
                else
                    textbox.Dispatcher.Invoke(DispatcherPriority.Normal, new ClearControlText(ClearText), textbox);
            }
            catch (Exception e)
            {
                EntryPoint.HostLogger(e.Message, LogType.Error);
                throw new Exception(e.Message);
            }
        }

        private void SetControlText(TextBox textbox,string text)
        {
            try
            {
                if (textbox.Dispatcher.CheckAccess())
                {
                    if (text.Contains("*"))
                    {
                        int currentLength = textbox.Text.Length;
                        textbox.Text += text + Environment.NewLine;
                        textbox.Select(currentLength, text.Length);
                        textbox.SelectionBrush = Brushes.Green;
                    }
                    else
                        textbox.Text += text + Environment.NewLine;
                }
                else
                    textbox.Dispatcher.Invoke(DispatcherPriority.Normal, new SetControlText(SetControlText), textbox, text);
            }
            catch (Exception e)
            {
                EntryPoint.HostLogger(e.Message, LogType.Error);
                throw new Exception(e.Message);
            }
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            _stop = true;
        }

        private void InitButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GeneticTextBox.Clear();
            GeneticTextBox.Clear();
            FitnessTextBox.Clear();
            BinaryCodeTextBox.Clear();
            CurrentHighestFitnessPointTextBox.Clear();
            CurrentFitnessPointTextBox.Clear();

            string stringBinaryValue = "";
            stringBinaryValue += FlightCheckBox.IsChecked == true ? 1 : 0;
            stringBinaryValue += StrengthCheckBox.IsChecked == true ? 1 : 0;
            stringBinaryValue += SpeedCheckBox.IsChecked == true ? 1 : 0;
            stringBinaryValue += InvisibilityCheckBox.IsChecked == true ? 1 : 0;
            stringBinaryValue += TeleportationCheckBox.IsChecked == true ? 1 : 0;
            stringBinaryValue += IntelligenceCheckBox.IsChecked == true ? 1 : 0;
            stringBinaryValue += MindReadingCheckBox.IsChecked == true ? 1 : 0;
            stringBinaryValue += TelekinesisCheckBox.IsChecked == true ? 1 : 0;
            stringBinaryValue += HealingCheckBox.IsChecked == true ? 1 : 0;
            stringBinaryValue += ShapeShiftingCheckBox.IsChecked == true ? 1 : 0;

            _fitness = new Chromosome(stringBinaryValue);
            _fitness.SetFitnessBasedBinary();

            _maxPopulation = int.Parse(MaxPopulationTextBox.Text);
            _maxGeneration = int.Parse(MaxGenerationTextBox.Text);
            _targetPopulation = int.Parse(TargetPopulation.Text);
            _targetFitnessPoint = _targetPopulation*_fitness.Fitness;
            TargetFitnessPoint.Text = _targetFitnessPoint.ToString();

            _genetics = new Genetics(_maxPopulation, _fitness);

            SetControlText(GeneticTextBox,"======================");
            SetControlText(GeneticTextBox, "Initial Generation");
            SetControlText(GeneticTextBox, "======================");

            foreach (Chromosome chromosome in _genetics.CurrentGeneration)
            {
                SetControlText(GeneticTextBox,chromosome.StringBinaryEncoding +
                    " Fitness: " + chromosome.Fitness);
            }

            SetControlText(FitnessTextBox, _fitness.Fitness.ToString());
            SetControlText(BinaryCodeTextBox, stringBinaryValue);
            StartButton.IsEnabled = true;
        }

    }
}
