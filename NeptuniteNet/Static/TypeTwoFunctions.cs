namespace Neptunite.Static;
internal static class TypeTwoFunctions
{
  // MatrixA and MatrixB are two instances of the same image =>> Guaranteed to be same dimension
  private static int[][] Apply(in int[][] matrixA, in int[][] matrixB, in Func<int, int, int> function)
  {
    int height = matrixA.Length;
    int width = matrixA[0].Length;
    int[][] output = new int[height][];

    for (int i = 0; i < height; i++)
    {
      int[] row = new int[width];
      for (int j = 0; j < width; j++)
      {
        row[j] = function(matrixA[i][j], matrixB[i][j]);
      }
      output[i] = row;
    }

    return output;
  }

  private static readonly Func<int, int, int> _Add = (a, b) => (int)(a + b);
  private static int[][] Add(in int[][] A, in int[][] B) => Apply(in A, in B, _Add);

  private static readonly Func<int, int, int> _Subtract = (a, b) => (int)(a - b);
  private static int[][] Subtract(in int[][] A, in int[][] B) => Apply(in A, in B, _Subtract);

  private static readonly Func<int, int, int> _Multiply = (a, b) => (int)(a * b);
  private static int[][] Multiply(in int[][] A, in int[][] B) => Apply(in A, in B, _Multiply);

  private static readonly Func<int, int> _PreventZeroDivision = (int x) => (int)(x == 0 ? 1 : x);
  private static readonly Func<int, int, int> _Divide = (a, b) => (int)(a / _PreventZeroDivision(b));
  private static int[][] Divide(in int[][] A, in int[][] B) => Apply(in A, in B, _Divide);

  public enum TypeTwoOperation
  {
    Add, Subtract, Multiply, Divide,
  }
}
