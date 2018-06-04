using System;
using System.Collections.Generic;
using System.Linq;
using Modl.Common;
using Modl.Vm.Asm.Antlr;

namespace Modl.Vm.Asm {
    public class AsmAstBuilder : AsmBaseVisitor<object> {

        private List<byte> _program = new List<byte> ();
        private Dictionary<string, FunctionDescriptor> _functions = new Dictionary<string, FunctionDescriptor> ();
        private FunctionDefinition _currentFunction;

        public byte[] Program => _program.ToArray ();
        public FunctionDescriptor[] Functions => _functions.Values.ToArray ();

        public override object VisitProgram (AsmParser.ProgramContext context) {
            Console.WriteLine ("Found program.");

            base.VisitChildren (context);

            foreach (var item in _functions.Values) {
                if (item.Address == -1) {
                    throw new Exception ($"Undefined function [{item.Name}]");
                }
            }

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

            _currentFunction = new FunctionDefinition (_program) { Descriptor = new FunctionDescriptor (name, _program.Count, argsCnt, localsCnt) };

            if (_functions.TryGetValue (name, out var tmp)) {
                if (tmp.Address != -1) {
                    throw new Exception ($"Function with name [{name}] already defined. Line: {context.ID().Symbol.Line}.");
                }
            }

            _functions[name] = _currentFunction.Descriptor;

            base.VisitChildren (context.stats ());

            _currentFunction.VerifyForwardRefs ();

            return null;
        }

        public override object VisitZerOpStat (AsmParser.ZerOpStatContext ctx) {
            Console.WriteLine ("\tFound stat: " + ctx.GetText ());

            var zeroOp = ctx.KEYWORD_NOOP ();

            if (zeroOp != null) {
                switch (zeroOp.GetText ()) {
                    case "hlt":
                        _program.Add ((byte) OpCode.Halt);
                        return null;

                    case "add":
                        _program.Add ((byte) OpCode.AddInt);
                        return null;

                    case "sub":
                        _program.Add ((byte) OpCode.SubInt);
                        return null;

                    case "mul":
                        _program.Add ((byte) OpCode.MulInt);
                        return null;

                    case "div":
                        _program.Add ((byte) OpCode.DivInt);
                        return null;

                    case "mod":
                        _program.Add ((byte) OpCode.ModInt);
                        return null;                                                                        

                    case "ret":
                        _program.Add ((byte) OpCode.Ret);
                        return null;

                    case "print":
                        _program.Add ((byte) OpCode.Print);
                        return null;

                    default:
                        throw new Exception ($"Unknown [{zeroOp.GetText()}] instruction.");
                }
            }

            return null;
        }

        public override object VisitOneOpStat (AsmParser.OneOpStatContext ctx) {
            var oneOp = ctx.KEYWORD_SINGLE ();

            if (oneOp != null) {
                switch (oneOp.GetText ()) {

                    case "int":
                        {
                            var arg = int.Parse (ctx.operand ().NUM ().GetText ());
                            if (arg == 0) {
                                _program.Add ((byte) OpCode.CInt0);
                            } else if (arg == 1) {
                                _program.Add ((byte) OpCode.CInt1);
                            } else {
                                _program.Add ((byte) OpCode.CIntN);
                                _program.AddRange (Utils.GetBytes (arg));
                            }

                            return null;
                        }

                    case "lda":
                        {
                            var arg = int.Parse (ctx.operand ().NUM ().GetText ());

                            if (_currentFunction.Descriptor.ArgumentsCount < arg || arg < 0) {
                                throw new Exception ($"Can't load {arg} argument in function {_currentFunction.Descriptor.Name}.");
                            }

                            _program.Add ((byte) OpCode.LdArg);
                            _program.AddRange (Utils.GetBytes (arg));

                            return null;
                        }

                    case "ldloc":
                        {
                            var arg = int.Parse (ctx.operand ().NUM ().GetText ());

                            if (_currentFunction.Descriptor.LocalsCount < arg || arg < 0) {
                                throw new Exception ($"Can't load {arg} local in function {_currentFunction.Descriptor.Name}.");
                            }

                            _program.Add ((byte) OpCode.LdLoc);
                            _program.AddRange (Utils.GetBytes (arg));

                            return null;
                        }

                    case "stloc":
                        {
                            var arg = int.Parse (ctx.operand ().NUM ().GetText ());

                            if (_currentFunction.Descriptor.LocalsCount < arg || arg < 0) {
                                throw new Exception ($"Can't store {arg} local in function {_currentFunction.Descriptor.Name}.");
                            }

                            _program.Add ((byte) OpCode.StLoc);
                            _program.AddRange (Utils.GetBytes (arg));

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

                            _program.Add ((byte) OpCode.Call);
                            _program.AddRange (Utils.GetBytes (ind));

                            return null;
                        }

                    case "br":
                        {
                            var arg = ctx.operand ().ID ().GetText ();

                            _program.Add ((byte) OpCode.Br);
                            _program.AddRange (Utils.GetBytes (_currentFunction.GetLabel (arg)));

                            return null;
                        }

                    default:
                        throw new Exception ($"Unknown [{oneOp.GetText()}] instruction.");
                }
            }

            return null;
        }

        public override object VisitLabel (AsmParser.LabelContext ctx) {
            var name = ctx.ID ().GetText ();

            _currentFunction.MarkLabel (name);

            return null;
        }
    }
}