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

        public int Cols
        {
            get;
            set;
        }

        public CompileException(int line,int cols, string message)
            : base(message)
        {
            Line = line;
            Cols = cols;
        }

        public CompileException(int line, int cols, string message, Exception innerException)
            : base(message, innerException)
        {
            Line = line;
            Cols = cols;
        }
    }
}
