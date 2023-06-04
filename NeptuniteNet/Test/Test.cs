using Neptunite.Configuration;
using Neptunite.Generate;
using Neptunite.Image;
using Neptunite.Tree;

namespace Neptunite.Test;
internal static class Test
{
  public static int[] FittestChromosome(in Pop[] population, in ParameterSchema parameter)
  {
    Images images = Load.PathOfImages(in parameter, true);
    Pop[] bestPop = SelectBestChromosome(in population);
    int[] fitness = Structure.EvaluatePopulation(bestPop, in images);

    return new int[2] { fitness[0], CalculateMaximumPossibleFitness(in images) };
  }

  private static int CalculateMaximumPossibleFitness(in Images images) =>
    images.WithFeature.Count + images.WithoutFeature.Count;

  private static Pop[] SelectBestChromosome(in Pop[] population)
  {
    Pop[] selectedRange = new Pop[1];
    Array.Copy(population, 0, selectedRange, 0, 1);
    return selectedRange;
  }
}
