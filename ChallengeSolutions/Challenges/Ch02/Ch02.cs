namespace ChallengeSolutions.Challenges.Ch02;

public static class Ch02
{
    /// <summary>
    /// A solution to the second challenge.
    /// <see href="https://adventofcode.com/2025/day/2"/>
    /// </summary>
    /// <remarks>This challenge has two parts, both have the same input but different processing</remarks>
    public static void Solve()
    {
        var input = "1061119-1154492,3-23,5180469-5306947,21571-38630,1054-2693,141-277,2818561476-2818661701,21177468-21246892,40-114,782642-950030,376322779-376410708,9936250-10074071,761705028-761825622,77648376-77727819,2954-10213,49589608-49781516,9797966713-9797988709,4353854-4515174,3794829-3861584,7709002-7854055,7877419320-7877566799,953065-1022091,104188-122245,25-39,125490-144195,931903328-931946237,341512-578341,262197-334859,39518-96428,653264-676258,304-842,167882-252124,11748-19561";

        var ranges = input.Split(',');

        Console.WriteLine("------------Part one------------");
        PartOne(ranges);
        Console.WriteLine("------------Part two------------");
        PartTwo(ranges);
    }

    private static void PartOne(string[] ranges)
    {
        var invalids = new List<long>();
        foreach (var range in ranges) // range is like 11-22
        {
            var rangeNums = range.Split('-').Select(long.Parse).ToList();
            for (var num = rangeNums[0]; num <= rangeNums[1]; num++) 
                // Looping between each range, like from 11 to 22
            {
                var numStr = num.ToString();
                
                // Getting the two halves of the current numbers' string => "1212" => "12" "12"
                var firstHalf = numStr.Substring(0, numStr.Length / 2);
                var secondHalf = numStr.Substring(numStr.Length / 2);
                
                // If the halves are equal the id is invalid
                if(firstHalf == secondHalf)
                {
                    Console.WriteLine($"{num}: {firstHalf} + {secondHalf}");
                    invalids.Add(num);
                }
            }
        }
        
        Console.WriteLine($"Sum of all invalid ids: {invalids.Sum()} - count: {invalids.Count}");
    }
    
    private static void PartTwo(string[] ranges)
    {
        var invalids = new List<long>();
        
        foreach (var range in ranges) // range is like 11-22
        {
            var rangeNums = range.Split('-').Select(long.Parse).ToList();
            for (var num = rangeNums[0]; num <= rangeNums[1]; num++)
                // Looping between each range, like from 11 to 22
            {
                var numStr = num.ToString();
                for (var division = 2; division <= numStr.Length; division++)
                    // Keep dividing the current number until it's a match or the end of the number.
                {
                    // If the number was odd and the division was even, it's definitely not a match
                    if(division % 2 == 0 && numStr.Length % 2 != 0) continue; 
                    
                    var currentLength = numStr.Length / division;
                    
                    // Chunk the number string to batches of currentLength
                    // if j = 3, number = 123_123_123 then the result is a list of three "123"s
                    var currentBatch = numStr.Chunk(currentLength).Select(c => new string(c)).ToList();

                    // If all the elements of the list weren't equal, it's not a match
                    if (!currentBatch.TrueForAll(c => c == currentBatch[0])) continue;
                    
                    // Else it's a match
                    invalids.Add(num);
                    break;
                }
            }
        }
        
        Console.WriteLine($"Sum of all invalid ids: {invalids.Sum()} - count: {invalids.Count}");
    }
}