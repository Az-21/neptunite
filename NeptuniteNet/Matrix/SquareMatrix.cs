namespace Neptunite.Matrix;
internal static class SquareMatrix
{
  public static byte[][] Create(byte dimension, byte fillWith = 0)
  {
    byte[][] matrix = new byte[dimension][];
    for (int i = 0; i < dimension; i++)
    {
      byte[] row = new byte[dimension];
      for (int j = 0; j < dimension; j++) { row[j] = fillWith; }
    }

    return matrix;
  }
}
