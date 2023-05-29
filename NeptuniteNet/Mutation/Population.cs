using Neptunite.Configuration;
using Neptunite.Generate;

namespace Neptunite.Mutation;
internal static class Population
{
  public static void MutateTopFiftyAndReplaceBottomFiftyInplace(ref Pop[] parents, in ParameterSchema parameter)
  {
    int pops = parameter.PopulationSize;
    for (int i = pops; i < 2 * pops; i++)
    {
      // Deep copy top 50% population's genes
      int[] t1Chromosome = DeepCopy(parents[i - pops + 1].T1Chromosome);
      int[] t2Chromosome = DeepCopy(parents[i - pops + 1].T2Chromosome);
      Image.Matrix[] convolutionChromosome = DeepCopyImageMatrix(parents[i - pops + 1].ConvolutionChromosome);

      // Mutate
      Chromosome.MutateTypeOneChromosomeInplace(ref t1Chromosome, in parameter);
      Chromosome.MutateTypeTwoChromosomeInplace(ref t2Chromosome, in parameter);
      Chromosome.MutateConvolutionChromosomeInplace(ref convolutionChromosome, in parameter);

      // Replace bottom 50% of population
      parents[i] = new Pop(t1Chromosome, t2Chromosome, convolutionChromosome);
    }
  }

  private static T[] DeepCopy<T>(in T[] sourceArray)
  {
    int length = sourceArray.Length;
    T[] copy = new T[length];

    // Perform a deep copy of the bottom 50% of the elements
    Array.Copy(sourceArray, sourceArray.Length - length, copy, 0, length);
    return copy;
  }

  private static Image.Matrix[] DeepCopyImageMatrix(in Image.Matrix[] matrices)
  {
    int layers = matrices.Length;
    int rows = matrices[0].Height;
    int cols = matrices[0].Width;

    Image.Matrix[] copy = new Image.Matrix[layers];
    for (int i = 0; i < layers; i++)
    {
      int[][] matrix = new int[rows][];
      for (int j = 0; j < rows; j++)
      {
        int[] line = new int[cols];
        for (int k = 0; k < cols; k++)
        {
          line[k] = matrices[i].ReadValue(j, k);
        }
        matrix[j] = line;
      }
      copy[i] = new Image.Matrix(matrix);
    }

    return copy;
  }
}
