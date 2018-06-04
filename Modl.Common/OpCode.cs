
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
        LdArg = 103,
        LdLoc = 104,
        StLoc = 105,
        Print = 106,

        Br = 150,     
        Brt = 151,
        Brf = 152, 
    }
}