public static class Day4Main
{
  const string Filename = "data/day4" +
    // "example" +
    ".txt";

  static bool WasRun = false;
  static string Part1Answer = "";
  static string Part2Answer = "";

  public static void Run()
  {
    WasRun = true;
    var file = File.ReadAllLines(Filename);

    int answer1 = 0;
    int answer2 = 0;

    Regex rgxRanges = new Regex(@"^(\d+)-(\d+),(\d+)-(\d+)$");

    foreach (string line in file)
    {
      RegexUtils.TryMatch(rgxRanges, line, out Match mtcRanges);

      int left1 = int.Parse(mtcRanges.Groups[1].Value);
      int right1 = int.Parse(mtcRanges.Groups[2].Value);

      int left2 = int.Parse(mtcRanges.Groups[3].Value);
      int right2 = int.Parse(mtcRanges.Groups[4].Value);

      if (left1 <= left2 && right1 >= right2) answer1 += 1;
      else if (left1 >= left2 && right1 <= right2) answer1 += 1;

      if (left1 <= right2 && right1 >= left2) answer2 += 1;
      else if (left2 <= right1 && right2 >= left1) answer2 += 1;
    }

    Part1Answer = answer1.ToString();
    Part2Answer = answer2.ToString();
  }

  public static string Part1()
  {
    if (!WasRun) Run();
    return Part1Answer;
  }

  public static string Part2()
  {
    if (!WasRun) Run();
    return Part2Answer;
  }
}