using BossScript.BossScript;

namespace BossScript
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var parser = new BossScriptParser();
            parser.Run(
                @"public class hello {
    public static void main(string argv[]) {
        System.out.println(""hello, jzero!"");
    }
}"
                );
        }
    }
}