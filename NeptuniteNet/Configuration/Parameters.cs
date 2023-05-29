using Spectre.Console;
using System.Text.Json;

namespace Neptunite.Configuration;

public readonly record struct ParameterSchema(
  ushort IterationLimit,
  byte PopulationSize,
  double MutationThreshold, // Higher threshold will produce fewer mutations
  int ConvolutionMatrixMutations, // Exact number of mask elements which will get mutated
  string DatasetWithFeature,
  string DatasetWithoutFeature,
  byte ConvolutionMatrixDimension
);

internal static class Parameters
{
  // Read "Config.json" file
  private static String ReadRawParameters()
  {
    string relative = Directory.GetCurrentDirectory();
    string path = Path.Combine(relative, "Configuration", "Config.json");
    return File.ReadAllText(path);
  }

  public static ParameterSchema DeserializeFromFile()
  {
    string rawJson = ReadRawParameters();
    JsonSerializerOptions options = new() { AllowTrailingCommas = true };
    return JsonSerializer.Deserialize<ParameterSchema>(rawJson, options);
  }

  public static void PrintParameters(in ParameterSchema parameter)
  {
    VerifyParameters(in parameter);

    string dim = parameter.ConvolutionMatrixDimension.ToString();
    string convolutionMatrixDimension = $"{dim}x{dim}";

    AnsiConsole.Write(new Table()
        .Border(TableBorder.Rounded)
        .AddColumn("[green]Parameter[/]")
        .AddColumn("[green]Value[/]")
        .AddRow("Iteration limit", parameter.IterationLimit.ToString())
        .AddRow("Population size", parameter.PopulationSize.ToString())
        .AddRow("Convolution matrix dimension", convolutionMatrixDimension)
        .AddRow("Number of convolution matrix mutations", parameter.ConvolutionMatrixMutations.ToString())
        .AddRow("Mutation threshold (for T1 and T2 operations)", parameter.MutationThreshold.ToString())
        .AddRow("Path of training dataset with feature", parameter.DatasetWithFeature)
        .AddRow("Path of training dataset without feature", parameter.DatasetWithoutFeature)
      );
  }

  private static void VerifyParameters(in ParameterSchema parameter)
  {
    if (!VerifyConvolutionMatrixDimension(parameter.ConvolutionMatrixDimension))
    {
      AnsiConsole.MarkupLine("[red]FATAL[/]");
      AnsiConsole.MarkupLine("Convolution matrix dimension must be odd");
      Environment.Exit(0);
    }

    if (!VerifyThresholdRange(parameter.MutationThreshold))
    {
      AnsiConsole.MarkupLine("[red]FATAL[/]");
      AnsiConsole.MarkupLine("Mutation threshold must be in [0, 1)");
      Environment.Exit(0);
    }
  }

  private static bool VerifyConvolutionMatrixDimension(in byte dimension) => dimension % 2 != 0;
  private static bool VerifyThresholdRange(in double threshold) => threshold is >= 0 and < 1;
}
