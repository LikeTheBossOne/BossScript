using System;
using System.Collections.Generic;
using System.Text;

namespace BossScript.BossScript
{
    enum ParseType : short
    {
        BREAK = 129, DOUBLE, ELSE, FOR, IF, INT, RETURN, VOID, WHILE,
        IDENTIFIER, CLASSNAME, CLASS, STRING, BOOL,
        INT_LIT, DOUBLE_LIT, STRING_LIT, BOOL_LIT, NULL_VAL,
        LESS_THAN_OR_EQUAL, GREATER_THAN_OR_EQUAL,
        IS_EQUAL_TO, NOT_EQUAL_TO, LOGICAL_AND, LOGICAL_OR,
        INCREMENT, DECREMENT,

        PUBLIC,
        STATIC,
    }

    internal partial class BossScriptScanner
    {

        //public static int YYLineno, YYColno;
        //public static SimpleToken YYLval;
        public int LastToken;

        public int Ord(char character)
        {
            return (int)character;
        }

        public int Scan(int cat)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Token value is: " + yytext);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            if (cat == -1)
            {
                LexErr("invalid token type");
            }
            LastToken = yylval = cat;
            return cat;
        }


        public int Scan(ParseType parseType)
        {
            return Scan((int)parseType);
        }


        public void WhiteSpace()
        {
            cCol += yytext.Length;
        }

        public bool NewLine()
        {
            lNum++;
            cCol = 1;

            switch (LastToken)
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

            return false;
        }

        public int Semicolon()
        {
            //tokTxt = ";";
            //lNum--;
            //return Scan(';');
            return 0;
        }

        public void Comment()
        {
            for (int i = 0; i < yytext.Length; i++)
            {
                if (yytext[i] == '\n')
                {
                    lNum++;
                    cCol = 1;
                }
                else
                {
                    cCol++;
                }
            }
        }

        public void LexErr(string err)
        {
            Console.Error.WriteLine(err + ": line " + lNum + ": " + yytext);
            Environment.Exit(1);
        }

        public override void yyerror(string format, params object[] args)
		{
			base.yyerror(format, args);
			Console.WriteLine(format);
			Console.WriteLine();
        }

    }
}
