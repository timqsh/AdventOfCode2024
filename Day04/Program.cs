namespace Day04;

internal class Program
{
    static void Main(string[] args)
    {
        var inputLines = File.ReadAllLines("../../../input.txt");
        var linesToCheck = new List<List<char>>();

        var height = inputLines.Length;
        var width = inputLines[0].Length;

        // Horizontal lines
        for (var y = 0; y < height; y++)
        {
            var right = new List<char>();
            var left = new List<char>();
            for (var x = 0; x < width; x++)
            {
                right.Add(inputLines[y][x]);
                left.Add(inputLines[y][width - x - 1]);
            }
            linesToCheck.Add(right);
            linesToCheck.Add(left);
        }
        // Vertical lines
        for (var x = 0; x < width; x++)
        {
            var down = new List<char>();
            var up = new List<char>();
            for (var y = 0; y < height; y++)
            {
                down.Add(inputLines[y][x]);
                up.Add(inputLines[height - y - 1][x]);
            }
            linesToCheck.Add(down);
            linesToCheck.Add(up);
        }

        // Diagonals
        for (var start = 0; start < height + width - 1; start++)
        {
            var diag = new List<char>();
            var antidiag = new List<char>();
            for (var y = Math.Max(0, start - width + 1); y < Math.Min(height, start + 1); y++)
            {
                var x = start - y;
                if (x >= 0 && x < width)
                {
                    diag.Add(inputLines[y][x]);
                }
                x = width - 1 - (start - y);
                if (x >= 0 && x < width)
                {
                    antidiag.Add(inputLines[y][x]);
                }
            }
            if (diag.Count > 0)
            {
                linesToCheck.Add(diag);
                linesToCheck.Add(diag.AsEnumerable().Reverse().ToList());
            }
            if (antidiag.Count > 0)
            {
                linesToCheck.Add(antidiag);
                linesToCheck.Add(antidiag.AsEnumerable().Reverse().ToList());
            }
        }


        var totalCount = 0;
        foreach (var line in linesToCheck)
        {
            for (var i = 0; i < line.Count - 3; i++)
            {
                if (line[i] == 'X' && line[i + 1] == 'M' && line[i + 2] == 'A' && line[i + 3] == 'S')
                {
                    totalCount += 1;
                }
            }
        }
        Console.WriteLine(totalCount);

        var part2 = 0;
        for (var x = 0; x < width - 2; x++)
        {
            for (var y = 0; y < height - 2; y++)
            {
                if (inputLines[y + 1][x + 1] != 'A')
                {
                    continue;
                }
                var target = new HashSet<char> { 'M', 'S' };
                var main = new HashSet<char> { inputLines[y + 2][x + 2], inputLines[y][x] };
                var anti = new HashSet<char> { inputLines[y + 2][x], inputLines[y][x + 2] };

                if (main.SetEquals(target) && anti.SetEquals(target))
                {
                    part2++;
                }
            }
        }
        Console.WriteLine(part2);
    }
}
