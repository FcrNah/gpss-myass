﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAss.Application.Examples.NakedTestModels
{
    public static class Model_WithEQU
    {
        public static string Model
        {
            get
            {
                return @"
@using MyAss.Framework.BuiltIn.Blocks
@using MyAss.Framework.BuiltIn.Commands

@usingp MyAss.Framework.BuiltIn.SNA.SystemSNA
@usingp MyAss.Framework.BuiltIn.SNA.SavevalueSNA
@usingp MyAss.Framework.BuiltIn.SNA.QueueSNA
@usingp MyAss.Framework.BuiltIn.Procedures.Distributions

In_use EQU 5 ;Mean time
Range EQU 3 ;Half range
Server STORAGE 1

START 30000
        GENERATE  7,7           ;People arrive
        QUEUE     Turn          ;Enter queue
        ENTER     Server          ;Acquire turnstile
        DEPART    Turn          ;Depart the queue
        ADVANCE   In_use,Range  ;Use turnstile
        LEAVE   Server          ;Leave turnstile
        TERMINATE 1             ;One spectator enters
";
            }
        }
    }
}
