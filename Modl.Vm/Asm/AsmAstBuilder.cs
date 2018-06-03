using System;
using System.Collections.Generic;
using System.Linq;
using Modl.Common;
using Modl.Common.Instructions;
using Modl.Vm.Asm.Antlr;

namespace Modl.Vm.Asm {
    public class AsmAstBuilder : AsmBaseVisitor<object> {

        private List<byte> _program = new List<byte> ();
        private Dictionary<string, FunctionDescriptor> _functions = new Dictionary<string, FunctionDescriptor> ();
        private FunctionDescriptor _currentFunction;

        public byte[] Program => _program.ToArray();
        public FunctionDescriptor[] Functions => _functions.Values.ToArray();

        public override object VisitProgram (AsmParser.ProgramContext context) {
            Console.WriteLine ("Found program.");

            base.VisitChildren (context);

            // validate functions            

            return null;
        }

        public override object VisitFun (AsmParser.FunContext context) {

            var name = context.ID ().GetText ();
            var argsCnt = 0;
            var localsCnt = 0;

            Console.WriteLine ("Found function: " + context.ID ().GetText ());

            var args = context.args ();

            if (args != null) {
                var pairs = args.pair ().ToDictionary (p => p.ID ().GetText (), p => int.Parse (p.NUM ().GetText ()));

                pairs.TryGetValue ("args", out argsCnt);
                pairs.TryGetValue ("locals", out localsCnt);
            }

            _currentFunction = new FunctionDescriptor (name, _program.Count, argsCnt, localsCnt);

            if (_functions.TryGetValue (name, out var tmp)) {
                if (tmp.Address != -1) {
                    throw new Exception ($"Function with name [{name}] already defined. Line: {context.ID().Symbol.Line}.");
                }
            }

            _functions[name] = _currentFunction;

            foreach(var st in context.stats().stat()) VisitStat(st);

            // validate labels
            return null;
        }

        public override object VisitStat (AsmParser.StatContext ctx) {
            Console.WriteLine ("\tFound stat: " + ctx.GetText ());

            var zeroOp = ctx.KEYWORD_NOOP ();

            if (zeroOp != null) {
                switch (zeroOp.GetText ()) {
                    case "hlt":
                        _program.AddRange (OpCodes.Halt.GetBytes ());
                        return null;

                    case "add":
                        _program.AddRange (OpCodes.AddInt.GetBytes ());
                        return null;

                    case "ret":
                        _program.AddRange (OpCodes.Ret.GetBytes ());
                        return null;

                    case "print":
                        _program.AddRange (OpCodes.Print.GetBytes ());
                        return null;                        

                    default:
                        throw new Exception ($"Unknown [{zeroOp.GetText()}] instruction.");
                        _program.Add (0);
                }
            }

            var oneOp = ctx.KEYWORD_SINGLE ();

            if (oneOp != null) {
                switch (oneOp.GetText ()) {

                    case "int":
                        {
                            var arg = int.Parse (ctx.operand ().NUM ().GetText ());
                            Instruction inst;
                            if (arg == 0) {
                                inst = OpCodes.ConstIntZero;
                            } else if (arg == 1) {
                                inst = OpCodes.ConstIntOne;
                            } else {
                                inst = OpCodes.ConstInt (arg);
                            }

                            _program.AddRange (inst.GetBytes ());

                            return null;
                        }

                    case "lda":
                        {
                            var arg = int.Parse (ctx.operand ().NUM ().GetText ());

                            if (_currentFunction.ArgumentsCount < arg) {
                                throw new Exception ($"Can't load {arg} argument in function {_currentFunction.Name}.");
                            }

                            _program.AddRange (OpCodes.LoadArg (arg).GetBytes ());

                            return null;
                        }

                    case "call":
                        {
                            var arg = ctx.operand ().ID ().GetText ();

                            int index = -1;

                            if (!_functions.ContainsKey (arg)) {
                                _functions.Add (arg, new FunctionDescriptor (arg, -1, -1, -1));
                            }

                            var ind = Array.IndexOf (_functions.Keys.ToArray (), arg);

                            _program.AddRange (OpCodes.Call (ind).GetBytes ());

                            return null;
                        }

                    default:
                        throw new Exception ($"Unknown [{oneOp.GetText()}] instruction.");
                }
            }

            return null;
        }
    }
}