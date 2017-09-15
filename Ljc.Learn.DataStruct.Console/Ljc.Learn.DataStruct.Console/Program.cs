using Ljc.Learn.DataStruct.LiCharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ljc.Learn.DataStruct.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //DataStruct.Sort.QuickSort.Test();

            //语法分析器
            //x:5;
            //一元运算
            //二元运算
            //bool运算

//            new LiSharpCompiler(@"
//            //单行备注
//            /*
//            多行备注\\c'xx'asdf
//            */
//            -31+2365+1--33
//            88-98+7").Complier();

            new LiSharpCompiler(@"3+(5--8)+6-7+33--45*20").Complier();

            //DataStruct.List.ListCore.PrientTest();

            //DataStruct.String.StringCore.Test();
            System.Console.Read();
        }
    }
}
