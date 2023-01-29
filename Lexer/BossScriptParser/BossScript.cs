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

        private static string? _yytextInternal;
        public static string YYText
        {
            get => _yytextInternal ?? Scanner.yytext;
            set => _yytextInternal = value;
        }

        public static int YYLineno, YYColno;
        public static SimpleToken YYLval;
        public static SimpleToken LastToken;

        public static void Run(string input)
        {
            byte[] inputBuffer = Encoding.Default.GetBytes(input);
            MemoryStream stream = new MemoryStream(inputBuffer);
            Scanner = new BossScriptParserScanner(stream);
            YYLineno = 1;

            int i;
            while ((i = Scanner.yylex()) != (int) Token.EOF)
            {
                Console.WriteLine("token" + i + " (line " + YYLval.LineNo + "): " + YYText);

                _yytextInternal = null;
            }
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
            LastToken = YYLval = new SimpleToken(cat, YYText, YYLineno);
            return cat;
        }


        public static int Scan(ParseType parseType)
        {
            return Scan((int) parseType);
        }
        

        public static void WhiteSpace()
        {
            YYColno += YYText.Length;
        }

        public static bool NewLine()
        {
            YYLineno++;
            YYColno = 1;

            if (LastToken != null)
            {
                switch (LastToken.Cat)
                {
                    case (int)ParseType.IDENTIFIER:
                    case (int)ParseType.INT_LIT:
                    case (int)ParseType.DOUBLE_LIT:
                    case (int)ParseType.STRING_LIT:
                    case (int)ParseType.BREAK:
                    case (int)ParseType.RETURN:
                    case (int)ParseType.INCREMENT:
                    case (int)ParseType.DECREMENT:
                    case ')':
                    case ']':
                    case '}':
                        return true;
                }
            }

            return false;
        }

        public static int Semicolon()
        {
            YYText = ";";
            YYLineno--;
            return Scan(';');
        }

        public static void Comment()
        {
            for (int i = 0; i < YYText.Length; i++)
            {
                if (YYText[i] == '\n')
                {
                    YYLineno++;
                    YYColno = 1;
                }
                else
                {
                    YYColno++;
                }
            }
        }

        public static void LexErr(string err)
        {
            Console.Error.WriteLine(err + ": line " + YYLineno + ": " + YYText);
            Environment.Exit(1);
        }
    }
}
