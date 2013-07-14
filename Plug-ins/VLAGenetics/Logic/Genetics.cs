using System;
using System.Collections.Generic;

namespace VLAGenetics.Logic
{
    /// <summary>
    /// This is the class of a local search algorithm (Genetic Algorithm) that mimics evolution.
    /// Encodes possible solutions and combines them based on a fitness function to produce a fitter indiviuals.
    /// </summary>
    public class Genetics
    {
        public readonly List<Chromosome> CurrentGeneration;
        public List<Individuals> IndividualsPairs;
        public List<Chromosome> CrossoverPair;
        private const int CrossoverProbability = 50;
      
        /// <summary>
        /// This the population of candidate solution to 
        /// an optimization problem is evolved toward better solutions.
        /// </summary>
        public struct Individuals
        {
            public Chromosome ParentOne;
            public Chromosome ParentTwo;
        }

        public Genetics(int population, Chromosome fitness)
        {
            //Create random chromosome for our population
            CurrentGeneration = new List<Chromosome>(population);
            for (int i = 0; i < population; i++)
            {
                Chromosome chromosome = new Chromosome();
                chromosome.Fitness = CalculateFitness(chromosome, fitness);
                CurrentGeneration.Add(chromosome);
            }
        }

        /// <summary>
        /// During each successive generation, a proportion of the existing population is selected to breed a new generation
        /// </summary>
        /// <returns></returns>
        public void Selection()
        {
            Individuals individuals = new Individuals();
            IndividualsPairs = new List<Individuals>(CurrentGeneration.Count);
            CurrentGeneration.Sort();
            CurrentGeneration.Reverse();

            foreach (Chromosome _ in CurrentGeneration)
            {
                Random random = new Random();
                int value1 = 0;
                int value2 = 0;
                while (value1 == value2)
                {
                    value1 = random.Next(0, CurrentGeneration.Count - 1);
                    value2 = random.Next(0, CurrentGeneration.Count - 1);
                }
                individuals.ParentOne = CurrentGeneration.ToArray()[value1];
                individuals.ParentTwo = CurrentGeneration.ToArray()[value2];
                IndividualsPairs.Add(individuals);
            }
        }

        public void Crossover()
        {
            CrossoverPair = new List<Chromosome>(CurrentGeneration.Count);

            foreach (Individuals individuals in IndividualsPairs)
            {
                if(CrossoverPair.Count == IndividualsPairs.Capacity) continue;
                Individuals individual = new Individuals();
                Random random = new Random(Convert.ToInt32(DateTime.Now.Ticks % Int16.MaxValue));
                bool doCrossover = (((random.Next() % 100) + 1) < CrossoverProbability);
                if(doCrossover)
                {
                    individual.ParentOne = GenerateChild(individuals);
                    CrossoverPair.Add(individual.ParentOne);
                }
                else
                {
                    individual.ParentOne = individuals.ParentOne;
                    individual.ParentTwo = individuals.ParentTwo;
                    CrossoverPair.Add(individual.ParentOne);
                    CrossoverPair.Add(individual.ParentTwo);
                }
            }
        }


        private Chromosome GenerateChild(Individuals pair)
        {
            Byte[] parentOne = GetByteArrayFromString(pair.ParentOne.StringEncodingOneAndZeros);
            Byte[] parentTwo = GetByteArrayFromString(pair.ParentTwo.StringEncodingOneAndZeros);

            Chromosome chromosome = new Chromosome();
            for (int i = 0; i < parentOne.Length; i++)
            {
                parentOne[i] = (byte)(parentOne[i] | parentTwo[i]);
            }
            chromosome.StringEncodingOneAndZeros = GetStringFromByteArray(parentOne);
            //TODO parse StringEncodingOneAndZeros
            return chromosome;
        }

        private static byte[] GetByteArrayFromString(string stringEncoding)
        {
            List<byte> binaryEncoded = new List<byte>();
            foreach (Char bits in stringEncoding)
                binaryEncoded.Add(bits == '1' ? (byte)1 : (byte)0);

            return binaryEncoded.ToArray();
        }

        private static string GetStringFromByteArray(IEnumerable<byte> binaryEncoded)
        {
            string stringEncoding = string.Empty;
            foreach (byte bits in binaryEncoded)
                stringEncoding += bits;
            return stringEncoding;
        }

        private static int CalculateFitness(Chromosome pair, Chromosome fitness)
        {
            int fitnessPoint = 0;
            if (pair.Encoding.AbilityOne == fitness.Encoding.AbilityOne 
                && fitness.Encoding.AbilityOne != Encoding.AbilityOne.Ignore) fitnessPoint++;
            if (pair.Encoding.AbilityTwo == fitness.Encoding.AbilityTwo
                && fitness.Encoding.AbilityTwo != Encoding.AbilityTwo.Ignore) fitnessPoint++;
            if (pair.Encoding.AbilityThree == fitness.Encoding.AbilityThree
                && fitness.Encoding.AbilityThree != Encoding.AbilityThree.Ignore) fitnessPoint++;
            if (pair.Encoding.Dimples == fitness.Encoding.Dimples
                && fitness.Encoding.Dimples != Encoding.Dimples.Ignore) fitnessPoint++;
            if (pair.Encoding.BloodType == fitness.Encoding.BloodType
                && fitness.Encoding.BloodType != Encoding.BloodType.Ignore) fitnessPoint++;
            if (pair.Encoding.Freckles == fitness.Encoding.Freckles
                && fitness.Encoding.Freckles != Encoding.Freckles.Ignore) fitnessPoint++;
            if (pair.Encoding.Hair == fitness.Encoding.Hair
                && fitness.Encoding.Hair != Encoding.Hair.Ignore) fitnessPoint++;
            if (pair.Encoding.Handedness == fitness.Encoding.Handedness
                && fitness.Encoding.Handedness != Encoding.Handedness.Ignore) fitnessPoint++;
            if (pair.Encoding.Height == fitness.Encoding.Height
                && fitness.Encoding.Height != Encoding.Height.Ignore) fitnessPoint++;
            if (pair.Encoding.Eyes == fitness.Encoding.Eyes
                && fitness.Encoding.Eyes != Encoding.Eyes.Ignore) fitnessPoint++;
            return fitnessPoint;
        }
    }
}
