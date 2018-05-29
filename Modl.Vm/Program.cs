using System;
using System.Linq;
using Modl.Common;

namespace Modl.Vm
{
    class Program
    {
        static void Main(string[] args)
        {            
            var prg = new [] {
                OpCodes.ConstIntOne,                
                OpCodes.Call(1),
                OpCodes.Pop,
                OpCodes.ConstIntOne,
                OpCodes.ConstInt(1459),
                OpCodes.Halt,
                OpCodes.ConstInt(666),
                OpCodes.ConstIntZero,
                OpCodes.ConstIntOne,
                OpCodes.Call(2),
                OpCodes.Pop,
                OpCodes.Ret,
                OpCodes.ConstInt(777),
                OpCodes.Ret,
            }.SelectMany (i => i.GetBytes()).ToArray();

            var functions = new[] {
                new FunctionDescriptor("main", 0),
                new FunctionDescriptor("factorial", 14, 0, 4),
                new FunctionDescriptor("sum", 28, 2, 2)
            };

            var vm = new VirtualMachine(prg, functions);

            vm.Execute(true);
        }
    }
}
