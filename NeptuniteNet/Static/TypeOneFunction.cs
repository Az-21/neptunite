namespace Neptunite.Static;
internal static class TypeOneFunction
{
  private static void Apply(ref int[][] matrix, Func<int, int> function)
  {
    for (int i = 0; i < matrix.Length; i++)
    {
      for (int j = 0; j < matrix[0].Length; j++)
      {
        matrix[i][j] = function(matrix[i][j]);
      }
    }
  }

  static readonly Func<int, int> LogBase10 = (x) => (int)Math.Log10(x);
  public static void LogBase10Inplace(ref int[][] matrix) => Apply(ref matrix, LogBase10);
}
