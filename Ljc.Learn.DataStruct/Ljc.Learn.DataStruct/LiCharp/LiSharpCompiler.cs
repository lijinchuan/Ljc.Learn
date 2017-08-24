using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ljc.Learn.DataStruct.LiCharp
{
    public class LiSharpCompiler
    {
        public static Dictionary<string, object> ProtectWords = new Dictionary<string, object>();

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

        private bool CanConcat(char newchar)
        {
            if (string.IsNullOrWhiteSpace(Context.ScanStr))
            {
                return true;
            }

            return true;
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
                    throw new CompileException(Context.LineNo, "期待注释开始标记“/*”");
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
                        
                    }

                    Console.WriteLine(line);
                }
            }
        }
    }
}
