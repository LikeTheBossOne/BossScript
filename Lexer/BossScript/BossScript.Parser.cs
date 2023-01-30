#define TRACE_ACTIONS

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BossScript.BossScript
{
    internal partial class BossScriptParser
    {
        public BossScriptParser(BossScriptScanner scanner) : base(scanner) { }

        public BossScriptParser() : base(null) { }

        public void Run(string input)
        {
            byte[] inputBuffer = Encoding.Default.GetBytes(input);
            MemoryStream stream = new MemoryStream(inputBuffer);
            var scanner = new BossScriptScanner(stream);
            this.Scanner = scanner;

            bool success = this.Parse();
            if (success)
            {
                Console.WriteLine("No errors");
            }
        }
    }
}
