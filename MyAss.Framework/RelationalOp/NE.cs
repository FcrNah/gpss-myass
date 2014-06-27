﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAss.Framework.RelationalOp
{
    public class NE : RelationalOperator
    {
        public override bool Compare(int lValue, int rValue)
        {
            return lValue != rValue;
        }

        public override bool Compare(double lValue, double rValue)
        {
            return lValue != rValue;
        }
    }
}