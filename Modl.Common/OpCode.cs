
namespace Modl.Common {
    public enum OpCode : byte  {
        Halt = 0,
        CIntN = 10,
        CInt0 = 11,
        CInt1 = 12,

        AddInt = 30,
        SubInt = 31,
        MulInt = 32,
        DivInt = 33,
        ModInt = 34,

        Call = 100,
        Ret = 101,
        Pop = 102,
    }
}