using System;
using System.Collections.Generic;

namespace Modl.Vm.Asm.Ast {
    public class ProgramNode {
        public IEnumerable<FunctionDescriptor> Functions { get; }

        public ProgramNode (IEnumerable<FunctionDescriptor> functions) {
            Functions = functions;
        }
    }
}