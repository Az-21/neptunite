using Neptunite.Configuration;

namespace Neptunite;
internal static class Program
{
  static void Main()
  {
    // 1. Read configuration
    ParameterSchema parameter = Parameters.DeserializeFromFile();
    Parameters.PrintParameters(parameter);
  }
}
