using Spectre.Console;
using System.Text.Json;

namespace Neptunite.Configuration;

public readonly record struct ParameterSchema(
  ushort IterationLimit,
  byte PopulationSize
);

internal static class Parameters
{
  // Read "Config.json" file
  private static String ReadRawParameters()
  {
    String relative = Directory.GetCurrentDirectory();
    String path = Path.Combine(relative, "Configuration", "Config.json");
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
    Table table = new();

    // Headers
    table.AddColumn("[green]Parameter[/]");
    table.AddColumn("[green]Value[/]");

    // Values
    table.AddRow("Iteration Limit", parameter.IterationLimit.ToString());
    table.AddRow("Population Size", parameter.PopulationSize.ToString());

    table.Border(TableBorder.Rounded);
    AnsiConsole.Write(table);
  }
}
