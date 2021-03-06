﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAss.Framework.OperandTypes;
using MyAss.Framework.Entities;
using MyAss.Framework.Blocks;
using MyAss.Framework.BuiltIn.Entities;

namespace MyAss.Framework.BuiltIn.Blocks
{
    public class Enter : AnyBlock
    {
        public IDoubleOperand A_StorageEntityId { get; private set; }
        public IDoubleOperand B_NumberOfUnits { get; private set; }

        public Enter(IDoubleOperand storageEntityId, IDoubleOperand numberOfUnits)
        {
            this.A_StorageEntityId = storageEntityId;
            this.B_NumberOfUnits = numberOfUnits;
        }

        public void PreEnter(Simulation simulation, Transaction transaction)
        {
            // TODO: Refactor this, storage may pass self here;

            // A: Required.
            StorageEntity storage = (StorageEntity)simulation.GetEntity((int)this.A_StorageEntityId.GetValue());

            // B: The default value is 1.
            int units = this.B_NumberOfUnits == null ? 1 : (int)this.B_NumberOfUnits.GetValue();

            if (storage.IsAvaliable && storage.RemainingCapacity >= units)
            {
                this.EntryCount++;

                storage.Enter(units);
                Console.WriteLine("Entered  \tTime: " + simulation.Clock + transaction, ConsoleColor.Yellow);
                Console.WriteLine("\tStorageSize: " + storage.CurrentCount);
                transaction.ChangeOwner(this);
                this.NextSequentialBlock.PassTransaction(transaction);
                simulation.CurrentEventChain.AddBehind(transaction);
            }
            else
            {
                Console.WriteLine("preEntered  \tTime: " + simulation.Clock + transaction, ConsoleColor.Yellow);
                transaction.NextEventTime = -1; //temp for tests
                storage.DelayChain.AddLast(transaction);
            }
            
        }

        public override void Action()
        {
            // A: Required. The operand must be PosInteger.
            if (this.A_StorageEntityId == null)
            {
                throw new ModelingException("ENTER: Operand A is required operand!");
            }
            int entityId = (int)this.A_StorageEntityId.GetValue();
            if (entityId <= 0)
            {
                throw new ModelingException("ENTER: Operand A must be PosInteger!");
            }

            // B: The default is 1. The operand must be PosInteger.
            int units = this.B_NumberOfUnits == null ? 1 : (int)this.B_NumberOfUnits.GetValue();
            if (units <= 0)
            {
                throw new ModelingException("ENTER: Operand B must be PosInteger!");
            }


            Transaction transaction = this.Simulation.ActiveTransaction;

            StorageEntity storage = (StorageEntity)this.Simulation.GetEntity(entityId);

            if (storage.IsAvaliable && storage.RemainingCapacity >= units)
            {
                this.EntryCount++;

                storage.Enter(units);
                Console.WriteLine("Entered  \tTime: " + this.Simulation.Clock + transaction, ConsoleColor.Yellow);
                Console.WriteLine("\tStorageSize: " + storage.CurrentCount);
                transaction.ChangeOwner(this);
                this.NextSequentialBlock.PassTransaction(transaction);
                this.Simulation.CurrentEventChain.AddAhead(transaction);
            }
            else
            {
                Console.WriteLine("DelayEntered  \tTime: " + this.Simulation.Clock + transaction, ConsoleColor.Yellow);
                transaction.NextEventTime = -1; //temp for tests
                storage.DelayChain.AddLast(transaction);
            }
            
        }
    }
}
