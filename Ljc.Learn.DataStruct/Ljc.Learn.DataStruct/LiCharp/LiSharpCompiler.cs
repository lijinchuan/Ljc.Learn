using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ljc.Learn.DataStruct.LiCharp
{
    public class LiSharpCompiler
    {
        public static Dictionary<string, object> ProtectWords = new Dictionary<string, object>();
        static char StringChar = '\'';
        static char ChanageMeanChar = '\\';
        static char WriteSpace=' ';

        private string Code
        {
            get;
            set;
        }

        private CompilerContext Context = new CompilerContext();

        public LiSharpCompiler(string code)
        {
            Code = code;
        }

        private bool IsInstr()
        {
            if (Context.CodeStack.Count > 0 && Context.CodeStack.Peek() == StringChar.ToString())
            {
                return true;
            }

            return false;
        }

        private bool CanConcat(char newchar)
        {
            bool isinstr = IsInstr();
            //处理字符串
            if (newchar.Equals(StringChar))
            {
                if (isinstr)
                {
                    if (Context.LastScanChar == ChanageMeanChar)
                    {
                        return true;
                    }
                    else
                    {
                        Context.CodeStack.Pop();
                        return false;
                    }
                }
                else
                {
                    if (Context.LastScanChar == ChanageMeanChar)
                    {
                        throw new CompileException(Context.LineNo, Context.ColsNo, "意外的字符“\\”");
                    }
                    else
                    {
                        Context.CodeStack.Push(StringChar.ToString());
                        return false;
                    }
                }
            }
            else if (newchar.Equals(ChanageMeanChar))
            {
                if (!isinstr)
                {
                    throw new CompileException(Context.LineNo, Context.ColsNo, "意外的字符“\\”");
                }
                else
                {
                    Context.ScanStr += newchar;
                    return true;
                }
            }
            else if (newchar.Equals(WriteSpace))
            {
                if (isinstr)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (isinstr)
                {
                    Context.ScanStr += newchar;
                    return true;
                }

                //处理四则运算
                if (newchar == '+')
                {
                    return true;
                }

                Context.ScanStr += newchar;
                return true;
            }
        }

        /// <summary>
        /// 处理注释
        /// </summary>
        /// <returns></returns>
        private string ProcessAnnotation(string code)
        {
            var idx = code.IndexOf("//");

            if (code.StartsWith("//"))
            {
                return null;
            }
            else if (code.StartsWith("/*") && code.EndsWith("*/"))
            {
                return null;
            }
            else if (code.EndsWith("*/"))
            {
                if (this.Context.CodeStack.Count == 0 || !this.Context.CodeStack.Peek().Equals("/*"))
                {
                    throw new CompileException(Context.LineNo, Context.ColsNo, "期待注释开始标记“/*”");
                }
                else
                {
                    this.Context.CodeStack.Pop();
                    return null;
                }

            }
            else if (code.StartsWith("/*"))
            {
                this.Context.CodeStack.Push("/*");
                return null;
            }
            else if (this.Context.CodeStack.Count > 0 && this.Context.CodeStack.Peek().Equals("/*"))
            {
                return null;
            }

            return code;
        }

        public void Complier()
        {
            using(System.IO.StringReader sr = new System.IO.StringReader(Code))
            {
                var line = string.Empty;
                while((line = sr.ReadLine())!=null)
                {
                    Context.LineNo += 1;
                    Context.ColsNo = 0;

                    line = line.Trim();

                    line = ProcessAnnotation(line);
                    if (line == null)
                    {
                        continue;
                    }

                    for (; Context.ColsNo < line.Length; Context.ColsNo++)
                    {
                        var ch = line[Context.ColsNo];
                        if (CanConcat(ch))
                        {
                            //Context.ScanStr += ch;
                        }
                        else
                        {
                            var str = Context.ScanStr;
                            Console.WriteLine(str);
                            Context.ScanStr = null;
                        }

                        Context.LastScanChar = ch;
                    }
                }
            }

            if (Context.CodeStack.Count > 0)
            {
                throw new CompileException(Context.LineNo, Context.ColsNo, "意外的字符:" + Context.CodeStack.Peek());
            }
        }
    }
}
