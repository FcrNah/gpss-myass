﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAss.Framework.Blocks;
using MyAss.Framework.BuiltIn.Entities;
using MyAss.Framework.OperandTypes;

namespace MyAss.Framework.BuiltIn.Blocks
{
    public class Depart : AnyBlock
    {
        public IDoubleOperand A_QueueEntityId { get; private set; }
        public IDoubleOperand B_NumberOfUnits { get; private set; }

        public Depart(IDoubleOperand queueEntityId, IDoubleOperand numberOfUnits)
        {
            this.A_QueueEntityId = queueEntityId;
            this.B_NumberOfUnits = numberOfUnits;
        }

        public override void Action()
        {
            // A: Required. The operand must be PosInteger.
            if (this.A_QueueEntityId == null)
            {
                throw new ModelingException("DEPART: Operand A is required operand!");
            }
            int entityId = (int)this.A_QueueEntityId.GetValue();
            if (entityId <= 0)
            {
                throw new ModelingException("DEPART: Operand A must be PosInteger!");
            }

            // B: The default is 1. The operand must be PosInteger.
            int units = this.B_NumberOfUnits == null ? 1 : (int)this.B_NumberOfUnits.GetValue();
            if (units <= 0)
            {
                throw new ModelingException("DEPART: Operand B must be PosInteger!");
            }


            Transaction transaction = this.Simulation.ActiveTransaction;
            this.EntryCount++;

            QueueEntity queue = (QueueEntity)this.Simulation.GetEntity(entityId);
            queue.Depart(units);

            Console.WriteLine("Departed  \tTime: " + this.Simulation.Clock + transaction, ConsoleColor.DarkYellow);
            Console.WriteLine("\tQueueSize: " + queue.CurrentContent);
            transaction.ChangeOwner(this);
            this.NextSequentialBlock.PassTransaction(transaction);
            this.Simulation.CurrentEventChain.AddAhead(transaction);
        }
    }
}
