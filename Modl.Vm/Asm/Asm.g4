grammar Asm;

program
    : fun+ EOF;

fun
    : '.' ID args? ':' NL+ stats;

stats
    : stat+;

stat
    : KEYWORD_NOOP (NL+) 
    | KEYWORD_SINGLE operand (NL+)
    ;

operand
    : NUM 
    | ID
    ;

args: '(' (pair (',' pair)*)? ')';

pair: ID '=' NUM;

KEYWORD_NOOP:
	'hlt'
	| 'ret'
	| 'pop'
	| 'print'
	| 'add'
	| 'sub'
	| 'mul'
	| 'div'
	| 'mod';
KEYWORD_SINGLE: 'call' | 'int' | 'lda';
ID: [a-zA-Z]+;
NUM: [0-9]+;
WS: [ \t] -> skip;
NL: '\r'? '\n';