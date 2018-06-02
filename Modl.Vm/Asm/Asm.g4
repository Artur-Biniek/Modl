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
    : ID operand* NL+
;

operand
    : NUM
    | ID
;

args
    : '(' pair (',' pair)* ')'
;

pair
    : ID '=' NUM
;

ID: [a-zA-Z]+;
NUM: [0-9]+;
WS: [ \t] -> skip;
NL: '\r'? '\n';