using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BossScript.DataStructures
{
    internal class Tree
    {
        private static int NextID = 0;

        public int ID { get; set; }

        public string Symbol { get; set; }
        public int Rule { get; set; }
        public Token Token { get; set; }

        public int NumKids { get; }
        public Tree[] Kids { get; }

        // Attributes
        public bool IsConst { get; }
        public Dictionary<string, string> SymbolTableRef { get; }


        public Tree(string symbol, int rule, Token token)
        {
            ID = NextID++;
            Symbol = symbol;
            Rule = rule;
            Token = token;
            Kids = null;
            NumKids = 0;
        }

        public Tree(string symbol, int rule, Tree[] kids)
        {
            ID = NextID++;
            Symbol = symbol;
            Rule = rule;
            Kids = kids;
            NumKids = kids.Length;
        }

        public void Print(int level)
        {
            for (int i = 0; i < level; i++)
            {
                Console.Write(" ");
            }

            if (Token != null)
            {
                Console.WriteLine(ID + "   " + Token.YYText +  " (" + Token.Type + "): " + Token.YYLineNo);
            }
            else
            {
                Console.WriteLine(ID + "   " + Symbol + " (" + Rule + "): " + NumKids);
            }

            for (int i = 0; i < NumKids; i++)
            {
                Kids[i].Print(level + 1);
            }
        }

        public void Print()
        {
            Print(0);
        }

        public void PrintGraph(string fileName)
        {
            try
            {
                StreamWriter writer = new StreamWriter(fileName);
                writer.WriteLine("digraph {");
                int j = 0;
                PrintGraph(writer, ref j);
                writer.WriteLine("}");
                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void PrintGraph(StreamWriter writer, ref int j)
        {
            if (Token != null)
            {
                PrintLeaf(writer);
                return;
            }
            PrintBranch(writer);
            for (int i = 0; i < NumKids; i++)
            {
                if (Kids[i] != null)
                {
                    writer.WriteLine($"N{ID} -> N{Kids[i].ID};");
                    Kids[i].PrintGraph(writer, ref j);
                }
                else
                {
                    writer.WriteLine($"N{ID} -> N{Kids[i].ID}{j};");
                    writer.WriteLine($"N{ID}{j} [label=\"Empty rule\"];");
                    j++;
                }
            }
        }

        private void PrintLeaf(StreamWriter writer)
        {
            PrintBranch(writer);
            writer.Write($"N{ID} [shape=box style=dotted label=\" {Token.Type} \\n");
            writer.WriteLine($"text = {Escape(Token.YYText)} \\l lineno = {Token.YYLineNo} \\l\"];");
        }

        private void PrintBranch(StreamWriter writer)
        {
            writer.WriteLine($"N{ID} [shape=box label=\"{PrettyPrintName()}\"];");
        }

        private string PrettyPrintName()
        {
            if (Token == null)
                return Symbol + "#" + (Rule % 10);
            else
                return Escape(Token.YYText) + ":" + (int) Token.Type;
        }

        private string Escape(String s)
        {
            if (s[0] == '\"')
            {
                return "\\" + s.Substring(0, s.Length - 1) + "\\\"";
            }
            else
            {
                return s;
            }
        }
    }
}
