using Neptunite.Configuration;

namespace Neptunite.Generate;
internal static class InitialPopulation
{
  public static sbyte[][] GenerateRandomConvolutionMatrix(in ParameterSchema parameter)
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
}
