using System.Collections.Generic;

namespace Modl.Common.Instructions {
    public abstract class Instruction {
        public OpCode OpCode { get; }
        public Instruction (OpCode opcode) {
            OpCode = opcode;
        }

        public virtual IEnumerable<byte> GetBytes () {
            yield return (byte) OpCode;
        }
    }
}