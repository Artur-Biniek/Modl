namespace Modl.Vm {
    public sealed class FunctionDescriptor {

        public string Name { get; }
        public int Address { get; }
        public int ArgumentsCount { get; }
        public int LocalsCount { get; }

        public FunctionDescriptor (string name, int address, int numArgs = 0, int numLocals = 0) {
            Name = name;
            Address = address;
            ArgumentsCount = numArgs;
            LocalsCount = numLocals;
        }
    }
}