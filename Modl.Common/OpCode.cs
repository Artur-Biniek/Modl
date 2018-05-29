
namespace Modl.Common {
    public enum OpCode : byte  {
        Halt = 0,
        CIntN = 10,
        CInt0 = 11,
        CInt1 = 12,

        Call = 100,
        Ret = 101,
        Pop = 102,
    }
}