using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer.BossScriptParser
{
    enum ParseType : short
    {
        BOOL = 257,
        BOOL_LIT,
        BREAK,
        CLASS,
        DOUBLE,
        ELSE,
        FOR,
        IF,
        INT,
        NULL_VAL,
        PUBLIC,
        RETURN,
        STATIC,
        STRING,
        WHILE,
        VOID,

        LESS_THAN_OR_EQUAL,
        GREATER_THAN_OR_EQUAL,
        IS_EQUAL_TO,
        NOT_EQUAL_TO,
        LOGICAL_AND,
        LOGICAL_OR,
        INCREMENT,
        DECREMENT,

        IDENTIFIER,
        INT_LIT,
        DOUBLE_LIT,
        STRING_LIT,
    }

    internal class BossScript
    {
        private static BossScriptParserScanner Scanner;
        public static int yylineno, yylcolno;
        public static SimpleToken yylval;
        public static SimpleToken LastToken;

        public static void Run(string input)
        {
            byte[] inputBuffer = System.Text.Encoding.Default.GetBytes(input);
            MemoryStream stream = new MemoryStream(inputBuffer);
            Scanner = new BossScriptParserScanner(stream);
            yylineno = 1;

            int i;
            while ((i = Scanner.yylex()) != (int)Token.EOF)
                Console.WriteLine("token" + i + " (line " + yylval.Lineno + "): " + Scanner.yytext);
        }

        public static short Ord(char character)
        {
            return (short) character;
        }

        public static int Scan(int cat)
        {
            if (cat == -1)
            {
                LexErr("invalid token type");
            }
            LastToken = yylval = new SimpleToken(cat, Scanner.yytext, yylineno);
            yylcolno += Scanner.yytext.Length;
            return cat;
        }


        public static int Scan(ParseType parseType)
        {
            return Scan((int) parseType);
        }

        public static void IncrementLineNo()
        {
            yylineno++;
        }

        public static void WhiteSpace()
        {
            yylcolno += Scanner.yytext.Length;
        }

        public static void NewLine()
        {
            yylineno++;
            yylcolno = 1;

            if (LastToken != null)
            {
                switch (LastToken.Cat)
                {
                    case (int)ParseType.IDENTIFIER:
                        break;
                    default:
                        break;
                }
            }
        }

        public static void Comment()
        {
            
        }

        public static void LexErr(string err)
        {
            Console.Error.WriteLine(err + ": line " + yylineno + ": " + Scanner.yytext);
            Environment.Exit(1);
        }
    }
}
