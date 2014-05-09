﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAssCompiler.AST
{
    public abstract class ASTAccessor : IASTNode
    {
        public abstract void Accept(IASTVisitor visitor);
    }
}
