﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAss.Compiler
{
    public enum TokenType
    {
        // Keywords
        USING,
        USINGP,

        // BinaryOperator
        PLUS,       // '+'
        MINUS,      // '-'
        OCTOTHORPE, // '#'
        FWDSLASH,   // '/'
        BCKSLASH,   // '\\'
        CARRET,     // '^'

        LPAR,       // '('
        RPAR,       // ')'
        DOLLAR,     // '$'
        ASTERISK,   // '*'
        COMMA,      // ','
        //SEMICOL,    // ';'
        ATSIGN,     // '@'
        PERIOD,     // '.'
        UNDERSCORE, // '_'

        ILLEGAL,
        EOF,        // '\0'
        LF,         // '\r' or '\n'
        WHITE,

        COMMENT,
        ID,
        INTEGER,
    }
}
