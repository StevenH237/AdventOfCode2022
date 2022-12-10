public static class Day5Main
{
  const string Filename = "data/day5" +
    // "example" +
    ".txt";

  static Regex rgxMove = new Regex(@"^move (\d+) from (\d+) to (\d+)$");

  static bool WasRun = false;
  static string Part1Answer = "";
  static string Part2Answer = "";

  public static void Run()
  {
    WasRun = true;
    var file = File.ReadAllLines(Filename);

    string[] drawing = file.TakeWhile(x => x.Any()).SkipLast(1).ToArray();
    string[] instructions = file.Skip(drawing.Length + 2).ToArray();

    List<List<char>> stacks1 = GetStacks(drawing);
    List<List<char>> stacks2 = new List<List<char>>(stacks1.Select(x => new List<char>(x)));

    // Quick pause for debugging
    // What's the answer if we didn't move any boxes?
    Console.WriteLine($"The starting configuration of the boxes is {stacks1.Select(stack => stack[0]).FormString()}");

    // Now let's form the answers
    foreach (string line in instructions)
    {
      // We'll use this bit in both parts
      Match mtc = rgxMove.Match(line);

      int count = int.Parse(mtc.Groups[1].Value);
      int from = int.Parse(mtc.Groups[2].Value) - 1;
      int to = int.Parse(mtc.Groups[3].Value) - 1;

      // Here's the answer to part 1:
      List<char> stack1From = stacks1[from];
      List<char> stack1To = stacks1[to];

      foreach (int i in Enumerable.Range(0, count))
      {
        char element = stack1From[0];
        stack1From.RemoveAt(0);
        stack1To.Insert(0, element);
      }

      // Here's the answer to part 2:
      List<char> stack2From = stacks2[from];
      List<char> stack2To = stacks2[to];

      var cratesMoved = stack2From.Take(count).ToArray();
      stack2From.RemoveRange(0, count);
      stack2To.InsertRange(0, cratesMoved);
    }

    Part1Answer = stacks1.Select(stack => stack[0]).FormString();
    Part2Answer = stacks2.Select(stack => stack[0]).FormString();
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

  static List<List<char>> GetStacks(string[] drawing)
  {
    Grid<char> boxGrid = new(drawing.Select(
      line => line.Chunk(4).Select(
        box => box[1]
      )
    ));

    return boxGrid.Columns.Select(
      stack => stack.Where(chr => chr != ' ').ToList()
    ).ToList();
  }
}