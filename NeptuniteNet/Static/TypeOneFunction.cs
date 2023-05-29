namespace Neptunite.Static;
internal static class TypeOneFunction
{
  private static void Apply(ref Image.Matrix matrix, Func<int, int> function)
  {
    for (int i = 0; i < matrix.Height; i++)
    {
      for (int j = 0; j < matrix.Width; j++)
      {
        int value = function(matrix.ReadValue(i, j));
        matrix.Modify(value, i, j);
      }
    }
  }

  private static readonly Func<int, int> LogBase10 = (x) => (int)Math.Log10(x);
  private static void LogBase10Inplace(ref Image.Matrix matrix) => Apply(ref matrix, LogBase10);

  private static readonly Func<int, int> LogBase2 = (x) => (int)Math.Log2(x);
  private static void LogBase2Inplace(ref Image.Matrix matrix) => Apply(ref matrix, LogBase2);

  private static readonly Func<int, int> NaturalLog = (x) => (int)Math.Log(x);
  private static void NaturalLogInplace(ref Image.Matrix matrix) => Apply(ref matrix, NaturalLog);

  private static readonly Func<int, int> Sin = (x) => (int)Math.Sin(x); // Angle is in degree
  private static void SinInplace(ref Image.Matrix matrix) => Apply(ref matrix, Sin);

  private static readonly Func<int, int> Cos = (x) => (int)Math.Cos(x); // Angle is in degree
  private static void CosInplace(ref Image.Matrix matrix) => Apply(ref matrix, Cos);

  private static readonly Func<int, int> Tan = (x) => (int)Math.Tan(x); // Angle is in degree
  private static void TanInplace(ref Image.Matrix matrix) => Apply(ref matrix, Tan);

  private static readonly Func<int, int> Negate = (x) => -1 * x;
  private static void NegateInplace(ref Image.Matrix matrix) => Apply(ref matrix, Negate);

  private static readonly Func<int, int> Zero = (x) => 0 * x;
  private static void ZeroInplace(ref Image.Matrix matrix) => Apply(ref matrix, Zero);

  public enum TypeOneOperation { LogBase10, LogBase2, NaturalLog, Sin, Cos, Tan, Negate, Zero, }
  public static readonly int t1Count = Enum.GetNames(typeof(TypeOneOperation)).Length;

  public static void ApplyTypeOneOperationInplace(ref Image.Matrix matrix, in int operation)
  {
    switch (operation)
    {
      case (int)TypeOneOperation.LogBase10: LogBase10Inplace(ref matrix); break;
      case (int)TypeOneOperation.LogBase2: LogBase2Inplace(ref matrix); break;
      case (int)TypeOneOperation.NaturalLog: NaturalLogInplace(ref matrix); break;
      case (int)TypeOneOperation.Sin: SinInplace(ref matrix); break;
      case (int)TypeOneOperation.Cos: CosInplace(ref matrix); break;
      case (int)TypeOneOperation.Tan: TanInplace(ref matrix); break;
      case (int)TypeOneOperation.Negate: NegateInplace(ref matrix); break;
      case (int)TypeOneOperation.Zero: ZeroInplace(ref matrix); break;
      default: throw new NotImplementedException();
    }
  }
}
