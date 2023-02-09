using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BossScript.DataStructures
{
    internal class SymbolTableEntry
    {
        public string Symbol { get; set; }
        public SymbolTable SymbolTable { get; set; }
        public SymbolTable ParentSymbolTable { get; }
        public bool IsConst { get; set; }

        public SymbolTableEntry(String symbol, SymbolTable parent, bool isConst)
        {
            Symbol = symbol;
            ParentSymbolTable = parent;
            IsConst = isConst;
            SymbolTable = null;
        }

        public SymbolTableEntry(String symbol, SymbolTable parent, bool isConst, SymbolTable symbolTable)
        {
            Symbol = symbol;
            ParentSymbolTable = parent;
            IsConst = isConst;
            SymbolTable = symbolTable;
        }
    }
}
