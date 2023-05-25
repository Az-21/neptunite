namespace Neptunite.Matrix;
internal static class SquareMatrix
{
  public static sbyte[][] Create(in byte dimension, in sbyte fillWith = 0)
  {
    sbyte[][] matrix = new sbyte[dimension][];
    for (int i = 0; i < dimension; i++)
    {
      sbyte[] row = new sbyte[dimension];
      for (int j = 0; j < dimension; j++) { row[j] = fillWith; }
    }

    return matrix;
  }
}
