using System;
using System.Collections.Generic;
using System.Text;

namespace Lexer.BossScriptParser
{
    internal partial class BossScriptParserScanner
    {

        void GetNumber()
        {
        }

		public override void yyerror(string format, params object[] args)
		{
			base.yyerror(format, args);
			Console.WriteLine(format, args);
			Console.WriteLine();
		}
    }
}
