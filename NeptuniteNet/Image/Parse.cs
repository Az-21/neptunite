using SkiaSharp;
using Spectre.Console;

namespace Neptunite.Image;

internal static class Parse
{
  public static Image.Matrix AsGrayscaleImageMatrix(in string imagePath)
  {
    AnsiConsole.MarkupLine($"Evaluating population against [blue]{imagePath}[/]");

    SKBitmap image = SKBitmap.Decode(imagePath);
    int width = image.Width;
    int height = image.Height;

    // Note: Rectangular array [,] is better suited for this operation, but Microsoft has poorly implemented it
    int[][] grayscale = new int[height][];

    for (int i = 0; i < height; i++)
    {
      int[] pixelRow = new int[width];
      for (int j = 0; j < width; j++)
      {
        SKColor color = image.GetPixel(i, j);
        byte gray = ConvertPixelToGrayLevel(in color);
        pixelRow[j] = gray;
      }

      // Construct grayscale image row by row
      grayscale[i] = pixelRow;
    }

    return new Image.Matrix(grayscale);
  }

  private static byte ConvertPixelToGrayLevel(in SKColor color)
  {
    byte r = color.Red;
    byte g = color.Green;
    byte b = color.Blue;

    return (byte)((r + g + b) / 3);
  }
}
