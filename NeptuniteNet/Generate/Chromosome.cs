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

  const int TypeOneFunctionChromosomeLength = 5;
  public static int[] RandomlyGenerateTypeOneChromosome()
  {
    int enumCount = Enum.GetNames(typeof(TypeOneOperation)).Length;
    return FillChromosomeWithRandomIntegers(TypeOneFunctionChromosomeLength, enumCount);
  }

  const int TypeTwoFunctionChromosomeLength = 9;
  public static int[] RandomlyGenerateTypeTwoChromosome()
  {
    int enumCount = Enum.GetNames(typeof(TypeTwoOperation)).Length;
    return FillChromosomeWithRandomIntegers(TypeTwoFunctionChromosomeLength, enumCount);
  }
}

internal static partial class Chromosome
{
  public static sbyte[][] RandomlyGenerateConvolutionMask(in ParameterSchema parameter)
  {
    byte dimension = parameter.ConvolutionMatrixDimension;
    sbyte[][] matrix = Matrix.SquareMatrix.Create(dimension);
    for (int i = 0; i < dimension; i++)
    {
      for (int j = 0; j < dimension; j++)
      {
        matrix[i][j] = Mutation.Matrix.GenerateRandomMaskElement();
      }
    }

    return matrix;
  }

  const int ConvolutionChromosomeLength = 5;

}
