public static class Day1Main
{
  const string Filename = "data/day1" +
    // "example" +
    ".txt";

  public static string Part1()
  {
    var file = File.ReadAllLines(Filename);

    (int Ind, int Cals) thisElf = (1, 0);
    (int Ind, int Cals) highElf = (0, 0);

    foreach (string line in file.Append("")) // Append a blank to ensure the terminating logic always runs at the end
    {
      if (int.TryParse(line, out int cals)) { thisElf.Cals += cals; }
      else
      {
        if (thisElf.Cals > highElf.Cals) { highElf = thisElf; }
        thisElf.Ind += 1;
        thisElf.Cals = 0;
      }
    }

    // Console.WriteLine($"Elf #{highElf.Ind} has the highest total calories, with {highElf.Cals} altogether.");
    return highElf.Cals.ToString();
  }

  public static string Part2()
  {
    (int Ind, int Cals) thisElf = (1, 0);
    List<(int Ind, int Cals)> allElves = new();

    foreach (string line in File.ReadAllLines("data/day1.txt").Append("")) // Append a blank to ensure the terminating logic always runs at the end
    {
      if (int.TryParse(line, out int cals)) { thisElf.Cals += cals; }
      else
      {
        allElves.Add(thisElf);
        thisElf.Ind += 1;
        thisElf.Cals = 0;
      }
    }

    var Top3 = allElves.OrderByDescending(x => x.Cals).Take(3);

    // Console.WriteLine("The top three elves on this expedition are:");
    // Top3.Do(x => Console.WriteLine($"Elf #{x.Ind} with {x.Cals} calories."));
    // Console.WriteLine($"In total, they are carrying {Top3.Sum(x => x.Cals)} calories.");

    return Top3.Sum(x => x.Cals).ToString();
  }
}