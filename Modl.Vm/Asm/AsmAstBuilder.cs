using System;
using Modl.Vm.Asm.Antlr;

namespace Modl.Vm.Asm {
    public class AsmAstBuilder : AsmBaseVisitor<object> {
        public override object VisitProgram (AsmParser.ProgramContext context) {
            Console.WriteLine ("Found program.");

            base.VisitChildren(context);

            return null;
        }

        public override object VisitFun (AsmParser.FunContext context) {
            Console.WriteLine ("Found function: " + context.ID ().GetText());
            base.VisitChildren(context);
            return null;
        }

        public override object VisitStat (AsmParser.StatContext ctx) {
            Console.WriteLine("\tFound stat: " + ctx.GetText());
            return null;
        }
    }
}