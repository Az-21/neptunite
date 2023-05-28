using Neptunite.Configuration;

namespace Neptunite.Generate;
internal record class Pop(int[] T1Chromosome, int[] T2Chromosome, sbyte[][][] ConvolutionChromosome);
internal static class Population
{
  public static Pop[] GenerateInitialPopulation(in ParameterSchema parameter)
  {
    Pop[] initialPopulation = new Pop[parameter.PopulationSize];
    for (int i = 0; i < parameter.PopulationSize; i++)
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
