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
                OpCodes.ConstIntZero,
                OpCodes.Halt
            }.SelectMany (i => i.GetBytes()).ToArray();

            var vm = new VirtualMachine(prg);

            vm.Execute(true);
        }
    }
}
