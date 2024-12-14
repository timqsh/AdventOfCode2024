using ClassLibrary1;


internal class Program
{
    private static void Main(string[] args)
    {
        string s = CommonClass.GetCommonString();
        Console.WriteLine(s + " in second console app");

        // Read lines from input.txt file
        string[] lines = File.ReadAllLines("../../../input.txt");
        for (int i = 0; i < lines.Length; i++)
        {
            Console.WriteLine(i + lines[i]);

        }
    }
}