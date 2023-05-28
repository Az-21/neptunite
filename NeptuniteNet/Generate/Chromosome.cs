using static Neptunite.Static.TypeOneFunction;
using static Neptunite.Static.TypeTwoFunction;

namespace Neptunite.Generate;
internal static class Chromosome
{
  // NOTE: See Tree/Structure.cs for placeholders. Items are arranged in BFS manner.
  const int TypeOneFunctionChromosomeLength = 5;
  const int TypeTwoFunctionChromosomeLength = 9;

  // Generate chromosome of fixed length. Fill values with T1F and T2F enums.
  private static int[] FillChromosomeWithRandomIntegers(in int length, in int enumCount)
  {
    int[] chromosome = new int[length];
    for (int i = 0; i < length; i++) { chromosome[i] = Random.Shared.Next(0, enumCount); }
    return chromosome;
  }

  public static int[] RandomlyGenerateTypeOneChromosome()
  {
    int enumCount = Enum.GetNames(typeof(TypeOneOperation)).Length;
    return FillChromosomeWithRandomIntegers(0, enumCount);
  }

  public static int[] RandomlyGenerateTypeTwoChromosome()
  {
    int enumCount = Enum.GetNames(typeof(TypeTwoOperation)).Length;
    return FillChromosomeWithRandomIntegers(0, enumCount);
  }
}
