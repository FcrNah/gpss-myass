﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAss.Compiler.AST;

namespace MyAss.Compiler
{
    public interface IASTResolver
    {
        ASTModel Resolve(ASTModel model);
    }
}