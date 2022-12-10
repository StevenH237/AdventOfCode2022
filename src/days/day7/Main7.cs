#pragma warning disable

public static class Day7Main
{
  const string Filename = "data/day7" +
    // "example" +
    ".txt";

  static bool WasRun = false;
  static string Part1Answer = "";
  static string Part2Answer = "";

  public static void Run()
  {
    WasRun = true;
    var file = File.ReadAllLines(Filename);

    Dictionary<string, Entity> allEntities = new();

    Entity root = new()
    {
      IsDir = true,
      Size = 0,
      Children = new(),
      Parent = null
    };

    Entity current = root;

    foreach (string line in file)
    {
      // We'll ignore "$ ls" because we're assuming all command *output* already is that.
      if (line == "$ ls") continue;

      if (line == "$ cd /")
      {
        current = root;
        continue;
      }

      if (line == "$ cd ..")
      {
        current = current.Parent;
        continue;
      }

      if (line.StartsWith("$ cd "))
      {
        current = current.Children[line[5..]];
        continue;
      }

      if (line.StartsWith("dir "))
      {
        Entity newDir = new()
        {
          IsDir = true,
          Size = 0,
          Parent = current
        };

        current.Children[line[4..]] = newDir;
        continue;
      }

      var bits = line.Split(' ');

      int size = int.Parse(bits[0]);
      string filename = bits[1];

      Entity newFile = new()
      {
        IsDir = false,
        Size = size,
        Parent = current
      };

      current.Children[filename] = newFile;

      for (Entity ent = current; ent != null; ent = ent.Parent)
      {
        ent.Size += size;
      }
    }

    // Console.WriteLine($"Total size: {root.Size}");
    // Console.WriteLine($"[PART 1 ANSWER] Total size of dirs under 10k: {GetDirsUnder10k(root)}");
    // Console.WriteLine($"This should match the above: {GetAllDirSizes(root).Where(x => x < 100000).Sum()}");

    // Console.WriteLine($"/ (dir, size={root.Size})");
    // PrintDirTree(root).Do(x => Console.WriteLine(x));

    int freeSpace = 70000000 - root.Size;
    int neededSpace = 30000000 - freeSpace;
    // Console.WriteLine($"Needed space: {neededSpace}");
    // Console.WriteLine($"[PART 2 ANSWER] Smallest dir over needed space: {GetAllDirSizes(root).Where(x => x > neededSpace).OrderBy(x => x).First()}");

    Part1Answer = GetAllDirSizes(root).Where(x => x < 100000).Sum().ToString();
    Part2Answer = GetAllDirSizes(root).Where(x => x > neededSpace).OrderBy(x => x).First().ToString();
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

  static IEnumerable<int> GetAllDirSizes(Entity root)
  {
    yield return root.Size;
    foreach (int size in root.Children.Values.Where(x => x.IsDir).SelectMany(x => GetAllDirSizes(x)))
    {
      yield return size;
    }
  }

  static int GetDirsUnder10k(Entity root)
  {
    int total = 0;

    if (root.Size <= 100000) total += root.Size;

    total += root.Children.Values
      .Where(x => x.IsDir)
      .Select(GetDirsUnder10k)
      .Sum();

    return total;
  }

  static int SmallestDirOverSize(Entity root, int neededSpace)
  {
    return root.Children.Values
      .Where(x => x.Size > neededSpace)
      .Select(x => SmallestDirOverSize(x, neededSpace))
      .OrderBy(x => x)
      .FirstOrDefault(int.MaxValue);
  }

  static IEnumerable<string> PrintDirTree(Entity root)
  {
    foreach (var item in root.Children)
    {
      yield return $"├╴{item.Key} ({(item.Value.IsDir ? "dir" : "file")}, size={item.Value.Size})";
      if (item.Value.IsDir)
      {
        foreach (var line in PrintDirTree(item.Value))
        {
          yield return $"│ {line}";
        }
      }
    }
  }
}

internal class Entity
{
  internal bool IsDir;
  internal int Size;
  internal Dictionary<string, Entity> Children = new();
  internal Entity Parent;
}