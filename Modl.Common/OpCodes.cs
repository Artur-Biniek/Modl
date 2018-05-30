using Modl.Common.Instructions;

namespace Modl.Common {
    public static class OpCodes {
        public static Instruction Halt => new ZeroOperandInstruction (OpCode.Halt);
        public static Instruction ConstInt (int n) {
            return new OneOperandInstruction<int> (OpCode.CIntN, n);
        }
        public static Instruction ConstIntZero = new ZeroOperandInstruction (OpCode.CInt0);
        public static Instruction ConstIntOne = new ZeroOperandInstruction (OpCode.CInt1);
        public static Instruction Call (int functionIndex) {
            return new OneOperandInstruction<int> (OpCode.Call, functionIndex);
        }
        public static Instruction Ret = new ZeroOperandInstruction (OpCode.Ret);
        public static Instruction Pop = new ZeroOperandInstruction (OpCode.Pop);

        public static Instruction AddInt = new ZeroOperandInstruction (OpCode.AddInt);
        public static Instruction SubInt = new ZeroOperandInstruction (OpCode.SubInt);
        public static Instruction MulInt = new ZeroOperandInstruction (OpCode.MulInt);
        public static Instruction DivInt = new ZeroOperandInstruction (OpCode.DivInt);
        public static Instruction ModInt = new ZeroOperandInstruction (OpCode.ModInt);
    }
}