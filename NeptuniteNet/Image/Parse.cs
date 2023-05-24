using SkiaSharp;

namespace Neptunite.Image;

internal static class Parse
{
  private static byte[][] AsGrayscaleImageMatrix(in string imagePath)
  {
    SKBitmap image = SKBitmap.Decode(imagePath);
    int width = image.Width;
    int height = image.Height;

    // Note: Rectangular array [,] is better suited for this operation, but Microsoft has poorly implemented it
    byte[][] grayscale = new byte[height][];

    for (int i = 0; i < height; i++)
    {
      byte[] pixelRow = new byte[width];
      for (int j = 0; j < width; j++)
      {
        SKColor color = image.GetPixel(i, j);
        byte gray = ConvertPixelToGrayLevel(in color);
        pixelRow[j] = gray;
      }

      // Construct grayscale image row by row
      grayscale[i] = pixelRow;
    }

    return grayscale;
  }

  private static byte ConvertPixelToGrayLevel(in SKColor color)
  {
    byte r = color.Red;
    byte g = color.Green;
    byte b = color.Blue;

    return (byte)((r + g + b) / 3);
  }
}
