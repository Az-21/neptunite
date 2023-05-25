using Neptunite.Configuration;

namespace Neptunite.Mutation;
internal static class Matrix
{
  // TODO: Make multi-threading safe
  static readonly Random random = new();

  public static void MutateInplace(ref sbyte[][] matrix, in ParameterSchema parameter)
  {
    int height = matrix.Length;
    int width = matrix[0].Length;

    for (int i = 0; i < height; i++)
    {
      for (int j = 0; j < width; j++)
      {
        if (parameter.MutationThreshold > random.NextDouble()) { matrix[i][j] = (sbyte)GenerateRandomMaskElement(); }
      }
    }
  }

  const int MinMaskValue = -5;
  const int MaxMaskValue = 5;
  private static int GenerateRandomMaskElement() => random.Next(MinMaskValue, MaxMaskValue);
}
