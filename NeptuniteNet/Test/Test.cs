using Neptunite.Configuration;
using Neptunite.Generate;
using Neptunite.Image;
using Neptunite.Tree;
using Spectre.Console;

namespace Neptunite.Test;
internal static class Test
{
  public static int[] FittestChromosome(in Pop[] population, in ParameterSchema parameter)
  {
    Images images = Load.PathOfImages(in parameter, true);
    Pop[] bestPop = SelectBestChromosome(in population);

    Console.WriteLine("\n\n");
    AnsiConsole.Write(new Rule("Testing fittest chromosmome").LeftJustified().RuleStyle("green"));

    int[] fitness = Structure.EvaluatePopulation(bestPop, in images);
    int maxFitness = CalculateMaximumPossibleFitness(in images);

    AnsiConsole.MarkupLine($"\nFitness of best chromosome against testing database: [fuchsia]{fitness[0]}[/]");
    AnsiConsole.MarkupLine($"Maximum possible fitness against testing database: [green]{maxFitness}[/]");

    return new int[2] { fitness[0], maxFitness };
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
