#pragma warning disable 8605

public static class Day9Main
{
  const string Filename = "data/day9" +
    // "example" +
    // "2" +
    ".txt";

  public static Dictionary<char, IVector2> moves = new()
  {
    ['U'] = (0, 1),
    ['R'] = (1, 0),
    ['D'] = (0, -1),
    ['L'] = (-1, 0)
  };

  public static string Part1()
  {
    var file = File.ReadAllLines(Filename);
    return RopeSim(2, file).ToString();
  }

  public static string Part2()
  {
    var file = File.ReadAllLines(Filename);
    return RopeSim(10, file).ToString();
  }

  public static int RopeSim(int length, string[] file)
  {
    IVector2[] rope = new IVector2[length];

    DictionaryGenerator<IVector2, int> tailVisited = new(
      new Dictionary<IVector2, int>() { [(0, 0)] = 1 },
      new SingleValueGenerator<IVector2, int>(0)
    );

    foreach (var line in file)
    {
      IVector2 move = moves[line[0]];
      int count = int.Parse(line[2..]);

      foreach (int _ in Enumerable.Range(0, count))
      {
        rope[0] += move;

        foreach (int i in Enumerable.Range(1, length - 1))
        {
          IVector2 space = rope[i];
          IVector2 following = rope[i - 1];
          IVector2 movement = CatchUp(space, following);

          if (movement != IVector2.Identity)
          {
            space += movement;
            rope[i] = space;

            if (i == length - 1)
            {
              tailVisited[space] += 1;
            }
          }
          else break;
        }
      }
    }

    return tailVisited.Count;
  }

  static IVector2 CatchUp(IVector2 from, IVector2 to)
  {
    IVector2 offset = to - from;
    IVector2 move = offset.Sign();

    if (move != offset) return move;
    else return (0, 0);
  }
}

public struct IVector2
{
  public int X;
  public int Y;

  public static implicit operator IVector2(System.ValueTuple<int, int> input) => new IVector2() { X = input.Item1, Y = input.Item2 };
  public static implicit operator System.ValueTuple<int, int>(IVector2 input) => (input.X, input.Y);
  public static IVector2 operator +(IVector2 left, IVector2 right) => (left.X + right.X, left.Y + right.Y);
  public static IVector2 operator -(IVector2 input) => (-input.X, -input.Y);
  public static IVector2 operator -(IVector2 left, IVector2 right) => left + -right;
  public static IVector2 operator *(IVector2 left, int right) => (left.X * right, left.Y * right);

  public static bool operator ==(IVector2 left, IVector2 right) => left.Equals(right);
  public static bool operator !=(IVector2 left, IVector2 right) => !(left == right);

  public IVector2 Sign() => (Math.Sign(X), Math.Sign(Y));

  public override int GetHashCode() => (((int X, int Y))this).GetHashCode();
  public override bool Equals(object? obj) => (((int X, int Y))this).Equals(((int X, int Y))(IVector2)obj);
  public override string ToString() => (((int X, int Y))this).ToString();

  public static IVector2 Identity = (0, 0);
}
