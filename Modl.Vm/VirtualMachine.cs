using System;
using System.Linq;
using Modl.Common;
using Modl.Vm.Exceptions;

namespace Modl.Vm {
    internal sealed class VirtualMachine {
        private readonly byte[] _program;

        private int __IP;
        private int __SP;
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
                var oldIp = __IP;
                var inst = (OpCode)_program[__IP++];

                switch (inst) {
                    case OpCode.CInt0:
                        if (__SP >= _stack.Length) throw new OperandStackOverflowException ();
                        _stack[__SP++] = 0;
                        break;

                    case OpCode.CInt1:
                        if (__SP >= _stack.Length) throw new OperandStackOverflowException ();
                        _stack[__SP++] = 1;
                        break;

                    case OpCode.Halt:
                        return;
                }

                if (trace) {
                    Console.WriteLine ($"{oldIp,5} {inst,-10} STACK: [{formatArray(_stack, __SP)}]");
                    Console.WriteLine ($"{' ',-16} CALLS: [{formatArray(_stack, __SP)}]");
                }
            }
        }

        static string formatArray (object[] arr, int cnt) {
            return string.Join (", ", arr.Take (cnt));
        }
    }
}