namespace Neptunite.Convolution;
internal static class Convolve
{
  private static int CalculateIndexOffset(in Image.Matrix matrix) =>
    // For a 1x1 matrix, offset is 0
    // For a 3x3 matrix, offset is 1
    // For a 5x5 matrix, offset is 2
    (matrix.Height - 1) / 2;

  public static Image.Matrix TwoMatrices(in Image.Matrix matrix, in Image.Matrix mask)
  {
    int matrixRows = matrix.Height;
    int matrixCols = matrix.Width;
    int maskRows = mask.Height;
    int maskCols = mask.Width;

    // Calculate the dimensions of the resulting convolved matrix
    int resultRows = matrixRows - maskRows + 1;
    int resultCols = matrixCols - maskCols + 1;

    // Create the resulting convolved matrix
    int[][] resultMatrix = new int[resultRows][];

    // Perform the convolution operation
    for (int i = 0; i < resultRows; i++)
    {
      resultMatrix[i] = new int[resultCols];
      for (int j = 0; j < resultCols; j++)
      {
        // Compute the convolution value at the current position
        int convolutionValue = 0;
        for (int k = 0; k < maskRows; k++)
        {
          for (int l = 0; l < maskCols; l++)
          {
            convolutionValue += matrix.ReadValue(i + k, j + l) * mask.ReadValue(k, l);
          }
        }
        resultMatrix[i][j] = convolutionValue;
      }
    }

    return new Image.Matrix(resultMatrix);
  }
}
