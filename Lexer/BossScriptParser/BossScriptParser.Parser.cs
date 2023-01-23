#define TRACE_ACTIONS

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lexer.BossScriptParser
{
    internal partial class BossScriptParserParser
    {
        public BossScriptParserParser() : base(null) { }

        public void Parse(string s)
        {
            byte[] inputBuffer = System.Text.Encoding.Default.GetBytes(s);
            MemoryStream stream = new MemoryStream(inputBuffer);
            this.Scanner = new BossScriptParserScanner(stream);
            this.Parse();
        }
    }
}
