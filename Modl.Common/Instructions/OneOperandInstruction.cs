using System;
using System.Linq;
using System.Collections.Generic;

namespace Modl.Common.Instructions {
    public class OneOperandInstruction<T> : Instruction {
        public T Operand { get; }
        public OneOperandInstruction (OpCode opcode, T operand) : base (opcode) {
            Operand = operand;
        }

        public override IEnumerable<byte> GetBytes () {
            foreach (var b in base.GetBytes ()) yield return b;

            byte[] bytes = new byte[] { };
            if (Operand is int i) {
                bytes = BitConverter.GetBytes (i);
            }            

            foreach (var b in (BitConverter.IsLittleEndian ? bytes : bytes.Reverse())) yield return b;
        }
    }
}