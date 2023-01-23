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

        SIZE
    }

    internal class BossScript
    {
        private static BossScriptParserScanner Scanner;
        public static int yylineno, yycolno;
        public static SimpleToken yylval;

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
            switch (character)
            {
                case '(':
                    return (int) ParseType.SIZE + 1;
                case ')':
                    return (int)ParseType.SIZE + 2;
                case '[':
                    return (int)ParseType.SIZE + 3;
                case ']':
                    return (int)ParseType.SIZE + 4;
                case '{':
                    return (int)ParseType.SIZE + 5;
                case '}':
                    return (int)ParseType.SIZE + 6;
                case ';':
                    return (int)ParseType.SIZE + 7;
                case ':':
                    return (int)ParseType.SIZE + 8;
                case '!':
                    return (int)ParseType.SIZE + 9;
                case '*':
                    return (int)ParseType.SIZE + 10;
                case '/':
                    return (int)ParseType.SIZE + 11;
                case '%':
                    return (int)ParseType.SIZE + 12;
                case '+':
                    return (int)ParseType.SIZE + 13;
                case '-':
                    return (int)ParseType.SIZE + 14;
                case '<':
                    return (int)ParseType.SIZE + 15;
                case '>':
                    return (int)ParseType.SIZE + 16;
                case '=':
                    return (int)ParseType.SIZE + 17;
                case ',':
                    return (int)ParseType.SIZE + 18;
                case '.':
                    return (int)ParseType.SIZE + 19;
                default:
                    return -1;
            }
        }

        public static int Scan(int cat)
        {
            if (cat == -1)
            {
                LexErr("invalid token type");
            }

            yylval = new SimpleToken(cat, Scanner.yytext, yylineno);
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
