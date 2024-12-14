namespace Entrypoint
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("First console app project\nOpening file");
            string[] lines = File.ReadAllLines("../../../input.txt");
            for (int i = 0; i<lines.Length; i++)
            {
                Console.WriteLine($"{i}. {lines[i]}");
            }
        }
    }
}