using System;
using System.Collections.Generic;
using System.Threading;

namespace VLAGenetics.Logic
{
    /// <summary>
    /// This is the class of a local search algorithm (Genetic Algorithm) that mimics evolution.
    /// Encodes possible solutions and combines them based on a fitness function to produce a fitter individuals.
    /// </summary>
    public class Genetics
    {
        #region variables
        public List<Chromosome> CurrentGeneration;
        public List<Individual> IndividualPairs;
        public List<Chromosome> CrossoverPair;
        public List<Chromosome> MutatedPair;
        private const int CrossoverProbability = 50;
        private const int MutationProbability = 25;
        private readonly int _population;
        private readonly Chromosome _fitness;
        #endregion

        #region Constructor
        /// <summary>
        /// Generate random initial population
        /// </summary>
        /// <param name="population"></param>
        /// <param name="fitness"></param>
        public Genetics(int population, Chromosome fitness)
        {
            //Set population and fitness data
            _population = population;
            _fitness = fitness;

            //Create random chromosome for our population
            CurrentGeneration = new List<Chromosome>(population);
            for(int i = 0; i < _population; i++)
            {
                Chromosome chromosome = new Chromosome();
                chromosome.Fitness = CalculateFitness(chromosome, _fitness);
                CurrentGeneration.Add(chromosome);
            }
        }
        #endregion

        #region public functions
        /// <summary>
        /// During each successive generation, a proportion of the existing population is selected to breed a new generation
        /// </summary>
        /// <returns></returns>
        public void Selection()
        {
            //Sort current generation is accordance of fitness.
            CurrentGeneration.Sort();
            CurrentGeneration.Reverse();
            Chromosome[] initialgen = CurrentGeneration.ToArray();

            //Initializing new individual with 2 chromosomes.
            IndividualPairs = new List<Individual>(_population);
            Random random = new Random(Convert.ToInt32(DateTime.Now.Ticks %Int16.MaxValue));

            for(int i = 0; i < _population; i++)
            {
                Individual individual = new Individual
                {
                    ParentOne = initialgen[random.Next(0, _population - 1)],
                    ParentTwo = initialgen[random.Next(0, _population - 1)]
                };
                IndividualPairs.Add(individual);
                Thread.Sleep(1);
            }
        }

        /// <summary>
        /// crossover is a genetic operator used to vary the programming of a chromosome or chromosomes from one generation to the next
        /// This is using the roulette wheel selection:P
        /// The individual is selected on the basis of fitness. 
        /// The probability of an individual to be selected increases with 
        /// the fitness of the individual greater or less than its competitor's fitness.
        /// </summary>
        public void Crossover()
        {
            //Initialize new list of crossover pair
            CrossoverPair = new List<Chromosome>();
            int probability = CrossoverProbability;
            Random random = new Random(Convert.ToInt32(DateTime.Now.Ticks %Int16.MaxValue));

            //Sort so fitter have better probability
            foreach (Individual individual in IndividualPairs)
            {
                bool crossover = (random.Next(0, 100) < probability);
                if (crossover)
                {
                    CrossoverPair.Add(GenerateChild(individual));
                }
                else
                {
                    CrossoverPair.Add(individual.ParentOne);
                    CrossoverPair.Add(individual.ParentTwo);
                }
                //Probability reduces by 2 percent the weaker the chromosome
                probability -= 5;
            }
            if (CrossoverPair.Count <= 10) return;
            //Remove everything over the population limit
            CrossoverPair.RemoveRange(_population, CrossoverPair.Count - _population);
        }

        /// <summary>
        /// Mutation is a genetic operator used to maintain genetic diversity from one generation 
        /// of a population of genetic algorithm chromosomes to the next.
        /// </summary>
        public void Mutation()
        {
            //Initialize new mutated pair
            MutatedPair = new List<Chromosome>(_population);
            Random random = new Random(Convert.ToInt32(DateTime.Now.Ticks %Int16.MaxValue));

            foreach (Chromosome chromosome in CrossoverPair)
            {
                //Randomize mutation
                bool mutation = (((random.Next() % 100)) < MutationProbability);
                if (mutation)
                {
                    string newTraint = "";
                    //Randomize mutation trait
                    foreach (char trait in chromosome.StringBinaryEncoding)
                    {
                        newTraint += trait == '1' ? 0: 1 ;
                    }

                    Chromosome newChrom = new Chromosome(newTraint);
                    //Calculate the fitness of the new chromosome
                    newChrom.Fitness = CalculateFitness(newChrom, _fitness);
                    MutatedPair.Add(newChrom);
                }
                else
                {
                    MutatedPair.Add(chromosome);
                }
                CurrentGeneration = MutatedPair;
                Thread.Sleep(1);
            }
        }
        #endregion

        #region private functions
        /// <summary>
        /// Random crossover point will be used to mix genetic information
        /// </summary>
        /// <param name="pair"></param>
        /// <returns></returns>
        private Chromosome GenerateChild(Individual pair)
        {
            byte[] child = new byte[pair.ParentOne.ByteBinaryEncoding.Length];
            byte[] parentOne = pair.ParentOne.ByteBinaryEncoding;
            byte[] parentTwo = pair.ParentTwo.ByteBinaryEncoding;

            for (int i = 0; i < parentOne.Length; i++)
            {
                //The more of the dominant chromosome get selected
                child[i] = (byte)(parentOne[i] | parentTwo[i]);
            }

            Chromosome newChrom = new Chromosome(child);
            newChrom.Fitness = CalculateFitness(newChrom, _fitness);
            return newChrom;
        }

        /// <summary>
        /// Calculate the fitness of a chromosome with the fittest
        /// </summary>
        /// <param name="pair"></param>
        /// <param name="fitness"></param>
        /// <returns></returns>
        private static int CalculateFitness(Chromosome pair, Chromosome fitness)
        {
            int fitnessPoint = 0;
            for(int i = 0;i < fitness.ByteBinaryEncoding.Length;i++)
            {
                if (pair.ByteBinaryEncoding[i] == fitness.ByteBinaryEncoding[i] &&
                    fitness.ByteBinaryEncoding[i] == 1)
                    fitnessPoint++;
            }
            return fitnessPoint;
        }
        #endregion
    }
}
