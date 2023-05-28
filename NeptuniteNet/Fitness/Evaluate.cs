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

  // Assign +1 to correct answer, -1 to incorrect answer
  // Here, we're just forcing algorithm to classify to left and right of x=0
  const int CorrectAnswerScore = 1;
  const int IncorrectAnswerScore = -1;
  public static int Fitness(in int[][] matrixA, in int[][] matrixB, in bool isImageWithFeature)
  {
    int customMatrixProduct = CustomMatrixProduct(matrixA, matrixB);
    if (customMatrixProduct >= 0 && isImageWithFeature) { return CorrectAnswerScore; }
    if (customMatrixProduct < 0 && !isImageWithFeature) { return CorrectAnswerScore; }
    return IncorrectAnswerScore;
  }
}
