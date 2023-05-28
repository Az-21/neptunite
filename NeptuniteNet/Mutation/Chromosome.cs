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
  public static sbyte GenerateRandomMaskElement() => (sbyte)Random.Shared.Next(MinMaskValue, MaxMaskValue);

  public static void MutateConvolutionChromosomeInplace(ref Image.Matrix[] convolutionChromosome, in ParameterSchema parameter)
  {
    for (int i = 0; i < parameter.ConvolutionMatrixMutations; i++)
    {
      int x = Random.Shared.Next(Generate.Chromosome.ConvolutionChromosomeLength - 1);
      int y = Random.Shared.Next(parameter.ConvolutionMatrixDimension - 1);
      int z = Random.Shared.Next(parameter.ConvolutionMatrixDimension - 1);

      convolutionChromosome[x].Modify(GenerateRandomMaskElement(), y, z);
    }
  }
}
