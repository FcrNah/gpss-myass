﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAss.Framework.OperandTypes;

namespace MyAss.Framework.Commands
{
    public class Initial : ICommand
    {
        public IDoubleOperand A_TargetEntity { get; private set; }
        public IDoubleOperand B_Value { get; private set; }

        public string Label
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Initial(IDoubleOperand targetEntity, IDoubleOperand value)
        {
            this.A_TargetEntity = targetEntity;
            this.B_Value = value;
        }

        public void SetLabel(string label)
        {
            throw new NotImplementedException();
        }
    }
}