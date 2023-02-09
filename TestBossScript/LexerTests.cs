using BossScript;
using System.Text;

namespace TestBossScript
{
    [TestClass]
    public class LexerTests
    {
        private void AssertLex(BossScriptScanner scanner, int tokenType)
        {
            int token = scanner.yylex();
            Assert.AreEqual((TokenType)tokenType, (TokenType)token);
        }

        [TestMethod]
        public void TestMethod1()
        {
            string input = 
@"public static void main(String argv[]) {
    System.out.println(""hello, BossScript"");
 }";

            byte[] inputBuffer = Encoding.Default.GetBytes(input);
            MemoryStream stream = new MemoryStream(inputBuffer);

            var scanner = new BossScriptScanner(stream);

            AssertLex(scanner, (int)TokenType.PUBLIC);
            AssertLex(scanner, (int)TokenType.STATIC);
            AssertLex(scanner, (int)TokenType.VOID);
            AssertLex(scanner, (int)TokenType.IDENTIFIER);
            AssertLex(scanner, '(');
            AssertLex(scanner, (int)TokenType.STRING);
            AssertLex(scanner, (int)TokenType.IDENTIFIER);
            AssertLex(scanner, '[');
            AssertLex(scanner, ']');
            AssertLex(scanner, ')');
            AssertLex(scanner, '{');
            AssertLex(scanner, (int)TokenType.IDENTIFIER);
            AssertLex(scanner, '.');
            AssertLex(scanner, (int)TokenType.IDENTIFIER);
            AssertLex(scanner, '.');
            AssertLex(scanner, (int)TokenType.IDENTIFIER);
            AssertLex(scanner, '(');
            AssertLex(scanner, (int)TokenType.STRING_LIT);
            AssertLex(scanner, ')');
            AssertLex(scanner, ';');
            AssertLex(scanner, '}');
        }
    }
}