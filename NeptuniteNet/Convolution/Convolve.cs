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
    int[][] paddedMatrix = new int[matrix.Height][];
    int height = matrix.Height;
    int width = matrix.Width;
    for (int i = 0; i < height; i++)
    {
      int[] row;
      if (i == 0 || i == height - 1) { row = ConstructArrayWithZeros(width + 2); }
      else { row = PadArrayWithZeros(matrix.ReadRow(i)); }
      paddedMatrix[i] = row;
    }

    return new Image.Matrix(paddedMatrix);
  }

  private static int[] ConstructArrayWithZeros(in int length)
  {
    int[] array = new int[length];
    for (int i = 0; i < length; i++) { array[i] = 0; }
    return array;
  }

  private static int[] PadArrayWithZeros(in int[] array)
  {
    int length = array.Length;
    int[] paddedArray = new int[length + 2];

    paddedArray[0] = 0;
    paddedArray[length - 1] = 0;
    for (int i = 1; i < length + 1; i++) { paddedArray[i] = array[i - 1]; }

    return paddedArray;
  }
}
