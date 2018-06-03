grammar Asm;

program
    : fun+ EOF
;

fun
    : '.' ID args? ':' NL+ stats
;

stats
    : stat+
;

stat
    : KEYWORD operand* (NL+)
;

operand
    : NUM
    | ID
;

args
    : '(' (pair (',' pair)*)? ')'
;

pair
    : ID '=' NUM
;

KEYWORD: 'hlt' | 'call' | 'ret' | 'pop' | 'prnt' | 'int' | 'add' | 'sub' | 'mul' | 'div' | 'mod' | 'lda' ;
ID: [a-zA-Z]+;
NUM: [0-9]+;
WS: [ \t] -> skip;
NL: '\r'? '\n';