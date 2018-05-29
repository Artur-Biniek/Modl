using Modl.Common.Instructions;

namespace Modl.Common {
    public static class OpCodes {
        public static Instruction Halt => new ZeroOperandInstruction (OpCode.Halt);
        public static Instruction ConstInt (int n) {
            return new OneOperandInstruction<int> (OpCode.CIntN, n);
        }
        public static Instruction ConstIntZero = new ZeroOperandInstruction (OpCode.CInt0);
        public static Instruction ConstIntOne = new ZeroOperandInstruction (OpCode.CInt1);
        public static Instruction Call(int functionIndex) {
             return new OneOperandInstruction<int> (OpCode.Call, functionIndex);
        }
        public static Instruction Ret = new ZeroOperandInstruction (OpCode.Ret);
        public static Instruction Pop = new ZeroOperandInstruction (OpCode.Pop);
    }
}