using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using MattEland.ML.TimeAndSpace.Core;

namespace MattEland.ML.TimeAndSpace
{
    public class EpisodeScoreFitness : IFitness
    {
        public double Evaluate(IChromosome chromosome)
        {
            return 0;
        }
    }

    public class EpisodeTraitsChromosome : ChromosomeBase
    {
        public EpisodeTraitsChromosome() : base(length: 1)
        {
        }

        public override Gene GenerateGene(int geneIndex)
        {
            throw new NotImplementedException();
        }

        public override IChromosome CreateNew()
        {
            return new EpisodeTraitsChromosome();
        }

        public Episode BuildEpisode()
        {
            return new Episode();
        }
    }

    public class GeneticEpisodeSolver
    {
        public Episode Optimize(DoctorWhoRegressionExperiment experiment)
        {
            var population = new Population(50, 100, adamChromosome: new EpisodeTraitsChromosome());
            var crossover = new UniformCrossover(0.5f);
            var selection = new EliteSelection();
            var mutation = new FlipBitMutation();
            var fitness = new EpisodeScoreFitness();

            var ga = new GeneticAlgorithm(population: population,
                                          fitness: fitness,
                                          selection: selection,
                                          crossover: crossover,
                                          mutation: mutation);

            var termination = new FitnessStagnationTermination(25);
            ga.Termination = termination;

            ga.Start();

            while (ga.IsRunning)
            {

            }

            EpisodeTraitsChromosome best = (EpisodeTraitsChromosome) ga.BestChromosome;

            return best.BuildEpisode();
        }
    }
}
