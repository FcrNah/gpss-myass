﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAssCompiler
{
    public interface ICharSource
    {
        int Position { get; }
        int Line { get; }
        int Column { get; }
        char CurrentChar { get; }

        void Next();
    }
}
