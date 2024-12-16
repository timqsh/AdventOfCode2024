using System.Text.RegularExpressions;

namespace Day03;

internal class Program
{
    static void Main(string[] args)
    {
        var text = File.ReadAllText("../../../input.txt");
        var matches = Regex.Matches(text, @"(?:mul\((\d+),(\d+)\))|(?:do\(\))|(?:don't\(\))");
        var result = 0;
        var enabled = true;
        foreach (Match match in matches)
        {
            if (match.Value.StartsWith("do()"))
            {
                enabled = true;
                continue;
            }
            if (match.Value.StartsWith("don't"))
            {
                enabled = false;
                continue;
            }
            if (!enabled)
            {
                continue;
            }
            var first = match.Groups[1].Value;
            var second = match.Groups[2].Value;
            result += Convert.ToInt32(first) * Convert.ToInt32(second);
        }
        Console.WriteLine(result);
    }
}
