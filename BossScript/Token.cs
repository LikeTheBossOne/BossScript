using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BossScript
{
    internal class Token
    {
        public TokenType Type { get; }
        public string YYText { get; }
        public int YYLineNo { get; }

        public Token(TokenType type, string yyText, int yylineNo)
        {
            Type = type;
            YYText = yyText;
            YYLineNo = yylineNo;
        }
    }
}
