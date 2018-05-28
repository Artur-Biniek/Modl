using System;
using Modl.Common;

namespace Modl.Vm
{
    class Program
    {
        static void Main(string[] args)
        {            
            var vm = new VirtualMachine(new [] {OpCodes.ConstIntOne, OpCodes.ConstIntZero, OpCodes.Halt});
            vm.Execute(true);
            
            Console.WriteLine($"Hello Modl! Opcode for ConstInt is {OpCodes.ConstInt}.");
        }
    }
}
