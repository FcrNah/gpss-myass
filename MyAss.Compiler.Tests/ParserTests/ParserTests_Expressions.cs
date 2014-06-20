﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAss.Compiler.AST;
using NUnit.Framework;

namespace MyAss.Compiler.Tests.ParserTests
{
    [TestFixture]
    [Category("ParserTests_Expressions")]
    public class ParserTests_Expressions
    {
        [Test]
        public void Expression_Literal()
        {
            string input = @"Mark 3";
            Assert.Pass(this.Run(input).ToString());
        }

        [Test]
        public void Expression_SignedLiteral1()
        {
            string input = @"Mark -3";
            Assert.Pass(this.Run(input).ToString());
        }

        [Test]
        public void Expression_SignedLiteral2()
        {
            string input = @"Mark +3";
            Assert.Pass(this.Run(input).ToString());
        }

        [Test]
        public void Expression_Id()
        {
            string input = @"Mark MarkId";
            Assert.Pass(this.Run(input).ToString());
        }

        [Test]
        public void Expression_ParExpression()
        {
            string input = @"Mark (MarkId)";
            Assert.Pass(this.Run(input).ToString());
        }

        [Test]
        public void Expression_Addition()
        {
            string input = @"Mark 3+4+5/25";
            Assert.Pass(this.Run(input).ToString());
        }

        [Test]
        public void Expression_Complex1()
        {
            string input = @"Mark ((MarkId + 3)/25)";
            Assert.Pass(this.Run(input).ToString());
        }

        [Test]
        public void Expression_Complex2()
        {
            string input = @"Mark ((MarkId + 3)/25 + (1 + (id2 # (2 / 7))))";
            Assert.Pass(this.Run(input).ToString());
        }

        private IASTNode Run(string input)
        {
            Parser parser = new Parser(new Scanner(new StringCharSource(input)));
            ASTModel model = parser.Parse();
            return model;
        }
    }
}
