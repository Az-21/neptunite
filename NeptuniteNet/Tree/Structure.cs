using Neptunite.Convolution;
using Neptunite.Fitness;
using Neptunite.Generate;
using Neptunite.Static;

namespace Neptunite.Tree;
/*
                 *
               /   \ 
           T2F       T2F 
          /   \     /   \ 
      T2F       T2F      T2F 
     /   \    /    \    /    \ 
 T1F       T1F       T1F       T1F
  |         |         |         |
Convo     Convo     Convo     Convo
  |         |         |         |
Image     Image     Image     Image
*/

internal static class Structure
{
  const int BaseLayerWidth = 4;
  private static int ApplyChromosomeOnImage(in Pop chromosome, in byte[][] image)
  {
    // Copy image for different convolutions
    int[][][] gpMatrix = new int[BaseLayerWidth][][];
    byte[][][] gpMatrixByte = new byte[BaseLayerWidth][][];
    for (int i = 0; i < BaseLayerWidth; i++) { gpMatrixByte[i] = image; }

    for (int i = 0; i < BaseLayerWidth; i++)
    {
      // 1. Convolution
      gpMatrix[i] = Convolve.TwoMatrices(gpMatrixByte[i], chromosome.ConvolutionChromosome[i]);

      // 2. Type 1 function
      TypeOneFunction.ApplyTypeOneOperationInplace(ref gpMatrix[i], chromosome.T1Chromosome[i]);
    }

    // 3. Bottom type 1 function layer
    gpMatrix[0] = TypeTwoFunction.ApplyTypeOneOperationInplace(
       gpMatrix[0],
       gpMatrix[1],
      chromosome.T2Chromosome[2]
    );

    gpMatrix[1] = TypeTwoFunction.ApplyTypeOneOperationInplace(
       gpMatrix[1],
       gpMatrix[2],
      chromosome.T2Chromosome[3]
    );

    gpMatrix[2] = TypeTwoFunction.ApplyTypeOneOperationInplace(
       gpMatrix[2],
       gpMatrix[3],
      chromosome.T2Chromosome[4]
    );

    // 4. Top type 1 function layer
    gpMatrix[0] = TypeTwoFunction.ApplyTypeOneOperationInplace(
       gpMatrix[0],
       gpMatrix[1],
      chromosome.T2Chromosome[0]
    );

    gpMatrix[1] = TypeTwoFunction.ApplyTypeOneOperationInplace(
       gpMatrix[1],
       gpMatrix[2],
      chromosome.T2Chromosome[1]
    );

    // 5. Special product of final two matrices (at depth = 2)
    return Evaluate.Fitness(in gpMatrix[0], in gpMatrix[1]);
  }

  private static int[] ApplyPopulationOnImage(in Pop[] population, in byte[][] image)
  {
    int popSize = population.Length;
    int[] fitness = new int[popSize];

    for (int i = 0; i < popSize; i++)
    {
      fitness[i] = ApplyChromosomeOnImage(in population[i], in image);
    }

    return fitness;
  }

  public static int[] RunGenetricProgramming(in Pop[] population, in List<byte[][]> images)
  {
    int popSize = population.Length;
    int[] fitness = new int[popSize];

    for (int i = 0; i < images.Count; i++)
    {
      int[] singleImageFitness = ApplyPopulationOnImage(in population, images[i]);
      for (int j = 0; j < singleImageFitness.Length; j++) { fitness[i] += singleImageFitness[i]; }
    }

    return fitness;
  }
}
