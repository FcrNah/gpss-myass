﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAss.Framework.Commands
{
    public interface ICommand
    {
        string Label { get; }

        void SetLabel(string label);
    }
}
