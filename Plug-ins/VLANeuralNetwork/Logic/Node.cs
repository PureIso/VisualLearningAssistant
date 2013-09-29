using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VLANeuralNetwork.Logic
{
    public class Node
    {
        public List<Connection> Connections { get; set; }
        public double Value { get; set; }
        public string Name { get; set; }
    }
}
