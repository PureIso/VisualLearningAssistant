﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VLANeuralNetwork.Logic
{
    public class Neuron
    {
        public List<Connection> Connections { get; set; }
        public double Value { get; set; }
        public string Name { get; set; }

        public Neuron(string name)
        {
            Connections = new List<Connection>();
            Name = name;
        }

        public Neuron(string name, double value)
        {
            Connections = new List<Connection>();
            Name = name;
            Value = value;
        }
    }
}
