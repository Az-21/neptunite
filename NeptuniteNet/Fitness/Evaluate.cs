namespace Neptunite.Fitness;
internal static class Evaluate
{
  // Custom product of two last remaining nodes (at depth=2)
  private static int CustomMatrixProduct(in Image.Matrix matrixA, in Image.Matrix matrixB)
  {
    int product = 0;
    for (int i = 0; i < matrixA.Height; i++)
    {
      for (int j = 0; j < matrixA.Width; j++)
      {
        product += matrixA.ReadValue(i, j) * matrixB.ReadValue(i, j);
      }
    }

    return product;
  }

  // Assign +1 to correct answer, -1 to incorrect answer
  // Here, we're just forcing algorithm to classify to left and right of x=0
  private const int CorrectAnswerScore = 1;
  private const int IncorrectAnswerScore = -1;
  public static int Fitness(in Image.Matrix matrixA, in Image.Matrix matrixB, in bool isImageWithFeature)
  {
    int customMatrixProduct = CustomMatrixProduct(matrixA, matrixB);
    if (customMatrixProduct >= 0 && isImageWithFeature) { return CorrectAnswerScore; }
    if (customMatrixProduct < 0 && !isImageWithFeature) { return CorrectAnswerScore; }
    return IncorrectAnswerScore;
  }
}
