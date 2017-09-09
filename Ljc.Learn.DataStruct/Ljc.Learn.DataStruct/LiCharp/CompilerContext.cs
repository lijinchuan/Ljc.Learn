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

        public char NextChar
        {
            get;
            set;
        }

        public string LastToken
        {
            get;
            set;
        }

        public string Token
        {
            get;
            set;
        }

        /// <summary>
        /// 是否回溯
        /// </summary>
        public bool IsBack
        {
            get;
            set;
        }

        public TokenType LastTokenType
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
