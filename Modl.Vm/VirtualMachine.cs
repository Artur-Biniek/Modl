using System;

namespace Modl.Vm {
    internal sealed class VirtualMachine {
        private readonly byte[] _program;
        private readonly int _maxOperandStack;
        private readonly int _maxCallStack;
        public VirtualMachine (byte[] program, int maxOperandStack = 1024, int maxCallStack = 1024, int maxGlobals = 0) {
            _program = program;
            _maxOperandStack = maxOperandStack;
            _maxCallStack = maxCallStack;
        }

        public void Execute (bool trace = false) {

        }
    }
}