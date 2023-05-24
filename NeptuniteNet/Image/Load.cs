using Spectre.Console;

namespace Neptunite.Image;

public readonly record struct Images(List<string> WithFeature, List<string> WithoutFeature);

internal static class Load
{
  public static Images PathOfImages(in Configuration.ParameterSchema parameter)
  {
    List<string> imagesWithFeature = Load.PathOfImages(parameter.DatasetWithFeature);
    List<string> imagesWithoutFeature = Load.PathOfImages(parameter.DatasetWithoutFeature);

    return new Images(imagesWithFeature, imagesWithoutFeature);
  }

  private static List<string> PathOfImages(string folderPath)
  {
    List<string> imageFiles = new();
    ProcessFolder(folderPath, ref imageFiles);
    return imageFiles;
  }

  private static void ProcessFolder(string folderPath, ref List<string> imageFiles)
  {
    string[] fileEntries = Directory.GetFiles(folderPath, "*.*");
    foreach (string filePath in fileEntries)
    {
      string extension = Path.GetExtension(filePath);
      if (extension is null) { continue; }

      bool isJpg = extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase);
      bool isJpeg = extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase);
      if (isJpg || isJpeg) { imageFiles.Add(filePath); }
    }

    // Recursively crawl through sub-directories
    string[] subdirectories = Directory.GetDirectories(folderPath);
    foreach (string subdirectory in subdirectories) { ProcessFolder(subdirectory, ref imageFiles); }
  }

  public static void PrintImagesCount(in Images images)
  {
    int imagesInDatasetWithFeature = images.WithFeature.Count;
    int imagesInDatasetWithoutFeature = images.WithoutFeature.Count;

    AnsiConsole.MarkupLine("\nLoaded images with [teal]`.jpg`[/] and [teal]`.jpeg`[/] extension");
    AnsiConsole.Write(new BreakdownChart()
      .FullSize()
      .AddItem("Images with feature", imagesInDatasetWithFeature, Color.Blue)
      .AddItem("Images without feature", imagesInDatasetWithoutFeature, Color.Green)
    );
  }
}
