﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAssCompiler.AST;

namespace MyAssCompiler
{
    public class ASTPrintVisitor : IASTVisitor
    {
        public IParser Parser { get; private set; }

        private StringBuilder result;

        public ASTPrintVisitor(IParser parser)
        {
            this.Parser = parser;
        }

        public string Print()
        {
            result = new StringBuilder();
            this.Visit(this.Parser.Parse());
            return result.ToString();
        }


        public void Visit(ASTModel model)
        {
            foreach (var verb in model.Verbs)
            {
                verb.Accept(this);
                result.Append(Environment.NewLine);
            }
        }

        public void Visit(ASTBlock block)
        {
            if (block.LabelId.HasValue)
            {
                result.Append(this.Parser.IdsList[block.LabelId.Value]);
                result.Append(" ");
            }

            result.Append(this.Parser.IdsList[block.VerbId]);
            result.Append(" ");

            if (block.Operator != null)
            {
                block.Operator.Accept(this);
                result.Append(" ");
            }

            if (block.Operands != null)
            {
                block.Operands.Accept(this);
            }

            if (!block.IsResolved)
            {
                result.Append(" :: Unresolved");
            }
        }

        public void Visit(ASTOperator op)
        {
            result.Append(this.Parser.IdsList[op.Id]);
        }

        public void Visit(ASTOperands operands)
        {
            if (operands.Operands.Count > 0)
            {
                operands.Operands[0].Accept(this);
                foreach (var actual in operands.Operands.Skip(1))
                {
                    actual.Accept(this);
                }
            }
        }

        public void Visit(ASTLiteral literal)
        {
            result.Append(literal.Value);
        }

        public void Visit(ASTBinaryExpression expr)
        {
            expr.LValue.Accept(this);

            switch (expr.Operator)
            {
                case BinaryOperatorType.ADD:
                    result.Append("+");
                    break;
                case BinaryOperatorType.SUBSTRACT:
                    result.Append("-");
                    break;
                case BinaryOperatorType.MULTIPLY:
                    result.Append("#");
                    break;
                case BinaryOperatorType.DIVIDE:
                    result.Append("/");
                    break;
                case BinaryOperatorType.MODULO:
                    result.Append("\\");
                    break;
                case BinaryOperatorType.EXPONENT:
                    result.Append("^");
                    break;
            }

            expr.RValue.Accept(this);
        }

        public void Visit(ASTPostfixUnaryExpression expr)
        {
            expr.Value.Accept(this);

            switch (expr.Operator)
            {
                case PostfixUnaryOperatorType.PLUS:
                    result.Append("+");
                    break;
                case PostfixUnaryOperatorType.MINUS:
                    result.Append("-");
                    break;
            }
        }

        public void Visit(ASTPrefixUnaryExpression expr)
        {
            switch (expr.Operator)
            {
                case PrefixUnaryOperatorType.PLUS:
                    result.Append("+");
                    break;
                case PrefixUnaryOperatorType.MINUS:
                    result.Append("-");
                    break;
            }

            expr.Value.Accept(this);
        }

        public void Visit(ASTLValue lval)
        {
            result.Append(this.Parser.IdsList[lval.Id]);
            lval.Accessor.Accept(this);
        }

        public void Visit(ASTDirectSNA sna)
        {
            result.Append("$");
            result.Append(this.Parser.IdsList[sna.Id]);
        }

        public void Visit(ASTCall call)
        {
            result.Append("(");

            if (call.Actuals != null)
            {
                call.Actuals.Accept(this);
            }

            result.Append(")");
        }

        public void Visit(ASTActuals actuals)
        {
            if (actuals.Expressions.Count > 0)
            {
                actuals.Expressions[0].Accept(this);
                foreach (var actual in actuals.Expressions.Skip(1))
                {
                    actual.Accept(this);
                }
            }
        }
    }
}
