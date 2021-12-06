using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var input = File.ReadAllText(@"input.txt");
var bingo = new Bingo(input);
var winningBoardScore = bingo.GetWinningBoardscore();

Console.WriteLine($"The answer to part 1 is: {winningBoardScore}");

class Bingo
{
    private IEnumerable<int> _allNumbers;
    private List<int> _calledNumbers = new();
    private List<Board> _boards = new();

    public Bingo(string input)
    {
        Parse(input);
    }

    public int GetWinningBoardscore()
    {
        foreach (var number in _allNumbers)
        {
            _calledNumbers.Add(number);

            foreach (var board in _boards)
            {
                if (board.HasWon(_calledNumbers))
                {
                    return board.GetScore(_calledNumbers, number);
                }
            }
        }

        return default;
    }

    private void Parse(string input)
    {
        var parts = input.Split("\n\n");
        _allNumbers = parts[0].Split(',').Select(int.Parse);

        for (var i = 1; i < parts.Length; i++)
        {
            _boards.Add(new Board(parts[i]));
        }
    }
}

class Board
{
    private const int BoardSize = 5;
    private int[,] _numbers = new int[BoardSize,BoardSize];

    public Board(string input)
    {
        var rows = input.Split("\n");

        for (var r = 0; r < rows.Length; r++)
        {
            var column = System.Text.RegularExpressions.Regex.Split(rows[r].Trim(), @"\s+");

            for (var c = 0; c < column.Length; c++)
            {
                _numbers[r,c] = int.Parse(column[c]);
            }
        }
    }

    public int Points { get; set; }

    public bool HasWon(IEnumerable<int> calledNumbers)
    {
        return WonRow(calledNumbers) || WonColumn(calledNumbers);
    }

    private bool WonRow(IEnumerable<int> calledNumbers)
    {
        for (var r = 0; r < BoardSize; r++)
        {
            var wonRow = true;

            for (var c = 0; c < BoardSize; c++)
            {
                if (!calledNumbers.Contains(_numbers[r,c]))
                {
                    wonRow = false;
                }
            }

            if (wonRow)
                return true;
        }

        return false;
    }

    private bool WonColumn(IEnumerable<int> calledNumbers)
    {
        for (var c = 0; c < BoardSize; c++)
        {
            var wonColumn = true;

            for (var r = 0; r < BoardSize; r++)
            {
                if (!calledNumbers.Contains(_numbers[r, c]))
                {
                    wonColumn = false;
                }
            }

            if (wonColumn)
                return true;
        }

        return false;
    }

    public int GetScore(IEnumerable<int> calledNumbers, int lastCalledNumber)
    {
        var score = 0;

        foreach (var number in _numbers)
        {
            if (!calledNumbers.Contains(number))
            {
                score += number;
            }
        }

        return score * lastCalledNumber;
    }
}