using System.Numerics;

namespace Day06;

internal class Program
{
    static void Main()
    {
        var lines = File.ReadAllLines("../../../input.txt");
        Parse(lines, out HashSet<Vector2> walls, out Vector2 curPos, out int maxY, out int maxX);

        var vistedCount = Iterate(curPos, walls, maxX, maxY);
        Console.WriteLine($"part 1: {vistedCount}");

        var cycledCount = 0;
        for (int i = 0; i < maxX; i++)
        {
            for (int j = 0; j < maxY; j++)
            {
                var newWall = new Vector2(i, j);
                if (newWall == curPos || walls.Contains(newWall))
                {
                    continue;
                }

                walls.Add(newWall);
                var visitedCount = Iterate(curPos, walls, maxX, maxY);
                if (visitedCount == -1)
                {
                    cycledCount++;
                }
                walls.Remove(newWall);
            }
        }
        Console.WriteLine($"part 2: {cycledCount}");
    }

    private static void Parse(string[] lines, out HashSet<Vector2> walls, out Vector2 curPos, out int maxY, out int maxX)
    {
        walls = new HashSet<Vector2>();
        curPos = new Vector2(-1, -1);
        maxY = lines.Length;
        maxX = lines[0].Length;
        for (var y = 0; y < maxY; y++)
        {
            for (var x = 0; x < maxX; x++)
            {
                var ch = lines[y][x];
                if (ch == '^')
                {
                    curPos = new Vector2(x, y);
                }
                else if (ch == '#')
                {
                    walls.Add(new Vector2(x, y));
                }
            }
        }
    }

    static int Iterate(Vector2 curPos, HashSet<Vector2> walls, int maxX, int maxY)
    {
        var prevStates = new HashSet<(Vector2, int)>();
        var visitedPos = new HashSet<Vector2>();
        var directions = new List<Vector2> { new(0, -1), new(1, 0), new(0, 1), new(-1, 0) };
        var dirIdx = 0;

        while (0 <= curPos.X && curPos.X < maxX && 0 <= curPos.Y && curPos.Y < maxY)
        {
            visitedPos.Add(curPos);

            if (prevStates.Contains((curPos, dirIdx)))
            {
                return -1;
            }
            prevStates.Add((curPos, dirIdx));

            var nextPos = curPos + directions[dirIdx];
            while (walls.Contains(nextPos))
            {
                dirIdx = (dirIdx + 1) % 4;
                nextPos = curPos + directions[dirIdx];
            }
            curPos = nextPos;
        }
        return visitedPos.Count;
    }
}
