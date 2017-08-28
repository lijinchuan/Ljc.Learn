using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ljc.Learn.DataStruct.String
{
    public class StringCore
    {
        public static char[] Reverse(char[] chars, int start, int end)
        {
            if (start >= end || start < 0 || end >= chars.Length)
            {
                return chars;
            }

            char ch;
            while (true)
            {
                ch = chars[start];
                chars[start] = chars[end];
                chars[end] = ch;

                start++;
                end--;
                if (start >= end)
                {
                    break;
                }
            }

            return chars;
        }

        public static string Shift(char[] chars, int n)
        {
            var len=chars.Length;
            n = n % len;

            Reverse(chars, 0, len - n - 1);
            Reverse(chars, len - n, len - 1);
            Reverse(chars, 0, len - 1);

            return new string(chars);
        }


        public static void Test()
        {
            var str = "abcdefg123";
            var newstr = Shift(str.ToCharArray(), 5);
            Console.WriteLine(newstr);

        }
    }
}
