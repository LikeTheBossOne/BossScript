using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BossScript.DataStructures
{
    internal class SymbolTable
    {
        public string Scope { get; }
        public SymbolTable Parent { get; }
        public Dictionary<string, SymbolTableEntry> Table { get; }

        public SymbolTable(string scope, SymbolTable parent)
        {
            Scope = scope;
            Parent = parent;
            Table = new Dictionary<string, SymbolTableEntry>();
        }

        public SymbolTableEntry this[string symbol] => Table[symbol];
    }
}
