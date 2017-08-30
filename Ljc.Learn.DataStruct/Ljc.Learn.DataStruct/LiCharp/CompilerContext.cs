using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ljc.Learn.DataStruct.LiCharp
{
    public class CompilerContext
    {
        private Stack<string> _codestack = new Stack<string>();
        public Stack<string> CodeStack
        {
            get
            {
                return _codestack;
            }
        }

        public int LineNo
        {
            get;
            set;
        }

        public int ColsNo
        {
            get;
            set;
        }

        public char LastScanChar
        {
            get;
            set;
        }

        public string ScanStr
        {
            get;
            set;
        }

        public TokenType TokenType
        {
            get;
            set;
        }
    }
}
