using Neptunite.Configuration;
using Neptunite.Generate;
using System.Text;

namespace Neptunite.Output;
internal static class Population
{
  public static void LogOutput(in Pop[] population, in ParameterSchema parameter, in int[] fitnessSeries)
  {
    Pop[] pops = FilterTopPops(in population, in parameter);
    StringBuilder sb = new();
    sb.AppendLine(ObjectDumper.Dump(fitnessSeries, DumpStyle.CSharp));
    sb.AppendLine(ObjectDumper.Dump(pops, DumpStyle.CSharp));
    WriteOutputCSharpFile(sb.ToString());
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
