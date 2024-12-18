namespace Day05;

internal class Program
{
    static void Main()
    {
        var text = File.ReadAllText("../../../input.txt");
        Parse(text, out Dictionary<int, List<int>> rules, out List<List<int>> seqs);

        var p1 = 0;
        var p2 = 0;
        foreach (var seq in seqs)
        {
            int middleIdx = (seq.Count - 1) / 2;
            if (SeqOrdered(seq, rules))
            {
                p1 += seq[middleIdx];
            }
            else
            {
                seq.Sort((x, y) => PairOrdered(x, y, rules) ? -1 : 1);
                p2 += seq[middleIdx];
            }
        }
        Console.WriteLine(p1);
        Console.WriteLine(p2);
    }

    private static void Parse(string text, out Dictionary<int, List<int>> rules, out List<List<int>> seqs)
    {
        var textParts = text.Split("\r\n\r\n");
        if (textParts.Length != 2)
        {
            throw new FormatException("not a two paragraph input");
        }
        rules = [];
        foreach (var line in textParts[0].Split("\r\n"))
        {
            var lineParts = line.Split("|");
            if (lineParts.Length != 2)
            {
                throw new FormatException($"not a 2 part line in rules: {line}");
            }
            var left = Convert.ToInt32(lineParts[0]);
            var right = Convert.ToInt32(lineParts[1]);
            if (!rules.TryGetValue(left, out List<int>? value))
            {
                value = [];
                rules[left] = value;
            }
            value.Add(right);
        }
        seqs = [];
        foreach (var line in textParts[1].Split("\r\n"))
        {
            var seq = line.Split(",").Select(s => Convert.ToInt32(s)).ToList();
            seqs.Add(seq);
        }
    }

    static bool SeqOrdered(List<int> seq, Dictionary<int, List<int>> rules)
    {
        for (var left = 0; left < seq.Count; left++)
        {
            for (var right = left + 1; right < seq.Count; right++)
            {
                if (!PairOrdered(seq[left], seq[right], rules))
                {
                    return false;
                }
            }
        }
        return true;
    }
    static bool PairOrdered(int left, int right, Dictionary<int, List<int>> rules)
    {
        if (!rules.TryGetValue(right, out List<int>? value))
        {
            return true;
        }
        if (value.Contains(left))
        {
            return false;
        }
        return true;
    }
}
