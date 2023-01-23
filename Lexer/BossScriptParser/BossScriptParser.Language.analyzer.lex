%namespace Lexer.BossScriptParser
%scannertype BossScriptParserScanner
%visibility internal
%tokentype Token

%option stack, minimize, parser, verbose, persistbuffer, noembedbuffers 

Number          [0-9]+

%{

%}

%%

/* Scanner body */
"/*"([^*]|"*"+[^/*])*"*"+"/"	{ BossScript.Comment(); }
"//".*\r?\n						{ BossScript.Comment(); }
[ \t\r\f]+						{ BossScript.WhiteSpace(); }
\n								{ BossScript.NewLine(); }

"bool"							{ return BossScript.Scan(ParseType.BOOL); }
"break"							{ return BossScript.Scan(ParseType.BREAK); }
"class"							{ return BossScript.Scan(ParseType.CLASS); }
"double"						{ return BossScript.Scan(ParseType.DOUBLE); }
"else"							{ return BossScript.Scan(ParseType.ELSE); }
"false"							{ return BossScript.Scan(ParseType.BOOL_LIT); }
"for"							{ return BossScript.Scan(ParseType.FOR); }
"if"							{ return BossScript.Scan(ParseType.IF); }
"int"							{ return BossScript.Scan(ParseType.INT); }
"null"							{ return BossScript.Scan(ParseType.NULL); }
"public"						{ return BossScript.Scan(ParseType.PUBLIC); }
"return"						{ return BossScript.Scan(ParseType.RETURN); }
"static"						{ return BossScript.Scan(ParseType.STATIC); }
"string"						{ return BossScript.Scan(ParseType.STRING); }
"true"							{ return BossScript.Scan(ParseType.BOOL_LIT); }
"void"							{ return BossScript.Scan(ParseType.VOID); }
"while"							{ return BossScript.Scan(ParseType.WHILE); }
"("								{ return BossScript.Scan(BossScript.Ord('(')); }
")"								{ return BossScript.Scan(BossScript.Ord(')')); }
"["								{ return BossScript.Scan(BossScript.Ord('[')); }
"]"								{ return BossScript.Scan(BossScript.Ord(']')); }
"{"								{ return BossScript.Scan(BossScript.Ord('{')); }
"}"								{ return BossScript.Scan(BossScript.Ord('}')); }
";"								{ return BossScript.Scan(BossScript.Ord(';')); }
":"								{ return BossScript.Scan(BossScript.Ord(':')); }
"!"								{ return BossScript.Scan(BossScript.Ord('!')); }
"*"								{ return BossScript.Scan(BossScript.Ord('*')); }
"/"								{ return BossScript.Scan(BossScript.Ord('/')); }
"%"								{ return BossScript.Scan(BossScript.Ord('%')); }
"+"								{ return BossScript.Scan(BossScript.Ord('+')); }
"-"								{ return BossScript.Scan(BossScript.Ord('-')); }
"<"								{ return BossScript.Scan(BossScript.Ord('<')); }
"<="							{ return BossScript.Scan(ParseType.LESS_THAN_OR_EQUAL); }
">"								{ return BossScript.Scan(BossScript.Ord('>')); }
">="							{ return BossScript.Scan(ParseType.GREATER_THAN_OR_EQUAL); }
"=="							{ return BossScript.Scan(ParseType.EQUAL_TO); }
"!="							{ return BossScript.Scan(ParseType.NOT_EQUAL_TO); }
"&&"							{ return BossScript.Scan(ParseType.LOGICAL_AND); }
"||"							{ return BossScript.Scan(ParseType.LOGICAL_OR); }
"="								{ return BossScript.Scan(BossScript.Ord('=')); }
"+="							{ return BossScript.Scan(ParseType.INCREMENT); }
"-="							{ return BossScript.Scan(ParseType.DECREMENT); }
","								{ return BossScript.Scan(BossScript.Ord(',')); }
"."								{ return BossScript.Scan(BossScript.Ord('.')); }

[a-zA-Z_] [a-zA-Z0-9_]*					{ return BossScript.Scan(ParseType.IDENTIFIER); }
{Number}								{ return BossScript.Scan(ParseType.INT_LIT); }
{Number}"."[0-9]*([eE][+-]?{Number})?	{ return BossScript.Scan(ParseType.DOUBLE_LIT); }
[0-9]*"."{Number}([eE][+-]?{Number})?	{ return BossScript.Scan(ParseType.DOUBLE_LIT); }
({Number})([eE][+-]?({Number}))			{ return BossScript.Scan(ParseType.DOUBLE_LIT); }
\"([^\"])|(\\.)*\"						{ return BossScript.Scan(ParseType.STRING_LIT); }
.										{ return BossScript.Err("unrecognized character"); }

%%