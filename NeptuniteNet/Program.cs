using Neptunite.Configuration;
using Neptunite.Generate;
using Neptunite.Image;
using Neptunite.Tree;

namespace Neptunite;
internal static class Program
{
  static void Main()
  {
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
      int[] fitness = Structure.EvaluatePopulation(in population, in images);
      Console.WriteLine($"Generation {i} evaluated");

      // TODO
      // - Linked sort
      // - Eliminate bottom half
      // - Mutate and append top half
    }

    // TODO: Output final interation chromosome
  }
}
