using Neptunite.Configuration;
using Neptunite.Generate;

namespace Neptunite.Mutation;
internal static class Population
{
  public static Pop[] CopyThenMutateChildrenPopulation(in Pop[] parents, in ParameterSchema parameter)
  {
    Pop[] children = new Pop[parameter.PopulationSize];
    for (int i = 0; i < parameter.PopulationSize; i++)
    {
      // Copy primitive structs =>> ensures deep copy
      int[] t1Chromosome = parents[i].T1Chromosome;
      int[] t2Chromosome = parents[i].T2Chromosome;
      sbyte[][][] convolutionChromosome = parents[i].ConvolutionChromosome;

      // Mutate
      Chromosome.MutateTypeOneChromosomeInplace(ref t1Chromosome, in parameter);
      Chromosome.MutateTypeTwoChromosomeInplace(ref t2Chromosome, in parameter);
      Chromosome.MutateConvolutionChromosomeInplace(ref convolutionChromosome, in parameter);

      // Constuct new object
      children[i] = new Pop(t1Chromosome, t2Chromosome, convolutionChromosome);
    }

    return children;
  }
}
