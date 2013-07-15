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
        public List<Chromosome> CurrentGeneration;
        public List<Individual> IndividualPairs;
        public List<Chromosome> CrossoverPair;
        public List<Chromosome> MutatedPair;
        private const int CrossoverProbability = 50;
        private const int MutationProbability = 25;
        public int Population;
        private Chromosome _fitness;
        public int currentHighestFitness;

        /// <summary>
        /// Generate random initial population
        /// </summary>
        /// <param name="population"></param>
        /// <param name="fitness"></param>
        public Genetics(int population, Chromosome fitness)
        {
            Population = population;
            _fitness = fitness;

            //Create random chromosome for our population
            CurrentGeneration = new List<Chromosome>(population);
            for (int i = 0; i < population; i++)
            {
                Chromosome chromosome = new Chromosome();
                chromosome.Fitness = CalculateFitness(chromosome, _fitness);
                CurrentGeneration.Add(chromosome);
            }
        }






        /// <summary>
        /// During each successive generation, a proportion of the existing population is selected to breed a new generation
        /// </summary>
        /// <returns></returns>
        public void Selection()
        {
            Individual individual = new Individual();

            IndividualPairs = new List<Individual>(Population);
            CurrentGeneration.Sort();
            CurrentGeneration.Reverse();
            int chances = 1;

            foreach (Chromosome chrome in CurrentGeneration)
            {
                Random random = new Random();

                //The least fit will be dropped
                int value1 = random.Next(0, chances);
                if (chances == Population) chances-=2;
                int value2 = random.Next(0, chances+1);
                individual.ParentOne = CurrentGeneration.ToArray()[value1];
                individual.ParentTwo = CurrentGeneration.ToArray()[value2];
                individual.TotalFitness = individual.ParentOne.Fitness + individual.ParentTwo.Fitness;
                IndividualPairs.Add(individual);
                chances++;
            }
        }

        public void Crossover()
        {
            //Store results of new List of Chromosomes after cross over
            CrossoverPair = new List<Chromosome>(Population);

            //Sort so fiter have better properbility

            foreach (Individual individual in IndividualPairs)
            {
                
                //Initialise new offspring from the individual generated from selection
                var r = new Random(Convert.ToInt32(DateTime.Now.Ticks % Int16.MaxValue));
                bool crossover = (((r.Next() % 100) + 1) < CrossoverProbability);

                if (crossover)
                {
                    Chromosome offspring = GenerateChild(individual);
                    CrossoverPair.Add(offspring);
                }
                else
                {
                    CrossoverPair.Add(individual.ParentOne);
                    CrossoverPair.Add(individual.ParentTwo);
                }
            }
            if
            (CrossoverPair.Count > 10)
            {
                CrossoverPair.RemoveRange(Population, CrossoverPair.Count - Population);
            }
        }

        public void Mutation()
        {
            MutatedPair = new List<Chromosome>(Population);
            
            foreach (Chromosome chromosome in CrossoverPair)
            {
                Random random = new Random();
                
                bool mutation = random.Next(0, 100) < MutationProbability;
                if (mutation)
                {
                    Chromosome newChromosome = new Chromosome();
                    string newTraint = "";
                    foreach (char trait in chromosome.StringBinaryEncoding)
                    {
                        newTraint += trait == '1' ? 0 : 1;
                    }
                    newChromosome.StringBinaryEncoding = newTraint;
                    MutatedPair.Add(newChromosome);
                }
                else
                {
                    MutatedPair.Add(chromosome);
                }
            }

            //======================
            MutatedPair.Sort();
            MutatedPair.Reverse();
            int count = 0;
            foreach (var chromosome in MutatedPair)
            {
                if (chromosome.Fitness == MutatedPair[0].Fitness)
                    count++;
            }
            currentHighestFitness = MutatedPair[0].Fitness * count;
        }


        #region functions
        /// <summary>
        /// Random crossover point will be used to mix genetic information
        /// </summary>
        /// <param name="pair"></param>
        /// <returns></returns>
        private Chromosome GenerateChild(Individual pair)
        {
            byte[] parentOne = Helper.StringToBinayByte(pair.ParentOne.StringBinaryEncoding);
            byte[] parentTwo = Helper.StringToBinayByte(pair.ParentTwo.StringBinaryEncoding);

            Chromosome chromosome = new Chromosome();
            for (int i = 0; i < parentOne.Length; i++)
            {
                parentOne[i] = (byte)(parentOne[i] | parentTwo[i]);
            }
            chromosome.StringBinaryEncoding = Helper.BinaryByteToString(parentOne);
            return chromosome;
        }

        public static int CalculateFitness(Chromosome pair, Chromosome fitness)
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
        #endregion
    }
}
