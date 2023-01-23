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
        public int Lineno;

        public SimpleToken(int c, string s, int l)
        {
            Cat = c;
            Text = s;
            Lineno = l;
        }
    }
}
