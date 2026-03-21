grammar ParserANTLR;

program: enum_decl* EOF;
enum_decl: ENUM ID LBRACE case_list RBRACE SEMI ;
case_list: case_decl * ;
case_decl: CASE ID SEMI;

ENUM: 'enum';
CASE: 'case';
LBRACE: '{';
RBRACE: '}';
SEMI: ';';
ID: [a-zA-Z_][a-zA-Z0-9_] * ;
WS: [ \t\r\n] + ->skip;

ANY: . ;