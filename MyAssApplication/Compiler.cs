﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAssCompiler;

namespace MyAssApplication
{
    public static class Compiler
    {
        public static void RunDefaultCompiler()
        {
            string source = System.IO.File.ReadAllText(@".\Source.txt");

            IScanner s = new Scanner(new StringCharSource(source));

            while (s.CurrentToken != TokenType.EOF)
            {
                s.Next();
            }

            Console.WriteLine();

            foreach (var item in s.Identifiers)
            {
                Console.WriteLine(s.Identifiers.IndexOf(item) + "\t" + item);
            }
        }

        public static void TestCodeGen()
        {
            CodeGenerator cg = new CodeGenerator();
            cg.Some();
        }
    }
}
