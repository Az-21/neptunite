namespace Neptunite.Fitness;
internal static class Evaluate
{
  // Custom product of two last remaining nodes (at depth=2)
  private static int CustomMatrixProduct(in int[][] matrixA, in int[][] matrixB)
  {
    int product = 0;
    for (int i = 0; i < matrixA.Length; i++)
    {
      for (int j = 0; j < matrixA[0].Length; j++)
      {
        product += matrixA[i][j] * matrixB[i][j];
      }
    }

    return product;
  }

  // Assign +3 to correct answer, 0 to incorrect answer
  const int CorrectAnswerScore = 3;
  const int IncorrectAnswerScore = 0;
  public static int Fitness(in int[][] matrixA, in int[][] matrixB)
  {
    int customMatrixProduct = CustomMatrixProduct(matrixA, matrixB);
    if (customMatrixProduct >= 1) { return CorrectAnswerScore; }
    return IncorrectAnswerScore;
  }
}
