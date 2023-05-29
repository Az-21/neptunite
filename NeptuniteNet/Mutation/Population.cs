using Neptunite.Configuration;
using Neptunite.Generate;

namespace Neptunite.Mutation;
internal static class Population
{
  public static void MutateTopFiftyAndReplaceBottomFiftyInplace(ref Pop[] parents, in ParameterSchema parameter)
  {
    for (int i = parameter.PopulationSize - 1; i < parameter.PopulationSize; i++)
    {
      // Copy primitive structs =>> ensures deep copy
      int[] t1Chromosome = parents[i - parameter.PopulationSize + 1].T1Chromosome;
      int[] t2Chromosome = parents[i - parameter.PopulationSize + 1].T2Chromosome;
      Image.Matrix[] convolutionChromosome = parents[i - parameter.PopulationSize + 1].ConvolutionChromosome;

      // Mutate
      Chromosome.MutateTypeOneChromosomeInplace(ref t1Chromosome, in parameter);
      Chromosome.MutateTypeTwoChromosomeInplace(ref t2Chromosome, in parameter);
      Chromosome.MutateConvolutionChromosomeInplace(ref convolutionChromosome, in parameter);

      // Replace bottom 50% of population
      parents[i] = new Pop(t1Chromosome, t2Chromosome, convolutionChromosome);
    }
  }
}
