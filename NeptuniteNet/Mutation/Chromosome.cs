using Neptunite.Configuration;

namespace Neptunite.Mutation;
internal static partial class Chromosome
{
  public static void MutateTypeOneChromosomeInplace(ref int[] t1Chromosome, in ParameterSchema parameter)
  {
    for (int i = 0; i < Generate.Chromosome.TypeOneChromosomeLength; i++)
    {
      if (Random.Shared.NextDouble() < parameter.MutationThreshold) { continue; }
      t1Chromosome[i] = Random.Shared.Next(0, Static.TypeOneFunction.t1Count);
    }
  }

  public static void MutateTypeTwoChromosomeInplace(ref int[] t2Chromosome, in ParameterSchema parameter)
  {
    for (int i = 0; i < Generate.Chromosome.TypeTwoChromosomeLength; i++)
    {
      if (Random.Shared.NextDouble() < parameter.MutationThreshold) { continue; }
      t2Chromosome[i] = Random.Shared.Next(0, Static.TypeTwoFunction.t2Count);
    }
  }
}
