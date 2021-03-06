﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAss.Compiler.AST
{
    public interface IASTVisitableNode
    {
        T Accept<T>(IASTVisitor<T> visitor);
    }
}
