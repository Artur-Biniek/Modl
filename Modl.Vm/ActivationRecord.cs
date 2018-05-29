namespace Modl.Vm{
    internal sealed class ActivationRecord {
        public int Function {get;}
        public int ReturnAddress {get;}
        public ActivationRecord(int funcIndex, int returnAddress)
        {
            Function =   funcIndex; 
            ReturnAddress = returnAddress;
        }
    }
}