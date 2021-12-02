using System;
using System.IO;
using System.Linq;

var input = File.ReadLines(@"input.txt").Select(x => new Command(x)).ToList();

Part1();
Part2();

void Part1()
{
    var submarine = new Submarine();

    foreach (var command in input)
    {
        submarine.ExecutePart1Command(command);
    }
    
    Console.WriteLine($"The answer to part 1 is: {submarine.HorizontalPosition * submarine.Depth}");
}

void Part2()
{
    var submarine = new Submarine();

    foreach (var command in input)
    {
        submarine.ExecutePart2Command(command);
    }
    
    Console.WriteLine($"The answer to part 2 is: {submarine.HorizontalPosition * submarine.Depth}");
}

class Command
{
    public Command(string cmd)
    {
        var parts = cmd.Split(' ');
        Direction = parts[0];
        AmountOfUnits = int.Parse(parts[1]);
    }
    
    public string Direction { get; }
    public int AmountOfUnits { get; }
}

class Submarine
{
    public int HorizontalPosition { get; private set; }
    public int Depth { get; private set; }
    public int Aim { get; private set; }

    public void ExecutePart1Command(Command cmd)
    {
        switch (cmd.Direction)
        {
            case "forward":
                HorizontalPosition += cmd.AmountOfUnits;
                break;
            case "down":
                Depth += cmd.AmountOfUnits;
                break;
            case "up":
                Depth -= cmd.AmountOfUnits;
                break;
        }
    }
    
    public void ExecutePart2Command(Command cmd)
    {
        switch (cmd.Direction)
        {
            case "forward":
                HorizontalPosition += cmd.AmountOfUnits;
                Depth += Aim * cmd.AmountOfUnits;
                break;
            case "down":
                Aim += cmd.AmountOfUnits;
                break;
            case "up":
                Aim -= cmd.AmountOfUnits;
                break;
        }
    }
}