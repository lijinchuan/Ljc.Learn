using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ljc.Learn.DataStruct.LiCharp
{
    public class LiSharpCompiler
    {
        public static Dictionary<string, object> ProtectWords = new Dictionary<string, object>();
        const char StringChar = '\'';
        const char ChanageMeanChar = '\\';
        const char WriteSpace=' ';
        const char EnterLine='\r';
        const char NewLine='\n';

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

        private bool CharIsNum(char ch)
        {
            return ch >= '0' && ch <= '9';
        }

        private bool CharIsMathOpSymbol(char ch)
        {
            return ch == '+' || ch == '-' || ch == '*' || ch == '/' || ch == '%';
        }


        private bool JoinToken(char ch)
        {
            if (Context.TokenType == TokenType.LineAnnotation
                || Context.TokenType == TokenType.LinesAnnotation)
            {
                if (Context.TokenType == TokenType.LineAnnotation)
                {
                    if (ch == EnterLine)
                    {
                        return true;
                    }
                    if (ch == NewLine)
                    {
                        return false;
                    }
                }

                if (Context.TokenType == TokenType.LinesAnnotation)
                {
                    if (ch == '/'&&Context.LastScanChar=='*')
                    {
                        Context.Token = Context.Token.TrimEnd('*');
                        return false;
                    }
                }
                Context.Token += ch;
                Context.LastScanChar = ch;
                return true;
            }
            else if (Context.TokenType == TokenType.str)
            {
                if (Context.LastScanChar != ChanageMeanChar && (ch == '"' || ch == '\''))
                {
                    if (Context.CodeStack.Count == 0)
                    {
                        throw new CompileException(Context.LineNo, Context.ColsNo, "意外的字符:" + ch);
                    }
                    var popch = Context.CodeStack.Pop();
                    if (popch == ch.ToString())
                    {
                        return false;
                    }
                }
                else if (Context.LastScanChar != ChanageMeanChar && ch == ChanageMeanChar)
                {
                    //转义
                    Context.LastScanChar = ChanageMeanChar;
                    return true;
                }

                Context.LastScanChar = ch;
                Context.Token += ch;
                return true;
            }
            else if (Context.TokenType == TokenType.Any)
            {
                if (ch == '/')
                {
                    if (Context.LastScanChar == '/')
                    {
                        Context.TokenType = TokenType.LineAnnotation;
                    }
                }
                else if (ch == '*')
                {
                    if (Context.LastScanChar == '/')
                    {
                        Context.TokenType = TokenType.LinesAnnotation;
                    }
                }
                else if (ch == '"' || ch == '\'')
                {
                    Context.CodeStack.Push(ch.ToString());
                    Context.TokenType = TokenType.str;
                }
                else if (CharIsNum(ch) || (ch == '-' && (CharIsMathOpSymbol(Context.LastScanChar)||Context.LastScanChar=='\0'||Context.LastScanChar=='\n') && CharIsNum(Context.NextChar)))
                {
                    Context.Token += ch;
                    Context.TokenType = TokenType.interger;
                }
                else if (ch == '+')
                {
                    if (CharIsNum(Context.NextChar))
                    {
                        Context.Token += ch;
                        Context.LastScanChar = ch;
                        return false;
                    }
                }
                else if (ch == '-')
                {
                    if (Context.IsBack)
                    {
                        Context.IsBack = false;
                        Context.LastScanChar = ch;
                        Context.Token += ch;
                        return false;
                    }
                    else
                    {
                        Context.ColsNo--;
                        Context.IsBack = true;
                        return false;
                    }
                }
                else
                {
                    Context.Token += ch;
                }
                Context.LastScanChar = ch;
                return true;
            }
            else
            {
                if (ch == '/' && Context.LastScanChar == '/')
                {
                    Context.ColsNo = -2;
                    return false;
                }
                else if (ch == '*')
                {
                    if (Context.LastScanChar == '/')
                    {
                        Context.ColsNo = -2;
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else if (ch == '.' && Context.TokenType == TokenType.interger)
                {
                    Context.Token += ch;
                    Context.TokenType = TokenType.floatnumber;
                    Context.LastScanChar = ch;
                    return true;
                }
                else if (ch == '"' || ch == '\'')
                {
                    Context.ColsNo -= 1;
                    return false;
                }
                else if (ch == '+')
                {
                    Context.ColsNo--;
                    return false;
                }
                else if (ch == '-')
                {
                    Context.ColsNo--;
                    Context.IsBack = true;
                    return false;
                }
                else
                {
                    Context.Token += ch;
                    return true;
                }
            }
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

                    line = line.Trim() + NewLine;

                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    char ch='\0';
                    for (; Context.ColsNo < line.Length; Context.ColsNo++)
                    {
                        ch = line[Context.ColsNo];
                        if (Context.ColsNo < line.Length - 1)
                        {
                            Context.NextChar = line[Context.ColsNo + 1];
                        }
                        
                        if (JoinToken(ch))
                        {
                            continue;
                        }
                        else
                        {
                            Console.WriteLine(Context.Token.Replace("\n",""));
                            Context.LastToken = Context.Token;
                            Context.Token = string.Empty;
                            
                            Context.LastTokenType = Context.TokenType;
                            Context.TokenType = TokenType.Any;
                        }
                    }
                }
                Console.WriteLine(Context.Token);
            }

            if (Context.CodeStack.Count > 0)
            {
                throw new CompileException(Context.LineNo, Context.ColsNo, "意外的字符:" + Context.CodeStack.Peek());
            }
        }
    }
}
