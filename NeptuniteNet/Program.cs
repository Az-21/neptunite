using Neptunite.Configuration;
using Neptunite.Image;

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
  }
}
