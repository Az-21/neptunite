using Neptunite.Configuration;
using Neptunite.Generate;
using Neptunite.Image;
using Neptunite.Tree;
using Spectre.Console;

namespace Neptunite;
internal static class Program
{
  private static void Main()
  {
    AnsiConsole.Write(new FigletText("Neptunite").Color(Color.Purple_2));
    Console.WriteLine();

    // 1. Read configuration
    ParameterSchema parameter = Parameters.DeserializeFromFile();
    Parameters.PrintParameters(in parameter);

    // 2. Load path of all `.jpg` and `.jpeg` files into memory
    Images images = Load.PathOfImages(in parameter);
    Load.PrintImagesCount(images);

    // 3. Generate initial population of chromosomes (T1F, T2F, and convolution)
    Pop[] population = Population.GenerateInitialPopulation(in parameter);

    // 4. Evaluate every chromosome against every image -> Select top 50% -> Mutate -> Repeat
    for (int i = 0; i < parameter.IterationLimit; i++)
    {
      Console.WriteLine("\n\n");
      AnsiConsole.Write(new Rule($"Evaluating Generation [green]{i + 1}[/]").LeftJustified());
      int[] fitness = Structure.EvaluatePopulation(population, in images);

      Array.Sort(fitness, population, Comparer<int>.Create((a, b) => b.CompareTo(a)));
      // TODO
      // - Linked sort
      // - Eliminate bottom half
      // - Mutate and append top half
    }

    // TODO: Output final iteration chromosome
  }
}
