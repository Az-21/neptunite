namespace Neptunite.Convolution;
internal static class Convolve
{
  private static byte CalculateIndexOffset(in sbyte[][] matrix) =>
    // For a 1x1 matrix, offset is 0
    // For a 3x3 matrix, offset is 1
    // For a 5x5 matrix, offset is 2
    (byte)((matrix.Length / 2) + 1);

  private static int[][] TwoMatrices(in byte[][] matrix, in sbyte[][] mask)
  {
    byte[][] paddedMatrix = Matrix.PadMatrix.WithZeros(in matrix);

    int offset = CalculateIndexOffset(in mask);
    int height = paddedMatrix.Length;
    int width = paddedMatrix[0].Length;

    int[][] convolvedMatrix = new int[height][];

    for (int i = offset; i < height - offset; i++)
    {
      int[] row = new int[width];
      for (int j = offset; j < width - offset; j++)
      {
        row[j] = ReplaceSourcePixel(in paddedMatrix, in mask, in i, in j, in offset);
      }
      convolvedMatrix[i] = row;
    }

    return convolvedMatrix;
  }

  private static int ReplaceSourcePixel(in byte[][] matrix, in sbyte[][] mask, in int i, in int j, in int offset)
  {
    int maskRow = 0;
    int maskCol = 0;
    int pixelReplacement = 0;

    for (int row = i - offset; row <= i + offset; row++)
    {
      for (int col = j - offset; col <= j + offset; col++)
      {
        pixelReplacement += matrix[row][col] * mask[maskRow][maskCol];
        maskCol++;
      }
      maskRow++;
    }

    return pixelReplacement;
  }
}
