using System;
using System.Collections.Generic;
using Modl.Common;

namespace Modl.Vm.Asm {
    public class FunctionDefinition {
        private readonly List<byte> _program;

        public FunctionDescriptor Descriptor { get; set; }

        private Dictionary<string, int> Labels { get; }
        private Dictionary<string, List<int>> ForwardRefs { get; }

        public FunctionDefinition (List<byte> program) {
            _program = program;

            Labels = new Dictionary<string, int> ();
            ForwardRefs = new Dictionary<string, List<int>> ();
        }

        public int GetLabel (string label) {
            if (Labels.ContainsKey (label)) {
                return Labels[label];
            } else {
                if (!ForwardRefs.ContainsKey (label)) {
                    ForwardRefs[label] = new List<int> ();
                }

                ForwardRefs[label].Add (_program.Count);

                return int.MaxValue;
            }
        }

        public void MarkLabel (string label) {
            if (Labels.ContainsKey (label)) {
                throw new Exception ($"Label [{label}] already defined.");
            }

            Labels[label] = _program.Count;
        }

        public void VerifyForwardRefs () {
            foreach (var fwd in ForwardRefs.Keys) {
                if (!Labels.ContainsKey (fwd)) {
                    throw new Exception ($"Label [{fwd}] was never defined");
                }

                var address = Labels[fwd];
                var bytes = Utils.GetBytes (address);

                foreach (var target in ForwardRefs[fwd]) {                   
                    for (int i = 0; i < bytes.Length; i++) {
                        _program[i + target] = bytes[i];
                    }
                }
            }
        }
    }
}