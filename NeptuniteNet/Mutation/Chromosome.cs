using Neptunite.Configuration;

namespace Neptunite.Mutation;
internal static partial class Chromosome
{
  public static void MutateTypeOneChromosomeInplace(ref int[] t1Chromosome, in ParameterSchema parameter)
  {
    for (int i = 0; i < Generate.Chromosome.TypeOneChromosomeLength; i++)
    {
      if (parameter.MutationThreshold > Random.Shared.NextDouble()) { continue; }
      t1Chromosome[i] = Random.Shared.Next(0, Static.TypeOneFunction.t1Count);
    }
  }

  public static void MutateTypeTwoChromosomeInplace(ref int[] t2Chromosome, in ParameterSchema parameter)
  {
    for (int i = 0; i < Generate.Chromosome.TypeTwoChromosomeLength; i++)
    {
      if (parameter.MutationThreshold > Random.Shared.NextDouble()) { continue; }
      t2Chromosome[i] = Random.Shared.Next(0, Static.TypeTwoFunction.t2Count);
    }
  }
}

internal static partial class Chromosome
{
  const int MinMaskValue = -5;
  const int MaxMaskValue = 5;
  public static void MutateConvolutionChromosome(ref sbyte[][][] convolutionChromosome, in ParameterSchema parameter)
  {
    for (int layer = 0; layer < Generate.Chromosome.ConvolutionChromosomeLength; layer++)
    {
      for (int i = 0; i < parameter.ConvolutionMatrixDimension; i++)
      {
        for (int j = 0; j < parameter.ConvolutionMatrixDimension; j++)
        {
          if (parameter.MutationThreshold > Random.Shared.NextDouble()) { continue; }
          convolutionChromosome[layer][i][j] = GenerateRandomMaskElement();
        }
      }
    }
  }

  public static sbyte GenerateRandomMaskElement() => (sbyte)Random.Shared.Next(MinMaskValue, MaxMaskValue);
}
