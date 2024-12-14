internal class Program
{
    private static void Main(string[] args)
    {
        var lines = File.ReadAllLines("../../../input.txt");
        var (nums1, nums2) = ParseInputLines(lines);

        var totalDist = CalculateDistances(nums1, nums2);
        var simScore = CalculateScores(nums1, nums2);

        Console.WriteLine($"Part 1: {totalDist}");
        Console.WriteLine($"Part 2: {simScore}");
    }

    private static long CalculateDistances(List<long> nums1, List<long> nums2)
    {
        nums1.Sort();
        nums2.Sort();

        return nums1
            .Zip(nums2, (x, y) => Math.Abs(x - y))
            .Sum();
    }

    private static long CalculateScores(List<long> nums1, List<long> nums2)
    {
        var counter = nums2
            .GroupBy(x => x)
            .ToDictionary(g => g.Key, g => g.LongCount());

        return nums1
            .Aggregate(
                0L,
                (acc, x) => acc + x * counter.GetValueOrDefault(x)
            );
    }

    private static (List<long> nums1, List<long> nums2) ParseInputLines(string[] lines)
    {
        var nums1 = new List<long>();
        var nums2 = new List<long>();
        foreach (string line in lines)
        {
            string[] parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (parts.Length != 2)
            {
                throw new FormatException($"Expected a line with 2 numbers, but got: {line}");
            }
            var p1 = Convert.ToInt64(parts[0]);
            var p2 = Convert.ToInt64(parts[1]);
            nums1.Add(p1);
            nums2.Add(p2);
        }
        return (nums1, nums2);
    }
}