internal class Program
{
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("../../../input.txt");
        var seqs = new List<List<long>>();
        foreach (var line in lines)
        {
            var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var cur = new List<long>();
            foreach (var part in parts)
            {
                cur.Add(long.Parse(part));
            }
            seqs.Add(cur);
        }

        var minStep = 1;
        var maxStep = 3;

        var safeCountPart1 = seqs
            .Select(x =>
                IsSequenceSafe(minStep, maxStep, x)
                || IsSequenceSafe(-maxStep, -minStep, x)
            )
            .Count(x => x);
        Console.WriteLine(safeCountPart1);

        var safeCountPart2 = 0;
        foreach (var seq in seqs)
        {
            var oneIsSafe = false;
            for (var i = 0; i <= seq.Count; i++)  // inclusing max index + 1, to account for original seq
            {
                var newSeq = seq.Where((_, idx) => idx != i).ToList();
                if (
                    IsSequenceSafe(minStep, maxStep, newSeq)
                    || IsSequenceSafe(-maxStep, -minStep, newSeq)
                )
                {
                    oneIsSafe = true;
                    break;
                }
            }
            if (oneIsSafe)
            {
                safeCountPart2++;
            }
        }
        Console.WriteLine(safeCountPart2);
    }

    private static bool IsSequenceSafe(int minStep, int maxStep, List<long> seq)
    {
        return seq
            .Zip(seq.Skip(1), (prev, cur) => cur - prev)
            .All(diff => minStep <= diff && diff <= maxStep);
    }
}
