using Neptunite.Convolution;
using Neptunite.Fitness;
using Neptunite.Generate;
using Neptunite.Image;
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
  private static int ApplyChromosomeOnImage(in Pop chromosome, in Image.Matrix image, in bool isImageWithFeature)
  {
    // Copy image for different convolutions
    Image.Matrix[] gpMatrix = new Image.Matrix[BaseLayerWidth];
    for (int i = 0; i < BaseLayerWidth; i++) { gpMatrix[i] = image; }

    for (int i = 0; i < BaseLayerWidth; i++)
    {
      // 1. Convolution
      gpMatrix[i] = Convolve.TwoMatrices(gpMatrix[i], chromosome.ConvolutionChromosome[i]);

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
    return Evaluate.Fitness(in gpMatrix[0], in gpMatrix[1], in isImageWithFeature);
  }

  private static int[] ApplyPopulationOnImage(in Pop[] population, in Image.Matrix image, in bool isImageWithFeature)
  {
    int popSize = population.Length;
    int[] fitness = new int[popSize];

    for (int i = 0; i < popSize; i++)
    {
      fitness[i] = ApplyChromosomeOnImage(in population[i], in image, in isImageWithFeature);
    }

    return fitness;
  }

  public static int[] EvaluatePopulation(in Pop[] population, in Images images)
  {
    int popSize = population.Length;
    int[] fitness = new int[popSize];

    bool isImageWithFeature = true;
    for (int i = 0; i < images.WithFeature.Count; i++)
    {
      Image.Matrix image = Parse.AsGrayscaleImageMatrix(images.WithFeature[i]);
      int[] singleImageFitness = ApplyPopulationOnImage(in population, in image, in isImageWithFeature);
      for (int j = 0; j < singleImageFitness.Length; j++) { fitness[i] += singleImageFitness[i]; }
    }

    isImageWithFeature = false;
    for (int i = 0; i < images.WithoutFeature.Count; i++)
    {
      Image.Matrix image = Parse.AsGrayscaleImageMatrix(images.WithoutFeature[i]);
      int[] singleImageFitness = ApplyPopulationOnImage(in population, in image, in isImageWithFeature);
      for (int j = 0; j < singleImageFitness.Length; j++) { fitness[i] += singleImageFitness[i]; }
    }

    return fitness;
  }
}
