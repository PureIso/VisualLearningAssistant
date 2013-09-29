using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VLANeuralNetwork.Logic
{
    public class NeuralNetwork
    {
        //Constructor
        public NeuralNetwork(int inputNodesCount,int outputNodesCount )
        {
            //Given set of inputs
            //Random connection weights
            //Desired output/s
            //Using random weight get an initial output
            //Compare to the desired outputs
            //Desired output - Calculated Output = Difference (Error in networks)
            //IE
            //35 - 14 = 21
            //45 - 29 = 16
            //17 - 41 = -24
            //Adjust the connection weight to produce smaller errors
            //http://www.youtube.com/watch?v=DG5-UyRBQD4
        }

        //How neural networks learn
        public void BackPropagation()
        {
            
        }

        private void InitializeRandomWeights()
        {
            
        }
    }
}
