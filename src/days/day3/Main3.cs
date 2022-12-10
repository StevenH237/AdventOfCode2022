public static class Day3Main
{
  const string Filename = "data/day3" +
    // "example" +
    ".txt";

  public static string Part1()
  {
    var file = File.ReadAllLines(Filename);

    int score = 0;

    foreach (string line in file)
    {
      int len = line.Length;
      string left = line[0..(len / 2)];
      string right = line[(len / 2)..^0];
      char shared = left.Where(x => right.Contains(x)).Distinct().Single();

      score += ((int)shared) + 1;

      if (shared >= 'A' && shared <= 'Z')
      {
        score -= ((int)'A');
        score += 26;
      }
      else
      {
        score -= ((int)'a');
      }
    }

    return score.ToString();
  }

  public static string Part2()
  {
    var file = File.ReadAllLines(Filename);

    int groupScore = 0;

    foreach (var chunk in file.Chunk(3))
    {
      string first = chunk[0];
      string second = chunk[1];
      string third = chunk[2];

      char shared = first.Where(x => second.Contains(x) && third.Contains(x)).Distinct().Single();
      groupScore += ((int)shared) + 1;

      if (shared >= 'A' && shared <= 'Z')
      {
        groupScore -= ((int)'A');
        groupScore += 26;
      }
      else
      {
        groupScore -= ((int)'a');
      }

      // Console.WriteLine($"Part 2: The total group priority score is {groupScore}.");
    }

    return groupScore.ToString();
  }
}