﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyAss.Compiler_v2.AST
{
    [DataContract]
    public abstract class ASTAnyNode : IASTVisitableNode
    {
        public abstract T Accept<T>(IASTVisitor<T> visitor);
    }
}