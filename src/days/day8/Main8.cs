public static class Day8Main
{
  const string Filename = "data/day8" +
    // "example" +
    ".txt";

  public static string Part1()
  {
    var file = File.ReadAllLines(Filename);

    Grid<int> map = new Grid<int>(
      file.Select(line => line.Select(
          chr => int.Parse(chr.ToString())
        )
      )
    );

    int w = map.Width;
    int h = map.Height;

    Grid<bool> visible = new Grid<bool>(w, h);

    var visibleFromWest = map.Rows.SelectMany((x, r) => x.Select((v, c) => (V: v, R: r, C: c))
      .WhereOrderedBy(x => x.V, distinctly: true));
    var visibleFromEast = map.Rows.SelectMany((x, r) => x.Reverse().Select((v, c) => (V: v, R: r, C: w - c - 1))
      .WhereOrderedBy(x => x.V, distinctly: true));
    var visibleFromNorth = map.Columns.SelectMany((x, c) => x.Select((v, r) => (V: v, R: r, C: c))
      .WhereOrderedBy(x => x.V, distinctly: true));
    var visibleFromSouth = map.Columns.SelectMany((x, c) => x.Reverse().Select((v, r) => (V: v, R: h - r - 1, C: c))
      .WhereOrderedBy(x => x.V, distinctly: true));

    int trees = 0;

    foreach (var tree in visibleFromWest.Concat(visibleFromEast).Concat(visibleFromNorth).Concat(visibleFromSouth))
    {
      // Console.Write($"Tree visible at {tree.R}, {tree.C}: ");
      var marked = visible[tree.R, tree.C];

      if (!marked)
      {
        // Console.WriteLine("New! Marking...");
        trees++;
        visible[tree.R, tree.C] = true;
      }
      else
      {
        // Console.WriteLine("Already marked.");
      }
    }

    return trees.ToString();
  }

  public static string Part2()
  {
    var file = File.ReadAllLines(Filename);

    Grid<int> map = new Grid<int>(
      file.Select(line => line.Select(
          chr => int.Parse(chr.ToString())
        )
      )
    );

    int w = map.Width;
    int h = map.Height;

    // Skip the border because it's always 0
    int highscore = 0;

    foreach (int r in Enumerable.Range(1, h - 2))
    {
      foreach (int c in Enumerable.Range(1, w - 2))
      {
        int tree = map[r, c];

        int north = 0;
        foreach (int rv in Enumerable.Range(1, r))
        {
          north++;
          if (map[r - rv, c] >= tree) break;
        }

        int south = 0;
        foreach (int rv in Enumerable.Range(r + 1, h - (r + 1)))
        {
          south++;
          if (map[rv, c] >= tree) break;
        }

        int west = 0;
        foreach (int cv in Enumerable.Range(1, c))
        {
          west++;
          if (map[r, c - cv] >= tree) break;
        }

        int east = 0;
        foreach (int cv in Enumerable.Range(c + 1, w - (c + 1)))
        {
          east++;
          if (map[r, cv] >= tree) break;
        }

        int score = north * south * west * east;
        if (score > highscore) highscore = score;
      }
    }

    return highscore.ToString();
  }
}