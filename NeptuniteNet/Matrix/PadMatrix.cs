namespace Neptunite.Matrix;
internal static class PadMatrix
{
  public static Image.Matrix WithZeros(in Image.Matrix matrix)
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
