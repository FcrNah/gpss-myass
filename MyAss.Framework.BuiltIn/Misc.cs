﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAss.Framework
{
    class Console
    {
        public static void WriteLine(object o, ConsoleColor color)
        {
            System.Console.ForegroundColor = color;
            //System.Console.WriteLine(o);
            System.Console.ResetColor();
        }

        public static void WriteLine(object o)
        {
            //System.Console.WriteLine(o);
        }
    }
}
