using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ljc.Learn.DataStruct.LiCharp
{
    public class CompileException:Exception
    {
        public int Line
        {
            get;
            set;
        }

        public CompileException(int line, string message)
            : base(message)
        {
            Line = line;
        }

        public CompileException(int line,string message, Exception innerException)
            :base(message,innerException)
        {
            Line = line;
        }
    }
}
