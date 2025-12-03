namespace ChallengeSolutions.Challenges.Ch01;

public static class Ch01
{
    /// <summary>
    /// A solution to the first challenge using circular linked lists.
    /// <see href="https://adventofcode.com/2025/day/1"/>
    /// </summary>
    public static void Solve()
    {
        var inputList = new List<(bool left, int size)>();
        ReadInputFromFile(inputList);

        var linkedList = SetupLinkedList(); // Create a LinkedList, First = 50, range [0, 99]

        var current = linkedList.First;
        var passwordCount = 0;
        foreach (var input in inputList)
        {
            var size = input.size;
            var left = input.left;

            // Optimize size to reduce unnecessary traversal
            OptimizeSize(ref size, ref left);

            // Traverse the list based on size and left/right input
            for (var i = 0; i < size; i++)
            {
                current = left
                    ? current.PreviousOrLast() // Left  <-
                    : current.NextOrFirst(); // Right ->
            }

            if (current.Value == 0)
                passwordCount++;
        }

        Console.WriteLine($"Password count is {passwordCount}");
    }

    private static void OptimizeSize(ref int size, ref bool left)
    {
        if (size == 100) // 100 size in a circle ends up in the same place
            size = 0;

        if (size > 100) // steps larger than 100 in size can lose unnecessary circulation
            size %= 100;

        if (size > 50) // steps larger than half can traverse in less steps in the opposite direction
        {
            size = 100 - size;
            left = !left;
        }
    }

    private static void ReadInputFromFile(List<(bool left, int size)> inputList)
    {
        using var fs = File.OpenRead($@"{Environment.CurrentDirectory}/Challenges/Ch01/inputs.txt");
        using var sr = new StreamReader(fs);

        while (!sr.EndOfStream) // Read the whole input from the file
        {
            var newLine = sr.ReadLine();
            var left = newLine[0] == 'L';
            var stepSize = int.Parse(newLine[1..]);
            inputList.Add((left, stepSize));
        }
    }

    private static LinkedList<int> SetupLinkedList()
    {
        var linkedList = new LinkedList<int>();
        linkedList.AddFirst(50);
        Enumerable.Range(51, 49).ToList()
            .ForEach(i => linkedList.AddLast(i));
        Enumerable.Range(0, 50).ToList()
            .ForEach(i => linkedList.AddLast(i));
        return linkedList;
    }
}

/// <summary>
/// Extension methods helping with circular linked list traversal
/// </summary>
static class CircularLinkedList
{
    public static LinkedListNode<T> NextOrFirst<T>(this LinkedListNode<T> current)
    {
        return current.Next ?? current.List.First;
    }

    public static LinkedListNode<T> PreviousOrLast<T>(this LinkedListNode<T> current)
    {
        return current.Previous ?? current.List.Last;
    }
}