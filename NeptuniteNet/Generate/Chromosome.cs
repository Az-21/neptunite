using Neptunite.Configuration;
using static Neptunite.Static.TypeOneFunction;
using static Neptunite.Static.TypeTwoFunction;

namespace Neptunite.Generate;
internal static partial class Chromosome
{
  // Generate chromosome of fixed length. Fill values with T1F and T2F enums.
  private static int[] FillChromosomeWithRandomIntegers(in int length, in int enumCount)
  {
    int[] chromosome = new int[length];
    for (int i = 0; i < length; i++) { chromosome[i] = Random.Shared.Next(0, enumCount); }
    return chromosome;
  }

  public const int TypeOneChromosomeLength = 4;
  public static int[] RandomlyGenerateTypeOneChromosome() =>
    FillChromosomeWithRandomIntegers(TypeOneChromosomeLength, t1Count);

  public const int TypeTwoChromosomeLength = 5;
  public static int[] RandomlyGenerateTypeTwoChromosome() =>
    FillChromosomeWithRandomIntegers(TypeTwoChromosomeLength, t2Count);
}

internal static partial class Chromosome
{
  private static sbyte[][] RandomlyGenerateConvolutionMask(in ParameterSchema parameter)
  {
    byte dimension = parameter.ConvolutionMatrixDimension;
    sbyte[][] matrix = new sbyte[dimension][];
    for (int i = 0; i < dimension; i++)
    {
      sbyte[] row = new sbyte[dimension];
      for (int j = 0; j < dimension; j++)
      {
        row[j] = Mutation.Chromosome.GenerateRandomMaskElement();
      }
    }

    return matrix;
  }

  public const int ConvolutionChromosomeLength = 5;
  public static sbyte[][][] RandomlyGenerateConvolutionChromosome(in ParameterSchema parameter)
  {
    sbyte[][][] chromosome = new sbyte[ConvolutionChromosomeLength][][];
    for (int i = 0; i < ConvolutionChromosomeLength; i++)
    {
      chromosome[i] = RandomlyGenerateConvolutionMask(in parameter);
    }

    return chromosome;
  }
}
