namespace Modl.Common.Instructions {
    public class OneOperandInstruction<T> : Instruction {
        public T Operand {get;}
        public OneOperandInstruction (OpCode opcode, T operand) : base (opcode) {
            Operand = operand;
        }
    }
}