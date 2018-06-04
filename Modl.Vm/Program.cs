using System;
using System.Linq;
using Antlr4.Runtime;
using Modl.Common;
using Modl.Vm.Asm.Antlr;
using Modl.Vm.Asm;

namespace Modl.Vm {
    class Program {
        static void Main (string[] args) {            
            var input = new AntlrFileStream ("Modl.Vm\\test.modl");
            var lexer = new AsmLexer (input);
            var tokens = new CommonTokenStream (lexer);
            var parser = new AsmParser (tokens);
            var tree = parser.program();
            var visit = new AsmAstBuilder();
            visit.VisitProgram(tree);
            
            var vm = new VirtualMachine (visit.Program, visit.Functions);

            vm.Execute (true);
        }
    }
}