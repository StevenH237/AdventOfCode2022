public static class Day2Main
{
  const string Filename = "data/day2" +
    // "example" +
    ".txt";

  static Dictionary<char, Item> Theirs = new()
  {
    ['A'] = Item.Rock,
    ['B'] = Item.Paper,
    ['C'] = Item.Scissors
  };

  static Dictionary<Result, int> Scores = new()
  {
    [Result.Loss] = 0,
    [Result.Tie] = 3,
    [Result.Win] = 6
  };

  public static string Part1()
  {
    int scoreXR_YP_ZS = GetTotalScoreP1(new()
    {
      ['X'] = Item.Rock,
      ['Y'] = Item.Paper,
      ['Z'] = Item.Scissors
    });

    // Console.WriteLine($"With X = Rock, Y = Paper, Z = Scissors, you score {scoreXR_YP_ZS}.");
    return scoreXR_YP_ZS.ToString();
  }

  public static int GetTotalScoreP1(Dictionary<char, Item> Yours)
  {
    int score = 0;

    foreach (string line in File.ReadAllLines(Filename))
    {
      Item TheirMove = Theirs[line[0]];
      Item YourMove = Yours[line[2]];
      Result MoveResult = (Result)NumberUtils.NNMod(TheirMove - YourMove, 3);

      score += (int)YourMove + Scores[MoveResult];
    }

    return score;
  }

  public static string Part2()
  {
    int scoreXL_YT_ZW = GetTotalScoreP2(new()
    {
      ['X'] = Result.Loss,
      ['Y'] = Result.Tie,
      ['Z'] = Result.Win
    });

    // Console.WriteLine($"With X = Lose, Y = Tie, Z = Win, you score {scoreXL_YT_ZW}.");
    return scoreXL_YT_ZW.ToString();
  }

  static int GetTotalScoreP2(Dictionary<char, Result> Yours)
  {
    int score = 0;

    foreach (string line in File.ReadAllLines(Filename))
    {
      Item TheirMove = Theirs[line[0]];
      Result MoveResult = Yours[line[2]];

      int YourMoveInt = (int)TheirMove - (int)MoveResult;
      if (YourMoveInt <= 0) YourMoveInt += 3;

      Item YourMove = (Item)YourMoveInt;

      score += (int)YourMove + Scores[MoveResult];
    }

    return score;
  }
}

public enum Item
{
  Rock = 1,
  Paper = 2,
  Scissors = 3
}

public enum Result
{
  Tie = 0,
  Loss = 1,
  Win = 2
}

/* Alright let's do some working out on the RPS factor here.

I think it'd be fastest to just determine win/loss with a mod. And I think
subtraction is the way to go here.

Your R - their R = tie (1 - 1 = 0)
Your R - their P = loss (1 - 2 = -1 or 2)
Your R - their S = win (1 - 3 = -2 or 1)

Your P - their R = win (2 - 1 = 1)
Your P - their P = tie (2 - 2 = 0)
Your P - their S = loss (2 - 3 = -1 or 2)

Your S - their R = loss (3 - 1 = 2)
Your S - their P = win (3 - 2 = 1)
Your S - their S = tie (3 - 3 = 0)

... Actually, if I swap that around, I can make the win 2, which is more
personally satisfying. So subtract *yours* from *theirs* (theirs - yours).

== PART 2 ==

I think it still takes subtraction and modulus.

Their R(1) - Your W(2) = -1 (=> 2) = Your P
Their R(1) - Your T(0) = 1 = Your R
Their R(1) - Your L(1) = 0 (=> 3) = Your S

Their P(2) - Your W(2) = 0 (=> 3) = Your S
Their P(2) - Your T(0) = 2 = Your P
Their P(2) - Your L(1) = 1 = Your R

Their S(3) - Your W(2) = 1 = Your R
Their S(3) - Your T(0) = 3 = Your S
Their S(3) - Your L(1) = 2 = Your P

Yup, that seems about right to me.

*/