using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var input = File.ReadLines(@"input.txt").Select(x => new BinaryNumber(x)).ToList();

var numbers = new Report(input);
Console.WriteLine($"The answer to part 1 is: {numbers.GetPowerConsumption()}");
Console.WriteLine($"The answer to part 2 is: {numbers.GetLifeSupportRating()}");

class Report
{
    private readonly List<BinaryNumber> _numbers;

    public Report(List<BinaryNumber> numbers)
    {
        _numbers = numbers;
    }
    
    public (int commonBit, int uncommonBit) GetCommonBitAt(List<BinaryNumber> numbers, int position)
    {
        var zeroBits = numbers.Where(x => x.GetBitAt(position) == 0).ToList();
        var oneBits = numbers.Where(x => x.GetBitAt(position) == 1).ToList();

        var commonBit = zeroBits.Count > oneBits.Count ? 0 : 1;
        var uncommonBit = zeroBits.Count <= oneBits.Count ? 0 : 1;

        return (commonBit, uncommonBit);
    }

    public int GetPowerConsumption()
    {
        var gammaRate = string.Empty;
        var epsilonRate = string.Empty;

        for (var i = 0; i < _numbers.First().Length; i++)
        {
            gammaRate += GetCommonBitAt(_numbers, i).commonBit.ToString();
            epsilonRate += GetCommonBitAt(_numbers, i).uncommonBit.ToString();
        }

        var gamma = Convert.ToInt32(gammaRate, 2);
        var epsilon = Convert.ToInt32(epsilonRate, 2);
        return gamma * epsilon;
    }

    public int GetLifeSupportRating()
    {
        int? oxygenGeneratorRating = null;
        int? co2ScrubberRating = null;

        var tempNumbersForOxygenGeneratorRating = _numbers;
        var tempNumbersForCo2ScrubberRating = _numbers;
        
        for (var i = 0; i <= _numbers.First().Length; i++)
        {
            if (oxygenGeneratorRating == null)
            {
                if (tempNumbersForOxygenGeneratorRating.Count == 1)
                {
                    oxygenGeneratorRating = Convert.ToInt32(tempNumbersForOxygenGeneratorRating.Single().Value, 2);
                }
                else
                {
                    tempNumbersForOxygenGeneratorRating = tempNumbersForOxygenGeneratorRating
                        .Where(x => x.GetBitAt(i) == GetCommonBitAt(tempNumbersForOxygenGeneratorRating, i).commonBit).ToList();
                }
            }

            if (co2ScrubberRating == null)
            {
                if (tempNumbersForCo2ScrubberRating.Count == 1)
                {
                    co2ScrubberRating = Convert.ToInt32(tempNumbersForCo2ScrubberRating.Single().Value, 2);
                }
                else
                {
                    tempNumbersForCo2ScrubberRating = tempNumbersForCo2ScrubberRating
                        .Where(x => x.GetBitAt(i) == GetCommonBitAt(tempNumbersForCo2ScrubberRating, i).uncommonBit).ToList();
                }
            }
        }
        
        return oxygenGeneratorRating!.Value * co2ScrubberRating!.Value;
    }
}

class BinaryNumber
{
    private readonly string _number;

    public BinaryNumber(string number)
    {
        _number = number;
    }

    public int Length => _number.Length;
    
    public string Value => _number;

    public int GetBitAt(int position)
    {
        return int.Parse(_number[position].ToString());
    }
}