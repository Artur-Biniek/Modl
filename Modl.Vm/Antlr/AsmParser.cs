//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.7.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from e:\Dev\Repos\Modl\Modl.Vm\Asm.g4 by ANTLR 4.7.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7.1")]
[System.CLSCompliant(false)]
public partial class AsmParser : Parser {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1;
	public const int
		RULE_program = 0, RULE_funcDecl = 1;
	public static readonly string[] ruleNames = {
		"program", "funcDecl"
	};

	private static readonly string[] _LiteralNames = {
		null, "'def'"
	};
	private static readonly string[] _SymbolicNames = {
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "Asm.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string SerializedAtn { get { return new string(_serializedATN); } }

	static AsmParser() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}

		public AsmParser(ITokenStream input) : this(input, Console.Out, Console.Error) { }

		public AsmParser(ITokenStream input, TextWriter output, TextWriter errorOutput)
		: base(input, output, errorOutput)
	{
		Interpreter = new ParserATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}
	public partial class ProgramContext : ParserRuleContext {
		public FuncDeclContext[] funcDecl() {
			return GetRuleContexts<FuncDeclContext>();
		}
		public FuncDeclContext funcDecl(int i) {
			return GetRuleContext<FuncDeclContext>(i);
		}
		public ProgramContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_program; } }
		public override void EnterRule(IParseTreeListener listener) {
			IAsmListener typedListener = listener as IAsmListener;
			if (typedListener != null) typedListener.EnterProgram(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IAsmListener typedListener = listener as IAsmListener;
			if (typedListener != null) typedListener.ExitProgram(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IAsmVisitor<TResult> typedVisitor = visitor as IAsmVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitProgram(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ProgramContext program() {
		ProgramContext _localctx = new ProgramContext(Context, State);
		EnterRule(_localctx, 0, RULE_program);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 5;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			do {
				{
				{
				State = 4; funcDecl();
				}
				}
				State = 7;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			} while ( _la==T__0 );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class FuncDeclContext : ParserRuleContext {
		public FuncDeclContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_funcDecl; } }
		public override void EnterRule(IParseTreeListener listener) {
			IAsmListener typedListener = listener as IAsmListener;
			if (typedListener != null) typedListener.EnterFuncDecl(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IAsmListener typedListener = listener as IAsmListener;
			if (typedListener != null) typedListener.ExitFuncDecl(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IAsmVisitor<TResult> typedVisitor = visitor as IAsmVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitFuncDecl(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public FuncDeclContext funcDecl() {
		FuncDeclContext _localctx = new FuncDeclContext(Context, State);
		EnterRule(_localctx, 2, RULE_funcDecl);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 9; Match(T__0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	private static char[] _serializedATN = {
		'\x3', '\x608B', '\xA72A', '\x8133', '\xB9ED', '\x417C', '\x3BE7', '\x7786', 
		'\x5964', '\x3', '\x3', '\xE', '\x4', '\x2', '\t', '\x2', '\x4', '\x3', 
		'\t', '\x3', '\x3', '\x2', '\x6', '\x2', '\b', '\n', '\x2', '\r', '\x2', 
		'\xE', '\x2', '\t', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x2', '\x2', 
		'\x4', '\x2', '\x4', '\x2', '\x2', '\x2', '\f', '\x2', '\a', '\x3', '\x2', 
		'\x2', '\x2', '\x4', '\v', '\x3', '\x2', '\x2', '\x2', '\x6', '\b', '\x5', 
		'\x4', '\x3', '\x2', '\a', '\x6', '\x3', '\x2', '\x2', '\x2', '\b', '\t', 
		'\x3', '\x2', '\x2', '\x2', '\t', '\a', '\x3', '\x2', '\x2', '\x2', '\t', 
		'\n', '\x3', '\x2', '\x2', '\x2', '\n', '\x3', '\x3', '\x2', '\x2', '\x2', 
		'\v', '\f', '\a', '\x3', '\x2', '\x2', '\f', '\x5', '\x3', '\x2', '\x2', 
		'\x2', '\x3', '\t',
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}