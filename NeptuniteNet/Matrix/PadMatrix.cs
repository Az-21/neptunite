namespace Neptunite.Matrix;
internal static class PadMatrix
{
  public static byte[][] WithZeros(in byte[][] matrix)
  {
    byte[][] paddedMatrix = new byte[matrix.Length][];
    int height = matrix.Length;
    int width = matrix[0].Length;
    for (int i = 0; i < height; i++)
    {
      byte[] row;
      if (i == 0 || i == height - 1) { row = ConstructArrayWithZeros(width + 2); }
      else { row = PadArrayWithZeros(matrix[i]); }
      paddedMatrix[i] = row;
    }

    return paddedMatrix;
  }

  private static byte[] ConstructArrayWithZeros(in int length)
  {
    byte[] array = new byte[length];
    for (int i = 0; i < length; i++) { array[i] = 0; }
    return array;
  }

  private static byte[] PadArrayWithZeros(in byte[] array)
  {
    int length = array.Length;
    byte[] paddedArray = new byte[length + 2];

    paddedArray[0] = 0;
    paddedArray[length - 1] = 0;
    for (int i = 1; i < length + 1; i++) { paddedArray[i] = array[i - 1]; }

    return paddedArray;
  }
}
