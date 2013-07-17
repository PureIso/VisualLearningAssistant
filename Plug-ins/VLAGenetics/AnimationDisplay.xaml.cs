
using System;
using System.Threading;
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

        private Genetics _gen;
        private Chromosome _fitness;
        private bool _stop;

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GeneticTextBox.Clear();
            _fitness = new Chromosome("0000000001") {Fitness = 1};

            _gen = new Genetics(10, _fitness);
            GeneticTextBox.Clear();
            HT.Clear();
            HT2.Clear();

            GeneticTextBox.Text = "Fitness:" + Environment.NewLine + "Number: " + _fitness.Fitness;
            GeneticTextBox.Text += "===================================:" +Environment.NewLine;
            GeneticTextBox.Text += _fitness.StringBinaryEncoding ;
            GeneticTextBox.Text += "===================================:" + Environment.NewLine;

            foreach (Chromosome individual in _gen.InitialGeneration)
            {
                GeneticTextBox.Text += individual.StringBinaryEncoding + " Fitness: " + individual.Fitness
                    +Environment.NewLine;
            }
            GeneticTextBox.Text += "===================================:" + Environment.NewLine;

            Thread ot = new Thread(It);
            ot.Start();
        }

        private void It()
        {
            const int count = 999;
            int overall = 0;

            for (int index = 0; index <= count; index++)
            {
                if (_stop)break;

                //Select Pairs based on current generation
                _gen.Selection();
                _gen.Crossover();
                _gen.Mutation();

                SetText("New Generation ( "+index+" )After Mutation." );
                SetText("===================================:" + _gen.MutatedPair.Count );

                for(int i = 0; i < _gen.MutatedPair.Count; i++)
                {
                    Chromosome chromosome = _gen.MutatedPair[i];

                    string star = "";
                    if (chromosome.Fitness == _fitness.Fitness) star += "***";
                    if (chromosome.StringBinaryEncoding == _gen.CrossoverPair[i].StringBinaryEncoding)
                        star += " No Mute";

                    SetText(chromosome.StringBinaryEncoding + " " + star);
                }
                SetText("===============================================:");

                int currenthighest = _gen.CurrentTotal();
                SetText2(currenthighest + " :: " + index);

                if (currenthighest <= overall) 
                    continue;

                overall = currenthighest;
                SetText3(overall.ToString());
                if (currenthighest == _fitness.Fitness * _gen.InitialGeneration.Count)
                    break;
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

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            _stop = true;
        }

    }
}
