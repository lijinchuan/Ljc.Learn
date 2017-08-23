using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ljc.Learn.DataStruct.Sort
{
    /// <summary>
    /// 快速排序
    /// </summary>
    public class QuickSort
    {
        private static void Exchange(int[] array, int i, int j)
        {
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        public static void Sort(int[] array, int istart, int iend)
        {
            if (istart >= iend)
            {
                return;
            }

            int oldstart = istart;
            int oldend = iend;
            int sb = iend; //哨兵

            while (istart < iend)
            {
                if (istart < sb)
                {
                    for (; istart < sb; istart++)
                    {
                        if (array[istart] > array[sb])
                        {
                            Exchange(array, istart, sb);
                            sb = istart;
                            break;
                        }
                    }
                }
                else if (sb < iend)
                {
                    for (; iend > sb; iend--)
                    {
                        if (array[iend] < array[sb])
                        {
                            Exchange(array, iend, sb);
                            sb = iend;
                            break;
                        }
                    }
                }
            }

            Sort(array, oldstart, sb - 1);
            Sort(array, sb + 1, oldend);
        }

        public static void Test()
        {
            var arr = new[] { 4, 7, 181, 410, 123, 25, 745, -92, 0, -589, 115201, 1147, 6, 8, 7, 3, 91, 42, 85, 44, 251, 45 };

            DataStruct.Sort.QuickSort.Sort(arr, 0, arr.Length - 1);

            foreach (var item in arr)
            {
                System.Console.Write(item + "\t");
            }
        }
    }
}
