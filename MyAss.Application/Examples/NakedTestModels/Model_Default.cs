﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAss.Application.Examples.NakedTestModels
{
    public static class Model_Default
    {
        public static string Model
        {
            get
            {
                return @"
@using MyAss.Framework.BuiltIn.Blocks
@using MyAss.Framework.BuiltIn.Commands

@usingp MyAss.Framework.BuiltIn.SNA.SavevalueSNA
@usingp MyAss.Framework.BuiltIn.SNA.QueueSNA
@usingp MyAss.Framework.BuiltIn.Procedures.Distributions

Server STORAGE 3

	START 10000

	GENERATE (Exponential(1,0,1/2))
	SAVEVALUE GenerateCounter,(X$GenerateCounter+1)

	TEST L Q$Tail,20,GoAway		;Jump if in Stack >20
	QUEUE Tail
	ENTER Server,1
	DEPART Tail
	ADVANCE (Exponential(2,0,1/0.2))
	LEAVE Server,1

	SAVEVALUE RejetionProb,(X$RejectCounter/X$GenerateCounter)
	TERMINATE 1


GoAway	SAVEVALUE RejectCounter,(X$RejectCounter+1)
	TERMINATE 		;Delete rejected.

";
            }
        }
    }
}
