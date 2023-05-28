namespace Neptunite.Convolution;
internal static class Convolve
{
  private static byte CalculateIndexOffset(in Image.Matrix matrix) =>
    // For a 1x1 matrix, offset is 0
    // For a 3x3 matrix, offset is 1
    // For a 5x5 matrix, offset is 2
    (byte)(matrix.Height / 2);

  public static Image.Matrix TwoMatrices(in Image.Matrix matrix, in Image.Matrix mask)
  {
    int offset = CalculateIndexOffset(in mask);
    int height = matrix.Height;
    int width = matrix.Width;

    int[][] convolvedMatrix = new int[height][];
    Image.Matrix paddedMatrix = PadMatrixWithZeros(in matrix);

    for (int i = offset; i < height - offset; i++)
    {
      int[] row = new int[width];
      for (int j = offset; j < width - offset; j++)
      {
        row[j] = ReplaceSourcePixel(in paddedMatrix, in mask, in i, in j, in offset);
      }
      convolvedMatrix[i] = row;
    }

    return new Image.Matrix(convolvedMatrix);
  }

  private static int ReplaceSourcePixel(in Image.Matrix matrix, in Image.Matrix mask, in int i, in int j, in int offset)
  {
    int maskRow = 0;
    int maskCol = 0;
    int pixelReplacement = 0;

    for (int row = i - offset; row <= i + offset; row++)
    {
      for (int col = j - offset; col <= j + offset; col++)
      {
        pixelReplacement += matrix.ReadValue(row, col) * mask.ReadValue(maskRow, maskCol);
        maskCol++;
      }
      maskCol = 0;
      maskRow++;
    }

    return pixelReplacement;
  }

  public static Image.Matrix PadMatrixWithZeros(in Image.Matrix matrix)
  {
    int rows = matrix.Height;
    int cols = matrix.Width;

    // Calculate the new dimensions for the padded matrix
    int paddedRows = rows + 2;
    int paddedCols = cols + 2;

    // Create the padded matrix
    int[][] paddedMatrix = new int[paddedRows][];

    // Fill the padded matrix with zeros
    for (int i = 0; i < paddedRows; i++)
    {
      paddedMatrix[i] = new int[paddedCols];
      for (int j = 0; j < paddedCols; j++)
      {
        paddedMatrix[i][j] = 0;
      }
    }

    // Copy the original matrix values to the padded matrix
    for (int i = 0; i < rows; i++)
    {
      for (int j = 0; j < cols; j++)
      {
        paddedMatrix[i + 1][j + 1] = matrix.ReadValue(i, j);
      }
    }

    return new Image.Matrix(paddedMatrix);
  }
}
