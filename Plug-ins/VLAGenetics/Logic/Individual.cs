using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VLAGenetics.Logic
{
    public class Individual:IComparable<Individual>
    {
        public Chromosome ParentOne;
        public Chromosome ParentTwo;
        public int TotalFitness;

        public int CompareTo(Individual other)
        {
            return TotalFitness.CompareTo(other.TotalFitness);
        }
    }
}
