public static class Day10Main
{
  const string Filename = "data/day10" +
    // "example" +
    ".txt";

  public static string Part1()
  {
    var file = File.ReadAllLines(Filename);

    int sum = EnumerateCyclesP1(file)
      .Where(x => x.Cycle % 40 == 20)
      .Select(x => x.Cycle * x.X)
      .Sum();

    return sum.ToString();
  }

  static IEnumerable<(int Cycle, int X)> EnumerateCyclesP1(string[] instructions)
  {
    int x = 1;
    int cycle = 0;

    foreach (string line in instructions)
    {
      if (line == "noop")
      {
        yield return (++cycle, x);
      }
      else if (line.StartsWith("addx "))
      {
        // addx instruction does nothing on the first cycle
        yield return (++cycle, x);

        // The change is effective on the second cycle
        int change = int.Parse(line[5..]);
        x += change;
        yield return (++cycle, x);
      }
    }
  }

  public static string Part2()
  {
    var file = File.ReadAllLines(Filename);
    return "Not written yet!";
  }
}