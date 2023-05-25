﻿using Spectre.Console;
using System.Text.Json;

namespace Neptunite.Configuration;

public readonly record struct ParameterSchema(
  ushort IterationLimit,
  byte PopulationSize,
  string DatasetWithFeature,
  string DatasetWithoutFeature,
  byte ConvolutionMatrixDimension
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
  }

  private static bool VerifyConvolutionMatrixDimension(in byte dimension) => dimension % 2 != 0;
}
