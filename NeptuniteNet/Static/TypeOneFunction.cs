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

  private static readonly Func<int, int> LogBase10 = (x) => (int)Math.Log10(x);
  private static void LogBase10Inplace(ref int[][] matrix) => Apply(ref matrix, LogBase10);

  private static readonly Func<int, int> LogBase2 = (x) => (int)Math.Log2(x);
  private static void LogBase2Inplace(ref int[][] matrix) => Apply(ref matrix, LogBase2);

  private static readonly Func<int, int> NaturalLog = (x) => (int)Math.Log(x);
  private static void NaturalLogInplace(ref int[][] matrix) => Apply(ref matrix, NaturalLog);

  private static readonly Func<int, int> Sin = (x) => (int)Math.Sin(x); // Angle is in degree
  private static void SinInplace(ref int[][] matrix) => Apply(ref matrix, Sin);

  private static readonly Func<int, int> Cos = (x) => (int)Math.Cos(x); // Angle is in degree
  private static void CosInplace(ref int[][] matrix) => Apply(ref matrix, Cos);

  private static readonly Func<int, int> Tan = (x) => (int)Math.Tan(x); // Angle is in degree
  private static void TanInplace(ref int[][] matrix) => Apply(ref matrix, Tan);

  public enum TypeOneOperation { LogBase10, LogBase2, NaturalLog, Sin, Cos, Tan, }
  public static readonly int t1Count = Enum.GetNames(typeof(TypeOneOperation)).Length;

  public static void ApplyTypeOneOperationInplace(ref int[][] matrix, in int operation)
  {
    switch (operation)
    {
      case (int)TypeOneOperation.LogBase10: LogBase10Inplace(ref matrix); break;
      case (int)TypeOneOperation.LogBase2: LogBase2Inplace(ref matrix); break;
      case (int)TypeOneOperation.NaturalLog: NaturalLogInplace(ref matrix); break;
      case (int)TypeOneOperation.Sin: SinInplace(ref matrix); break;
      case (int)TypeOneOperation.Cos: CosInplace(ref matrix); break;
      case (int)TypeOneOperation.Tan: TanInplace(ref matrix); break;
      default: throw new NotImplementedException();
    }
  }
}
