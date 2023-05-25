namespace Neptunite.Static;
internal static class EdgeDetection
{
  static readonly sbyte[][] SobelMatrixVertical = new sbyte[][] {
  new sbyte[] { -1, -2, -1 },
  new sbyte[] { 0, 0, 0 },
  new sbyte[] { 1, 2, 1 },
  };

  static readonly sbyte[][] SobelMatrixHorizontal = new sbyte[][] {
  new sbyte[] { -1, 0, 1 },
  new sbyte[] { -2, 0, 2 },
  new sbyte[] { -1, 0, 1 },
  };

}
