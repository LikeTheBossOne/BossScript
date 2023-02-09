using BossScript;

namespace BossScript
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.Error.WriteLine("Args.Length needs to be 1");
            }

            string fileText = File.ReadAllText(args[0]);
            var parser = new BossScriptParser();
            parser.Run(fileText);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Tree Report: \n\n");
            Tree root = ((Tree) parser.RootVal.obj);
            root.Print();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            root.PrintGraph("outGraph.dot");

        }
    }
}