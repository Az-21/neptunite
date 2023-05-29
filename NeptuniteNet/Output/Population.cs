using Neptunite.Configuration;
using Neptunite.Generate;

namespace Neptunite.Output;
internal static class Population
{
  public static void WriteTopFiftyPopsToOutput(in Pop[] population, in ParameterSchema parameter)
  {
    Pop[] pops = FilterTopPops(in population, in parameter);

    string popsDump = ObjectDumper.Dump(pops, DumpStyle.CSharp);
    WriteOutputCSharpFile(popsDump);
  }

  private static void WriteOutputCSharpFile(string outputString)
  {
    DateTime dateTime = DateTime.Now;
    string relative = Directory.GetCurrentDirectory();
    string outputFolderPath = Path.Combine(relative, "Output");
    if (!Directory.Exists(outputFolderPath)) { Directory.CreateDirectory(outputFolderPath); }
    string outputFilePath = Path.Combine(outputFolderPath, $"output_{dateTime:yyyy-MM-dd-HH-mm}.cs");

    using StreamWriter writer = new(outputFilePath);
    writer.Write(outputString);
  }

  private static Pop[] FilterTopPops(in Pop[] population, in ParameterSchema parameter)
  {
    int length = parameter.PopulationSize;

    Pop[] selectedRange = new Pop[length];
    Array.Copy(population, 0, selectedRange, 0, length);
    return selectedRange;
  }
}
