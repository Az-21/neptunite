using Neptunite.Configuration;

namespace Neptunite.Generate;
internal record class Pop(int[] T1Chromosome, int[] T2Chromosome, sbyte[][][] ConvolutionChromosome);
internal static class Population
{
  public static Pop[] GenerateInitialPopulation(in ParameterSchema parameter)
  {
    // Start with 2x pops so that 0th iteration of GP works correctly
    int initialPop = 2 * parameter.PopulationSize;

    Pop[] initialPopulation = new Pop[initialPop];
    for (int i = 0; i < initialPop; i++)
    {
      initialPopulation[i] = new Pop(
        Chromosome.RandomlyGenerateTypeOneChromosome(),
        Chromosome.RandomlyGenerateTypeTwoChromosome(),
        Chromosome.RandomlyGenerateConvolutionChromosome(in parameter)
        );
    }

    return initialPopulation;
  }
}
