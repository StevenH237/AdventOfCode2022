public static class Day6Main
{
  const string Filename = "data/day6" +
    // "example" +
    ".txt";

  public static string Part1()
  {
    string input = File.ReadAllText(Filename);

    return Enumerable.Range(0, input.Length - 3)
      .Select(x => (Index: x + 4, Chars: input[x..(x + 4)]))
      .Where(x => x.Chars.Distinct().Count() == 4)
      .First().Index.ToString();
  }

  public static string Part2()
  {
    string input = File.ReadAllText(Filename);

    return Enumerable.Range(0, input.Length - 13)
      .Select(x => (Index: x + 14, Chars: input[x..(x + 14)]))
      .Where(x => x.Chars.Distinct().Count() == 14)
      .First().Index.ToString();
  }
}