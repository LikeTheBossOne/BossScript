%namespace BossScript.BossScript
%scannertype BossScriptScanner
%visibility internal
%tokentype Token

%option stack, minimize, parser, verbose, persistbuffer, noembedbuffers 

%{

%}

%%

/* Scanner body */
"/*"([^*]|"*"+[^/*])*"*"+"/"	{ Comment(); }
"//".*\r?\n						{ Comment(); }
[ \t\r\f]+						{ WhiteSpace(); }
\n								{ NewLine(); }

"bool"							{ return Scan(ParseType.BOOL); }
"break"							{ return Scan(ParseType.BREAK); }
"class"							{ return Scan(ParseType.CLASS); }
"double"						{ return Scan(ParseType.DOUBLE); }
"else"							{ return Scan(ParseType.ELSE); }
"false"							{ return Scan(ParseType.BOOL_LIT); }
"for"							{ return Scan(ParseType.FOR); }
"if"							{ return Scan(ParseType.IF); }
"int"							{ return Scan(ParseType.INT); }
"null"							{ return Scan(ParseType.NULL_VAL); }
"public"						{ return Scan(ParseType.PUBLIC); }
"return"						{ return Scan(ParseType.RETURN); }
"static"						{ return Scan(ParseType.STATIC); }
"string"						{ return Scan(ParseType.STRING); }
"true"							{ return Scan(ParseType.BOOL_LIT); }
"void"							{ return Scan(ParseType.VOID); }
"while"							{ return Scan(ParseType.WHILE); }
"("								{ return Scan(Ord('(')); }
")"								{ return Scan(Ord(')')); }
"["								{ return Scan(Ord('[')); }
"]"								{ return Scan(Ord(']')); }
"{"								{ return Scan(Ord('{')); }
"}"								{ return Scan(Ord('}')); }
";"								{ return Scan(Ord(';')); }
":"								{ return Scan(Ord(':')); }
"!"								{ return Scan(Ord('!')); }
"*"								{ return Scan(Ord('*')); }
"/"								{ return Scan(Ord('/')); }
"%"								{ return Scan(Ord('%')); }
"+"								{ return Scan(Ord('+')); }
"-"								{ return Scan(Ord('-')); }
"<"								{ return Scan(Ord('<')); }
"<="							{ return Scan(ParseType.LESS_THAN_OR_EQUAL); }
">"								{ return Scan(Ord('>')); }
">="							{ return Scan(ParseType.GREATER_THAN_OR_EQUAL); }
"=="							{ return Scan(ParseType.IS_EQUAL_TO); }
"!="							{ return Scan(ParseType.NOT_EQUAL_TO); }
"&&"							{ return Scan(ParseType.LOGICAL_AND); }
"||"							{ return Scan(ParseType.LOGICAL_OR); }
"="								{ return Scan(Ord('=')); }
"+="							{ return Scan(ParseType.INCREMENT); }
"-="							{ return Scan(ParseType.DECREMENT); }
","								{ return Scan(Ord(',')); }
"."								{ return Scan(Ord('.')); }

[a-zA-Z_][a-zA-Z0-9_]*				{ return Scan(ParseType.IDENTIFIER); }
[0-9]+								{ return Scan(ParseType.INT_LIT); }
[0-9]+"."[0-9]*([eE][+-]?[0-9]+)?	{ return Scan(ParseType.DOUBLE_LIT); }
[0-9]*"."[0-9]+([eE][+-]?[0-9]+)?	{ return Scan(ParseType.DOUBLE_LIT); }
([0-9]+)([eE][+-]?([0-9]+))			{ return Scan(ParseType.DOUBLE_LIT); }
\"(([^\"])|(\\.))*\"				{ return Scan(ParseType.STRING_LIT); }
.									{ LexErr("unrecognized character"); }

%%