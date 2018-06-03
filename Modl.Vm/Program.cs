using System;
using System.Linq;
using Antlr4.Runtime;
using Modl.Common;
using Modl.Vm.Asm.Antlr;
using Modl.Vm.Asm;

namespace Modl.Vm {
    class Program {
        static void Main (string[] args) {
            var prg = new [] {
               OpCodes.ConstIntOne, 
                    OpCodes.Print,
                    OpCodes.ConstInt (12),
                    OpCodes.ConstInt (15),
                    OpCodes.Call (1),
                    OpCodes.Print,
                    OpCodes.Halt,

                    OpCodes.LoadArg(0),
                    OpCodes.LoadArg(1),
                    OpCodes.SubInt,
                    OpCodes.Ret,
            }.SelectMany (i => i.GetBytes ()).ToArray ();

            var functions = new [] {
                new FunctionDescriptor ("main", 0),                
                new FunctionDescriptor ("sum", 19, 2, 0)
            };


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