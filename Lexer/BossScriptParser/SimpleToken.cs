using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer.BossScriptParser
{
    internal class SimpleToken
    {
        public int Cat;
        public string Text;
        public int LineNo;

        
        public int IVal;
        public string SVal;
        public double DVal;

        public SimpleToken(int c, string s, int ln)
        {
            Cat = c;
            Text = s;
            LineNo = ln;
            switch (Cat)
            {
                case (int)ParseType.INT_LIT:
                    IVal = int.Parse(s);
                    break;
                case (int)ParseType.DOUBLE_LIT:
                    DVal = double.Parse(s);
                    break;
                case (int)ParseType.STRING_LIT:
                    SVal = DeEscape(s);
                    break;
            }

        }

        private string DeEscape(string input)
        {
            StringBuilder outBuilder = new StringBuilder();
            input = input.Substring(1,input.Length - 1);

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (c == '\\')
                {
                    if (input.Length < i + 1)
                    {
                        BossScript.LexErr("malformed string literal");
                    }
                    else
                    {
                        c = input[++i];
                        switch (c)
                        {
                            case 't':
                                outBuilder.Append('\t');
                                break;
                            case 'n':
                                outBuilder.Append("\n");
                                break;
                            default:
                                BossScript.LexErr("unrecognized escape");
                                break;
                        }
                    }
                }
                else
                {
                    outBuilder.Append(c);
                }
            }

            return outBuilder.ToString();
        }
    }
}
