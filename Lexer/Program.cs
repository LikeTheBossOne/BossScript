using Lexer.BossScriptParser;

namespace Lexer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = 
@"public class hello {
    public static void main(String argv[]) {
        System.out.println(""hello, jzero!"");
    }
}
";
            BossScript.Run(input);
        }
    }
}