﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyAss.Framework.BuiltIn.Entities;
using MyAss.Framework.Commands;
using MyAss.Framework.OperandTypes;

namespace MyAss.Framework.BuiltIn.Commands
{
    public class Storage : AnyQueuedCommand
    {
        public IDoubleOperand A_StorageCapacity { get; private set; }

        public Storage(IDoubleOperand storageCapacity)
        {
            this.A_StorageCapacity = storageCapacity;
        }

        public override void Execute(Simulation simulation)
        {
            // A: Required. The operand must be PosInteger.
            if (this.A_StorageCapacity == null)
            {
                throw new ModelingException("STORAGE: Operand A is required operand!");
            }
            int capacity = (int)this.A_StorageCapacity.GetValue();
            if (capacity <= 0)
            {
                throw new ModelingException("STORAGE: Operand A must be PosInteger!");
            }

            StorageEntity storageEntity = new StorageEntity(simulation, this.Id, capacity);
            simulation.Entities.Add(this.Id, storageEntity);
        }
    }
}
