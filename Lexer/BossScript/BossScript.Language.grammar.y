%namespace BossScript.BossScript
%partial
%parsertype BossScriptParser
%visibility internal
%tokentype Token

%token BREAK DOUBLE ELSE FOR IF INT RETURN VOID WHILE
%token IDENTIFIER CLASSNAME CLASS STRING BOOL
%token INT_LIT DOUBLE_LIT STRING_LIT BOOL_LIT NULL_VAL
%token LESS_THAN_OR_EQUAL GREATER_THAN_OR_EQUAL
%token IS_EQUAL_TO NOT_EQUAL_TO LOGICAL_AND LOGICAL_OR
%token INCREMENT DECREMENT
%token PUBLIC STATIC

%%

ClassDecl:			PUBLIC CLASS IDENTIFIER ClassBody ;
ClassBody:			'{' ClassBodyDecls '}' | '{' '}' ;
ClassBodyDecls:		ClassBodyDecl | ClassBodyDecls ClassBodyDecl ;
ClassBodyDecl:		FieldDecl | MethodDecl | ConstructorDecl ;

FieldDecl:			Type VarDecls ';' ;
Type:				INT | DOUBLE | BOOL | STRING | VOID | Name ;
Name:				IDENTIFIER | QualifiedName ;
QualifiedName:		Name '.' IDENTIFIER ;
VarDecls:			VarDeclarator | VarDecls ',' VarDeclarator ;
VarDeclarator:		IDENTIFIER | VarDeclarator '[' ']' ;

MethodDecl:			MethodHeader Block ;
ConstructorDecl:	FuncDeclarator Block ;
MethodHeader:		PUBLIC STATIC Type FuncDeclarator | VOID FuncDeclarator ;
FuncDeclarator:		IDENTIFIER '(' FormalParmListOpt ')' ;
FormalParmListOpt:	FormalParmList | ;
FormalParmList:		FormalParm | FormalParmList ',' FormalParm ;
FormalParm:			Type VarDeclarator ;

Block:				'{' BlockStmtsOpt '}' ;
BlockStmtsOpt:		BlockStmts | ;
BlockStmts:			BlockStmt | BlockStmts BlockStmts ;
BlockStmt:			LocalVarDeclStmt | Stmt ;
LocalVarDeclStmt:	LocalVarDecl ';' ;
LocalVarDecl:		Type VarDecls ;
Stmt:				Block | ';' | ExprStmt | BreakStmt | ReturnStmt | IfThenStmt 
					| IfThenElseStmt | IfThenElseIfStmt | WhileStmt | ForStmt ;
ExprStmt:			StmtExpr ';' ;
StmtExpr:			Assignment | MethodCall | InstantiationExpr ;

IfThenStmt:			IF '(' Expr ')' Block ;
IfThenElseStmt:		IF '(' Expr ')' Block ELSE Block ;
IfThenElseIfStmt:	IF '(' Expr ')' Block ElseIfSequence | IF '(' Expr ')' Block ElseIfSequence ELSE Block ;
ElseIfSequence:		ElseIfStmt | ElseIfSequence ElseIfStmt ;
ElseIfStmt:			ELSE IfThenStmt ;

WhileStmt:			WHILE '(' Expr ')' Block ;

ForStmt:			FOR '(' ForInit ';' ExprOpt ';' ForUpdate ')' Block ;
ForInit:			StmtExprList | LocalVarDecl | ;
ExprOpt:			Expr | ;
ForUpdate:			StmtExprList | ;
StmtExprList:		StmtExpr | StmtExprList ',' StmtExpr ;

BreakStmt:			BREAK ';' ;
ReturnStmt:			RETURN ExprOpt ';' ;

Primary:			Literal | '(' Expr ')' | FieldAccess | MethodCall ;
Literal:			INT_LIT | DOUBLE_LIT | BOOL_LIT | STRING_LIT | NULL_VAL ;
InstantiationExpr:	Name '(' ArgListOpt ')' ;
ArgList:			Expr | ArgList ',' Expr ;
ArgListOpt:			ArgList | ;
FieldAccess:		Primary '.' IDENTIFIER ;
MethodCall:			Name '(' ArgListOpt ')'
					| Name '{' ArgListOpt '}'
					| Primary '.' IDENTIFIER '(' ArgListOpt ')'
					| Primary '.' IDENTIFIER '{' ArgListOpt '}' ;
PostFixExpr:		Primary | Name ;
UnaryExpr:			'-' UnaryExpr | '!' UnaryExpr | PostFixExpr ;
MulExpr:			UnaryExpr | MulExpr '*' UnaryExpr | MulExpr '/' UnaryExpr | MulExpr '%' UnaryExpr ;
AddExpr:			MulExpr | AddExpr '+' MulExpr | AddExpr '-' MulExpr ;
RelOp:				LESS_THAN_OR_EQUAL | GREATER_THAN_OR_EQUAL | '<' | '>' ;
RelExpr:			AddExpr | RelExpr RelOp AddExpr ;
EqExpr:				RelExpr | EqExpr IS_EQUAL_TO RelExpr | EqExpr NOT_EQUAL_TO RelExpr ;
CondAndExpr:		EqExpr | CondAndExpr LOGICAL_AND EqExpr ;
CondOrExpr:			CondAndExpr | CondOrExpr LOGICAL_OR CondAndExpr ;
Expr:				CondOrExpr | Assignment ;
Assignment:			LeftHandSide AssignOp Expr ;
LeftHandSide:		Name | FieldAccess ;
AssignOp:			'=' | INCREMENT | DECREMENT ;


%%