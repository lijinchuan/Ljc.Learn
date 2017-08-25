using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ljc.Learn.DataStruct.List
{
    public class ListCore
    {
        /// <summary>
        /// 链表相加
        /// </summary>
        /// <param name="lista"></param>
        /// <param name="listb"></param>
        public static void AddList(List<int> lista, List<int> listb)
        {
            if (lista == null)
            {
                lista = new List<int>();
            }

            if (listb == null)
            {
                return;
            }

            for (int i = 0; i < lista.Count && i < listb.Count; i++)
            {
                int val = lista[i] + listb[i];
                if (val <= 9)
                {
                    lista[i] = val;
                }
                else
                {
                    lista[i] = val % 10;
                    int j = i + 1;
                    while (true)
                    {
                        if (lista.Count <= j)
                        {
                            lista.Add(1);
                            break;
                        }
                        else
                        {
                            lista[j] += 1;
                            if (lista[j] <= 9)
                            {
                                break;
                            }
                            else
                            {
                                lista[j] = 0;
                                j++;
                            }
                        }
                    }
                }
            }
            if (listb.Count > lista.Count)
            {
                var listacount=lista.Count;
                for (int i = listacount; i < listb.Count; i++)
                {
                    lista.Add(listb[i]);
                }
            }
        }

        public static void PrientTest()
        {
            var lista = new List<int>() { 9,9,9,9};
            //var lista = new List<int>() { 1,1,2,3,1};
            List<int> listb = null;

            AddList(lista, listb);

            for (int i = lista.Count - 1; i >= 0; i--)
            {
                Console.Write(lista[i]);
            }
            Console.WriteLine();
        }
    }
}
