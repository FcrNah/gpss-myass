﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAssCompiler.AST
{
    public class ASTBlock : ASTVerb
    {
        public int? LabelId { get; set; }
        public int VerbId { get; set; }
        public ASTOperator Operator { get; set; }
        public ASTOperands Operands { get; set; }

        public bool IsResolved { get; set; }
        public int? UnresolvedId1 { get; set; }
        public int? UnresolvedId2 { get; set; }

        public override void Accept(IASTVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return (this.LabelId.HasValue ? String.Format("id({0})", this.LabelId.Value) : "")
                + (this.LabelId.HasValue ? " " : "")
                + String.Format("id({0})", this.VerbId)
                + " "
                + String.Join(",", this.Operands)
                + (this.IsResolved ? "" : "  :: Unresolved");
        }
    }
}