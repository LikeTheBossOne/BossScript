using Lexer.BossScriptParser;

namespace Lexer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = 
@"dorrie is 
1 
fine puppy.";
            BossScript.Run(input);
        }
    }
}