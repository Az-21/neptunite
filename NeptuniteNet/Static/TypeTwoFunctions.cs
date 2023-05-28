namespace Neptunite.Static;
internal static class TypeTwoFunctions
{
  // MatrixA and MatrixB are two instances of the same image =>> Guaranteed to be same dimension
  private static void Apply(in int[][] matrixA, in int[][] matrixB, in Func<int, int, int> function)
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
  }

  private static readonly Func<int, int, int> _Add = (a, b) => (int)(a + b);
  public static void Add(in int[][] matrixA, in int[][] matrixB) =>
    Apply(in matrixA, in matrixB, _Add);

  private static readonly Func<int, int, int> _Subtract = (a, b) => (int)(a - b);
  public static void Subtract(in int[][] matrixA, in int[][] matrixB) =>
    Apply(in matrixA, in matrixB, _Subtract);

  private static readonly Func<int, int, int> _Multiply = (a, b) => (int)(a * b);
  public static void Multiply(in int[][] matrixA, in int[][] matrixB) =>
    Apply(in matrixA, in matrixB, _Multiply);

  private static readonly Func<int, int> _PreventZeroDivision = (int x) => (int)(x == 0 ? 1 : x);
  private static readonly Func<int, int, int> _Divide = (a, b) => (int)(a / _PreventZeroDivision(b));
  public static void Divide(in int[][] matrixA, in int[][] matrixB) =>
    Apply(in matrixA, in matrixB, _Divide);

  public enum TypeTwoNode
  {
    Add, Subtract, Multiply, Divide,
  }
}
