namespace Neptunite.Image;
internal class Matrix
{
  public int Height { get; }
  public int Width { get; }
  public int[][] Data { get; set; }

  public Matrix(int[][] data)
  {
    Height = data.Length;
    Width = data[0].Length;
    Data = data;
  }

  public void Modify(int value, int i, int j) => Data[i][j] = value;
  public int ReadValue(int i, int j) => Data[i][j];
  public int[] ReadRow(int i) => Data[i];
}
