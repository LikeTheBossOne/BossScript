#define TRACE_ACTIONS

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BossScript
{
    internal partial class BossScriptParser
    {
        public BossScriptParser(BossScriptScanner scanner) : base(scanner) { }

        public BossScriptParser() : base(null) { }

        public ParserVal RootVal { get; private set; }

        public void Run(string input)
        {
            byte[] inputBuffer = Encoding.Default.GetBytes(input);
            MemoryStream stream = new MemoryStream(inputBuffer);
            var scanner = new BossScriptScanner(stream);
            Scanner = scanner;

            bool success = Parse();
            if (success)
            {
                Console.WriteLine("No errors");
            }
        }

        public static ParserVal MakeNode(String symbol, int rule, params ParserVal[] parserVals)
        {
            Tree[] trees = new Tree[parserVals.Length];
            for (int i = 0; i < trees.Length; i++)
            {
                trees[i] = (Tree)parserVals[i].obj;
            }

            return new ParserVal{ obj = new Tree(symbol, rule, trees) };
        }
    }
}
