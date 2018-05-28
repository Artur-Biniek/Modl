using System;
using Modl.Common;

namespace Modl.Vm {
    internal sealed class VirtualMachine {
        private readonly byte[] _program;

        private int __IP;
        private int __FP;
        private int __AP;

        private readonly object[] _stack;
        private readonly ActivationRecord[] _calls;

        public VirtualMachine (byte[] program, int maxOperandStack = 1024, int maxCallStack = 1024, int maxGlobals = 0) {
            _program = program;

            _stack = new object[maxOperandStack];
            _calls = new ActivationRecord[maxCallStack];
        }

        public void Execute (bool trace = false) {
            while (true && __IP < _program.Length) {
                var inst = _program[__IP];

                switch (inst) {


                    case OpCodes.Halt:
                        return;
                }
            }
        }
    }
}