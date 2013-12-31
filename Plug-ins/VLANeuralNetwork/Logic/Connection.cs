using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VLANeuralNetwork.Logic
{
    public struct Connection
    {
        public Neuron Neuron;
        public double Weight;

        public Connection(Neuron neuron, double weight)
        {
            Neuron = neuron;
            Weight = weight;
            //neuron.Connections.Add(this);
        }
    }
}
