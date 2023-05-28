namespace Neptunite.Static;
internal static class TypeTwoFunction
{
  // MatrixA and MatrixB are two instances of the same image =>> Guaranteed to be same dimension
  private static Image.Matrix Apply(in Image.Matrix matrixA, in Image.Matrix matrixB, in Func<int, int, int> function)
  {
    int[][] output = new int[matrixA.Height][];

    for (int i = 0; i < matrixA.Height; i++)
    {
      int[] row = new int[matrixA.Width];
      for (int j = 0; j < matrixA.Width; j++)
      {
        row[j] = function(matrixA.ReadValue(i, j), matrixB.ReadValue(i, j));
      }
      output[i] = row;
    }

    return new Image.Matrix(output);
  }

  private static readonly Func<int, int, int> _Add = (a, b) => (int)(a + b);
  private static Image.Matrix Add(in Image.Matrix A, in Image.Matrix B) => Apply(in A, in B, _Add);

  private static readonly Func<int, int, int> _Subtract = (a, b) => (int)(a - b);
  private static Image.Matrix Subtract(in Image.Matrix A, in Image.Matrix B) =>
    Apply(in A, in B, _Subtract);

  private static readonly Func<int, int, int> _Multiply = (a, b) => (int)(a * b);
  private static Image.Matrix Multiply(in Image.Matrix A, in Image.Matrix B) =>
    Apply(in A, in B, _Multiply);

  private static readonly Func<int, int> _PreventZeroDivision = (int x) => (int)(x == 0 ? 1 : x);
  private static readonly Func<int, int, int> _Divide = (a, b) => (int)(a / _PreventZeroDivision(b));
  private static Image.Matrix Divide(in Image.Matrix A, in Image.Matrix B) =>
    Apply(in A, in B, _Divide);

  public enum TypeTwoOperation { Add, Subtract, Multiply, Divide, }
  public static readonly int t2Count = Enum.GetNames(typeof(TypeTwoOperation)).Length;

  public static Image.Matrix ApplyTypeOneOperationInplace(in Image.Matrix matrixA, in Image.Matrix matrixB, in int operation) =>
    operation switch
    {
      (int)TypeTwoOperation.Add => Add(matrixA, matrixB),
      (int)TypeTwoOperation.Subtract => Subtract(matrixA, matrixB),
      (int)TypeTwoOperation.Multiply => Multiply(matrixA, matrixB),
      (int)TypeTwoOperation.Divide => Divide(matrixA, matrixB),
      _ => throw new NotImplementedException(),
    };
}
