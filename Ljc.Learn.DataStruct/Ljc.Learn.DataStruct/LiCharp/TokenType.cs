using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ljc.Learn.DataStruct.LiCharp
{
    public enum TokenType
    {
        Any, //任何
        LineAnnotation, //行注释
        LinesAnnotation, //多行注释
        varname,
        str,
        interger,
        floatnumber,
        plus,
        plusplus,
        minus,
        minusminus,
        mult,
        devi,
        eq,
        eqeq,
        white,
        leftparentheses, //小括号
        rightparentheses,
        leftbracket, //中括号
        rightbracket,
        leftbrace, //大括号
        rightbrace,
        semicolon, //分行
        newline, //换行符号
    }
}
