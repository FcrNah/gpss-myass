﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAss.Compiler
{
    public class Scanner : IScanner
    {
        public ICharSourceTokenizer CharSource { get; private set; }

        private TokenType currentToken;
        private object currentTokenVal;
        private int currentTokenLine;
        private int currentTokenColumn;

        public bool ignoreWhitespace;

        public TokenType CurrentToken { get { return this.currentToken; } }
        public object CurrentTokenVal { get { return this.currentTokenVal; } }
        public int CurrentTokenLine { get { return this.currentTokenLine; } }
        public int CurrentTokenColumn { get { return this.currentTokenColumn; } }
        public string CurrentLine { get { return  this.CharSource.CurrentLine; } }

        public bool IgnoreWhitespace { get { return this.ignoreWhitespace; } set { this.ignoreWhitespace = value; } }


        public Scanner(ICharSourceTokenizer charSource)
        {
            this.CharSource = charSource;
            this.Next();
        }

        private void Ret(TokenType token, object value)
        {
            this.currentToken = token;
            this.currentTokenVal = value;

//            Console.WriteLine(String.Format("{0, -10}", token) + value);
        }

        private void Ret(TokenType token)
        {
            this.Ret(token, null);
            this.CharSource.Next();
        }

        public void Next()
        {
            if (this.IgnoreWhitespace)
            {
                while (Char.IsWhiteSpace(this.CharSource.CurrentChar) && this.CharSource.CurrentChar != '\n')
                {
                    this.CharSource.Next();
                }
            }
            this.currentTokenLine = this.CharSource.Line;
            this.currentTokenColumn = this.CharSource.Column;

            switch (this.CharSource.CurrentChar)
            {
                case '\0':
                    this.Ret(TokenType.EOF);
                    break;

                case '+':
                    this.Ret(TokenType.PLUS);
                    break;
                case '-':
                    this.Ret(TokenType.MINUS);
                    break;
                case '#':
                    this.Ret(TokenType.OCTOTHORPE);
                    break;
                case '/':
                    this.Ret(TokenType.FWDSLASH);
                    break;
                case '\\':
                    this.Ret(TokenType.BCKSLASH);
                    break;
                case '^':
                    this.Ret(TokenType.CARRET);
                    break;

                case '(':
                    this.Ret(TokenType.LPAR);
                    break;
                case ')':
                    this.Ret(TokenType.RPAR);
                    break;
                case '$':
                    this.Ret(TokenType.DOLLAR);
                    break;
                case '*':
                    this.Ret(TokenType.ASTERISK);
                    break;
                case ',':
                    this.Ret(TokenType.COMMA);
                    break;
                case '@':
                    this.Ret(TokenType.ATSIGN);
                    break;
                case '.':
                    this.Ret(TokenType.PERIOD);
                    break;
                case '_':
                    this.Ret(TokenType.UNDERSCORE);
                    break;

                case '\r':
                case '\n':
                    this.RetLinefeed();
                    break;

                case ';':
                    this.RetComment();
                    break;

                default:
                    if (Char.IsLetter(this.CharSource.CurrentChar) || this.CharSource.CurrentChar == '_')
                    {
                        this.RetIdOrKwd();
                    }
                    else if (Char.IsDigit(this.CharSource.CurrentChar) /*|| this.CharSource.CurrentChar == '.'*/)
                    {
                        this.RetInteger();
                    }
                    else if (Char.IsWhiteSpace(this.CharSource.CurrentChar) && this.CharSource.CurrentChar != '\n')
                    {
                        this.Ret(TokenType.WHITE);
                    }
                    else
                    {
                        this.Ret(TokenType.ILLEGAL);
                    }
                    break;
            }
        }

        private void RetLinefeed()
        {
            do
            {
                this.CharSource.Next();
            } while (this.CharSource.CurrentChar == '\n' || this.CharSource.CurrentChar == '\r');

            this.Ret(TokenType.LF, null);
        }

        private void RetComment()
        {
            string buffer = "";
            do
            {
                buffer += this.CharSource.CurrentChar;
                this.CharSource.Next();
            } while (this.CharSource.CurrentChar != '\n' && this.CharSource.CurrentChar != '\0');

            this.Ret(TokenType.COMMENT, buffer);
        }

        // <id> ::= <letter> | <UNDERSCORE> | <id> <letter> | <id> <digit> | <id> <UNDERSCORE>
        private void RetIdOrKwd()
        {
            string buffer = "";
            do
            {
                buffer += this.CharSource.CurrentChar;
                this.CharSource.Next();
            } while (char.IsLetterOrDigit(this.CharSource.CurrentChar)
                || this.CharSource.CurrentChar == '_');

            switch (buffer.ToUpper())
            {
                case "USING":
                    this.Ret(TokenType.USING, null);
                    break;
                case "USINGP":
                    this.Ret(TokenType.USINGP, null);
                    break;
                default:
                    this.Ret(TokenType.ID, buffer);
                    break;
            }
        }

        // <integer> ::= <digit> | <integer> <digit>
        private void RetInteger()
        {
            string buffer = "";
            do
            {
                buffer += this.CharSource.CurrentChar;
                this.CharSource.Next();
            } while (Char.IsDigit(this.CharSource.CurrentChar));

            this.Ret(TokenType.INTEGER, buffer);
        }
    }
}
