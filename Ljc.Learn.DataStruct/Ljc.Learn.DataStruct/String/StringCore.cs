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

        public static string Shift3(char[] chars, int n)
        {
            int i = 0, len = chars.Length-1;
            if (n > len) 
                n = len;
            char ch='0';
            while (n > 0)
            {
                ch = chars[len];
                for (i = len; i > 0; i--)
                {
                    chars[i] = chars[i - 1];
                }
                chars[0] = ch;
                n--;
            }

            return new string(chars);
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

        public static string Shift2(char[] chars, int n)
        {
            var len = chars.Length;
            n = n % len;

            int loop = len / n;
            char ch;
            int preindex = 0;
            for (int k = 0; k < loop; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    var index = len - k * n - i - 1;
                    preindex = index - n;
                    if (preindex < 0)
                    {
                        break;
                    }
                    ch = chars[index];
                    chars[index] = chars[preindex];
                    chars[preindex] = ch;
                }
                if (preindex < 0)
                {
                    break;
                }
            }

            //换前面的情况
            var mod = len % n;
            for (int i = 0; i < mod; i++)
            {
                ch = chars[i];
                chars[i] = chars[n - mod + i];
                chars[n - mod + i] = ch;
            }

            return new string(chars);
        }


        public static void Test()
        {
            var str = "abcdefg123";
            var newstr = Shift3(str.ToCharArray(), 4);
            Console.WriteLine(newstr);

        }
    }
}
