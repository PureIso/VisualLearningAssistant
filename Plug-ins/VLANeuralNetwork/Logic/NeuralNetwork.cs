using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VLANeuralNetwork.Logic
{
    public class NeuralNetwork
    {
        private static readonly Random _random = new Random();
        //Constructor
        public NeuralNetwork(int inputNodesCount)
        {
            List<Neuron> InputNeurons = new List<Neuron>();
            List<Neuron> HiddenNeurons = new List<Neuron>();
            
            Neuron OutputNeuron = new Neuron("Output_" + 1);
            //Example 
            //i1 = 0
            //i2 = 1
            //b1 = 1
            //w1=0.5
            //w2=0.6
            //w3=0.7
            for (int n = 0; n < inputNodesCount; n++)
            {
                //Add Name and Value between 0 and 1
                //InputNeurons.Add(new Neuron("Input_" + n, double.Parse(_random.NextDouble().ToString ("#.##"))));
            }

            
            InputNeurons.Add(new Neuron("Input_" + 1, 0 ));
            InputNeurons.Add(new Neuron("Input_" + 2, 1 ));

            //Add Input
            for (int i = 0; i < InputNeurons.Count; i++)
            {
                HiddenNeurons.Add(GetOutputNeuron(InputNeurons));//Bias is also applied to the calculations
            }

            OutputNeuron = GetOutputNeuron(HiddenNeurons);
        }

        //How neural networks learn
        public void BackPropagation()
        {
            
        }

        public Neuron GetOutputNeuron(List<Neuron> inputNeurons)
        {
            //Neuron neuron = new Neuron("");
            Neuron outputNeuron = new Neuron("Output");
            Neuron biasNeuron = new Neuron("Bias_" + 1, 1);

            //foreach (Neuron neuron in inputNeurons)
            //{
            //    neuron.Connections.Add(new Connection(hiddenNeuron, double.Parse(_random.NextDouble().ToString("#.##"))));
            //}
            //biasNeuron.Connections.Add(new Connection(hiddenNeuron, double.Parse(_random.NextDouble().ToString("#.##"))));

            //==========Demo
            outputNeuron.Connections.Add(new Connection(inputNeurons[0], 0.5));
            outputNeuron.Connections.Add(new Connection(inputNeurons[1], 0.6));
            outputNeuron.Connections.Add(new Connection(biasNeuron, 0.7));
            //==============

            //Calculate output value================================================================
            //e~ = Natural base
            //e = 2.71828182846
            //if a = 1.3 using S(sigmoid function) = 0.79
            //Activation Function - Sigmoid  f(x) = 1/1+e~-x (Only positive) & Hyperbolic Tangent e~2x-1/e~2x+1  (Positive and Negative)
            double x = (double)decimal.Round((decimal)ActivationFunction(outputNeuron, biasNeuron), 2);
            double y = 1 / (1 + Math.Pow(Math.E, -x));
            outputNeuron.Value = (double)decimal.Round((decimal)y, 2);
            return outputNeuron;
        }

        public double ActivationFunction(List<Neuron> inputNeurons, Neuron biasNeuron)
        {
            //I1*W1 + I2*W2 + B1*W3
            double returnValue = 0;
            foreach (Neuron inputNeuron in inputNeurons)
            {
                foreach (Connection neuronConnection in inputNeuron.Connections)
                {
                    returnValue += inputNeuron.Value * neuronConnection.Weight;
                }
            }

            foreach (Connection neuronConnection in biasNeuron.Connections)
            {
                returnValue += biasNeuron.Value * neuronConnection.Weight;
            }
            return returnValue;
        }

        public double ActivationFunction(Neuron inputNeuron, Neuron biasNeuron)
        {
            //I1*W1 + I2*W2 + B1*W3
            double returnValue = 0;
            foreach (Connection neuronConnection in inputNeuron.Connections)
            {
                returnValue += neuronConnection.Neuron.Value * neuronConnection.Weight;
            }
            return returnValue;
        }

        private void InitializeRandomWeights()
        {
            
        }
    }
}
