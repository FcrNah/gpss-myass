﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAssFramework.OperandTypes_Test;

namespace MyAssFramework.Blocks
{
    public class Terminate : AbstractBlock
    {
        public Operand A_TerminationCountDecriment { get; private set; }

        public Terminate(Operand terminationCountDecriment)
        {
            this.A_TerminationCountDecriment = terminationCountDecriment;
        }

        public override void Action()
        {
            Transaction transaction = Simulation.It.ActiveTransction;
            this.EntryCount++;

            Console.WriteLine("Terminated\tTime: " + Simulation.It.Clock + transaction + " " + this.Id, ConsoleColor.DarkGray);

            int decriment = (int)this.A_TerminationCountDecriment.GetValue();
            Simulation.It.TerminationsCount -= decriment;

            transaction.Dispose();
        }
    }
}
