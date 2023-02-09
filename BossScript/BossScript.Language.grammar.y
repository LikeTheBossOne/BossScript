%namespace BossScript
%partial
%YYSTYPE ParserVal
%parsertype BossScriptParser
%visibility internal
%tokentype TokenType

%token BREAK DOUBLE ELSE FOR IF INT RETURN VOID WHILE
%token IDENTIFIER CLASSNAME CLASS STRING BOOL
%token INT_LIT DOUBLE_LIT STRING_LIT BOOL_LIT NULL_VAL
%token LESS_THAN_OR_EQUAL GREATER_THAN_OR_EQUAL
%token IS_EQUAL_TO NOT_EQUAL_TO LOGICAL_AND LOGICAL_OR
%token INCREMENT DECREMENT
%token PUBLIC STATIC

%%

ClassDecl:			PUBLIC CLASS IDENTIFIER ClassBody
					{
						$$=MakeNode("ClassDecl",1000,$3,$4);
						RootVal = $$;
					} ;
ClassBody:			'{' ClassBodyDecls '}'
					{
						$$=MakeNode("ClassBody",1010,$2);
					} 
					| '{' '}'
					{
						$$=MakeNode("ClassBody",1011);
					} ;
ClassBodyDecls:		ClassBodyDecl | ClassBodyDecls ClassBodyDecl
					{
						$$=MakeNode("ClassBodyDecls",1020,$1,$2);
					} ;
ClassBodyDecl:		FieldDecl | MethodDecl | ConstructorDecl ;

FieldDecl:			Type VarDecls ';'
					{
						$$=MakeNode("FieldDecl",1030,$1,$2);
					} ;
Type:				INT | DOUBLE | BOOL | STRING | VOID | Name ;
Name:				IDENTIFIER | QualifiedName ;
QualifiedName:		Name '.' IDENTIFIER
					{
						$$=MakeNode("QualifiedName",1040,$1,$3);
					} ;
VarDecls:			VarDeclarator | VarDecls ',' VarDeclarator
					{
						$$=MakeNode("VarDecls",1050,$1,$3);
					} ;
VarDeclarator:		IDENTIFIER | VarDeclarator '[' ']'
					{
						$$=MakeNode("VarDeclarator",1060,$1);
					} ;

MethodDecl:			MethodHeader Block
					{
						$$=MakeNode("MethodDecl",1380,$1,$2);
					} ;
MethodReturnVal:	Type | VOID ;
MethodHeader:		PUBLIC STATIC MethodReturnVal MethodDeclarator
					{
						$$=MakeNode("MethodHeader",1070,$3,$4);
					} ;
MethodDeclarator:	IDENTIFIER '(' FormalParmListOpt ')'
					{ 
						$$=MakeNode("MethodDeclarator",1080,$1,$3);
					} ;
FormalParmListOpt:	FormalParmList | ;
FormalParmList:		FormalParm | FormalParmList ',' FormalParm
					{ 
						$$=MakeNode("FormalParmList",1090,$1,$3);
					} ;
FormalParm:			Type VarDeclarator
					{
						$$=MakeNode("FormalParm",1100,$1,$2);
					} ;
ConstructorDecl:	MethodDeclarator Block
					{
						$$=MakeNode("ConstructorDecl",1110,$1,$2);
					} ;

Block:				'{' BlockStmtsOpt '}' 
					{
						$$=MakeNode("Block",1200,$2);
					} ;
BlockStmtsOpt:		BlockStmts | ;
BlockStmts:			BlockStmt | BlockStmts BlockStmts 
					{
						$$=MakeNode("BlockStmts",1130,$1,$2);
					} ;
BlockStmt:			LocalVarDeclStmt | Stmt ;
LocalVarDeclStmt:	LocalVarDecl ';' ;
LocalVarDecl:		Type VarDecls 
					{
						$$=MakeNode("LocalVarDecl",1140,$1,$2);
					} ;
Stmt:				Block | ';' | ExprStmt | BreakStmt | ReturnStmt | IfThenStmt 
					| IfThenElseStmt | IfThenElseIfStmt | WhileStmt | ForStmt ;
ExprStmt:			StmtExpr ';' ;
StmtExpr:			Assignment | MethodCall | InstantiationExpr ;

IfThenStmt:			IF '(' Expr ')' Block 
					{
						$$=MakeNode("IfThenStmt",1150,$3,$5);
					} ;
IfThenElseStmt:		IF '(' Expr ')' Block ELSE Block
					{
						$$=MakeNode("IfThenElseStmt",1160,$3,$5,$7);
					} ;
IfThenElseIfStmt:	IF '(' Expr ')' Block ElseIfSequence 
					{
						$$=MakeNode("IfThenElseIfStmt",1170,$3,$5,$6);
					}
					| IF '(' Expr ')' Block ElseIfSequence ELSE Block 
					{
						$$=MakeNode("IfThenElseIfStmt",1171,$3,$5,$6,$8);
					} ;
ElseIfSequence:		ElseIfStmt | ElseIfSequence ElseIfStmt
					{
						$$=MakeNode("ElseIfSequence",1180,$1,$2);
					} ;
ElseIfStmt:			ELSE IfThenStmt
					{
						$$=MakeNode("ElseIfStmt",1190,$2);
					} ;

WhileStmt:			WHILE '(' Expr ')' Stmt
					{
						$$=MakeNode("WhileStmt",1210,$3,$5);
					} ;

ForStmt:			FOR '(' ForInit ';' ExprOpt ';' ForUpdate ')' Block 
					{
						$$=MakeNode("ForStmt",1220,$3,$5,$7,$9);
					} ;
ForInit:			StmtExprList | LocalVarDecl | ;
ExprOpt:			Expr | ;
ForUpdate:			StmtExprList | ;
StmtExprList:		StmtExpr | StmtExprList ',' StmtExpr 
					{
						$$=MakeNode("StmtExprList",1230,$1,$3);
					} ;

BreakStmt:			BREAK ';' ;
ReturnStmt:			RETURN ExprOpt ';' 
					{
						$$=MakeNode("ReturnStmt",1250,$2);
					} ;

Primary:			Literal | FieldAccess | MethodCall
					| '(' Expr ')' 
					{
						$$=$2;
					} ;
Literal:			INT_LIT | DOUBLE_LIT | BOOL_LIT | STRING_LIT | NULL_VAL ;
InstantiationExpr:	Name '(' ArgListOpt ')' 
					{
						$$=MakeNode("InstantiationExpr",1260,$1,$3);
					} ;
ArgList:			Expr | ArgList ',' Expr
					{
						$$=MakeNode("ArgList",1270,$1,$3);
					} ;
ArgListOpt:			ArgList | ;
FieldAccess:		Primary '.' IDENTIFIER 
					{
						$$=MakeNode("FieldAccess",1280,$1,$3);
					} ;
MethodCall:			Name '(' ArgListOpt ')' 
					{
						$$=MakeNode("MethodCall",1290,$1,$3);
					}
					| Name '{' ArgListOpt '}'
					{
						$$=MakeNode("MethodCall",1291,$1,$3);
					}
					| Primary '.' IDENTIFIER '(' ArgListOpt ')'
					{
						$$=MakeNode("MethodCall",1292,$1,$3,$5);
					}
					| Primary '.' IDENTIFIER '{' ArgListOpt '}'
					{
						$$=MakeNode("MethodCall",1293,$1,$3,$5);
					} ;
PostFixExpr:		Primary | Name ;
UnaryExpr:			'-' UnaryExpr 
					{
						$$=MakeNode("UnaryExpr",1300,$1,$2);
					}
					| '!' UnaryExpr 
					{
						$$=MakeNode("UnaryExpr",1301,$1,$2);
					}
					| PostFixExpr ;
MulExpr:			UnaryExpr 
					| MulExpr '*' UnaryExpr 
					{
						$$=MakeNode("MulExpr",1310,$1,$3);
					}
					| MulExpr '/' UnaryExpr 
					{
						$$=MakeNode("MulExpr",1311,$1,$3);
					}
					| MulExpr '%' UnaryExpr
					{
						$$=MakeNode("MulExpr",1312,$1,$3);
					} ;
AddExpr:			MulExpr 
					| AddExpr '+' MulExpr 
					{
						$$=MakeNode("AddExpr",1320,$1,$3);
					}
					| AddExpr '-' MulExpr
					{
						$$=MakeNode("AddExpr",1320,$1,$3);
					} ;
RelOp:				LESS_THAN_OR_EQUAL | GREATER_THAN_OR_EQUAL | '<' | '>' ;
RelExpr:			AddExpr 
					| RelExpr RelOp AddExpr
					{
						$$=MakeNode("RelExpr",1330,$1,$2,$3);
					} ;
EqExpr:				RelExpr
					| EqExpr IS_EQUAL_TO RelExpr
					{
						$$=MakeNode("EqExpr",1340,$1,$3);
					}
					| EqExpr NOT_EQUAL_TO RelExpr
					{
						$$=MakeNode("EqExpr",1341,$1,$3);
					} ;
CondAndExpr:		EqExpr
					| CondAndExpr LOGICAL_AND EqExpr
					{
						$$=MakeNode("CondAndExpr",1350,$1,$3);
					} ;
CondOrExpr:			CondAndExpr
					| CondOrExpr LOGICAL_OR CondAndExpr
					{
						$$=MakeNode("CondOrExpr",1360,$1,$3);
					};
Expr:				CondOrExpr | Assignment ;
Assignment:			LeftHandSide AssignOp Expr 
					{
						$$=MakeNode("Assignment",1370,$1,$2,$3);
					} ;
LeftHandSide:		Name | FieldAccess ;
AssignOp:			'=' | INCREMENT | DECREMENT ;


%%