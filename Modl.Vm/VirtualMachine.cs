using System;
using System.Collections.Generic;
using System.Linq;
using Modl.Common;
using Modl.Vm.Exceptions;

namespace Modl.Vm {
    internal sealed class VirtualMachine {
        private readonly byte[] _program;
        private readonly FunctionDescriptor[] _functions;

        private int __IP;
        private int __SP;
        private int __FP;
        private int __AP;

        private readonly object[] _stack;
        private readonly ActivationRecord[] _calls;

        public VirtualMachine (byte[] program, FunctionDescriptor[] functions, int maxOperandStack = 1024, int maxCallStack = 1024) {
            _program = program;
            _functions = functions;

            _stack = new object[maxOperandStack];
            _calls = new ActivationRecord[maxCallStack];
        }

        public void Execute (bool trace = false) {
            if (__AP >= _calls.Length) throw new CallStackOverflowException ();
            _calls[__AP++] = new ActivationRecord (0, _program.Length);
            __SP = _functions[0].LocalsCount;

            while (true && __IP < _program.Length) {
                var oldIp = __IP;
                var inst = (OpCode) _program[__IP++];

                var op1 = string.Empty;

                switch (inst) {
                    case OpCode.CInt0:
                        {
                            if (__SP >= _stack.Length) throw new OperandStackOverflowException ();
                            _stack[__SP++] = 0;
                            break;
                        }

                    case OpCode.CInt1:
                        {
                            if (__SP >= _stack.Length) throw new OperandStackOverflowException ();
                            _stack[__SP++] = 1;
                            break;
                        }

                    case OpCode.CIntN:
                        {
                            if (__SP >= _stack.Length) throw new OperandStackOverflowException ();
                            int v = getIntArg (_program, __IP);
                            op1 = v.ToString ();
                            __IP += 4;
                            _stack[__SP++] = v;
                            break;
                        }

                    case OpCode.Call:
                        {
                            int v = getIntArg (_program, __IP);
                            __IP += 4;
                            _stack[__SP] = __FP;
                            __FP = __SP;
                            __SP++;
                            var fd = _functions[v];
                            _calls[__AP++] = new ActivationRecord (v, __IP);
                            __SP += fd.LocalsCount;
                            __IP = fd.Address;

                            op1 = v.ToString ();
                            break;
                        }

                    case OpCode.Ret:
                        {
                            var tmp = _stack[__SP - 1];
                            __SP = __FP - _functions[_calls[--__AP].Function].ArgumentsCount;
                            __FP = (int) _stack[__FP];
                            __IP = _calls[__AP].ReturnAddress;
                            _stack[__SP++] = tmp;
                            break;
                        }

                    case OpCode.AddInt:
                        {
                            var a = (int) _stack[__SP - 1];
                            var b = (int) _stack[__SP - 2];
                            _stack[__SP - 2] = a + b;
                            __SP--;
                            break;
                        }

                    case OpCode.SubInt:
                        {
                            var a = (int) _stack[__SP - 1];
                            var b = (int) _stack[__SP - 2];
                            _stack[__SP - 2] = a - b;
                            __SP--;
                            break;
                        }

                    case OpCode.MulInt:
                        {
                            var a = (int) _stack[__SP - 1];
                            var b = (int) _stack[__SP - 2];
                            _stack[__SP - 2] = a * b;
                            __SP--;
                            break;
                        }

                    case OpCode.DivInt:
                        {
                            var a = (int) _stack[__SP - 1];
                            var b = (int) _stack[__SP - 2];
                            _stack[__SP - 2] = a / b;
                            __SP--;
                            break;
                        }

                    case OpCode.ModInt:
                        {
                            var a = (int) _stack[__SP - 1];
                            var b = (int) _stack[__SP - 2];
                            _stack[__SP - 2] = a % b;
                            __SP--;
                            break;
                        }

                    case OpCode.Pop:
                        {
                            __SP--;
                            break;
                        }

                    case OpCode.LdArg:
                        {
                            int v = getIntArg (_program, __IP) + 1;
                            __IP += 4;

                            op1 = v.ToString ();

                            _stack[__SP++] = _stack[__FP - v];
                            break;
                        }

                    case OpCode.Print:
                        {
                            var obj = _stack[--__SP];
                            Console.WriteLine (obj);
                            break;
                        }

                    case OpCode.Halt:
                        return;
                }

                if (trace) {
                    Console.WriteLine ($"{oldIp,5} {inst,-10} {op1,10}    STACK: [{formatArray(_stack.Take(__SP))}]");
                    Console.WriteLine ($"{' ',-27}    CALLS: [{formatArray(_calls.Take(__AP).Select(c => _functions[c.Function].Name))}]");
                }
            }
        }

        static string formatArray (IEnumerable<object> arr) {
            return string.Join (", ", arr);
        }

        static int getIntArg (byte[] arr, int offset) {
            var raw = arr.Skip (offset).Take (4);
            var bytes = (BitConverter.IsLittleEndian ? raw : raw.Reverse ()).ToArray ();
            return BitConverter.ToInt32 (bytes, 0);
        }
    }
}