using System;
using System.Collections.Generic;
using System.Linq;

namespace SelectionProblem
{
    class Program
    {
        static int Selection(List<int> a, int k)
        {
            //Jeśli n ≤ 10, posortuj tablicę A i zwróć A[k].
            if (a.Count <= 10)
                return a.OrderBy(p => p).ToList()[k];


            //W przeciwnym wypadku podziel tablicę A na ⌊n/5⌋ grup A1, . . . , A⌊n/5⌋po 5
            //elementów oraz na co najwyżej jedną grupę A⌈n / 5⌉ o(n mod 5) elementach.
            List<List<int>> subA = new List<List<int>>();
            for (int i = 0; i < a.Count / 5; i++)
                subA.Add(a.GetRange(i * 5, 5));
            if (a.Count % 5 != 0)
                subA.Add(a.GetRange(a.Count - (a.Count % 5), a.Count % 5));


            //Posortuj elementy grup A1, . . . , A⌈n/5⌉ i wyznacz medianę mi każdej z nich.
            List<int> med = new List<int>();
            for (int i = 0; i < subA.Count; i++)
            {
                subA[i] = subA[i].OrderBy(p => p).ToList();
                med.Add(subA[i][2]);
            }


            //Wywołaj rekurencyjnie procedurę SELECTION, aby wyznaczyć (dolną)  
            //medianę m zbioru { m1, . . . , m⌈n / 5⌉}.
            int m = Selection(med, med.Count % 2 == 0 ? (med.Count / 2) - 1 : med.Count / 2);


            //Podziel tablicę wejściową względem mediany median m tak, że wszystkie ele
            //menty podtablicy A[1. . .i−1] są mniejsze od m, A[i] = m, a wszystkie
            //elementy podtablicy A[i + 1. . .n] są większe od m.
            List<int> aLeft = new List<int>();
            List<int> aRight = new List<int>();
            foreach (var i in a)
                if (i < m)
                    aLeft.Add(i);
                else
                    aRight.Add(i);
            int aI = aLeft.Count;//Index m
            a = aLeft.Concat(aRight).ToList();


            //Jeśli i=k, zwróć m. W przeciwnym razie wywołaj rekurencyjnie procedurę
            //SELECTION, aby wyznaczyć:
            //    •k - ty najmniejszy element w podtablicy A[1. . .i−1], jeśli i > k;
            //    •(k−i)-ty najmniejszy element w podtablicy A[i + 1. . .n], jeśli i < k.
            if (aI == k)
                return m;
            if (aI > k)
                return Selection(aLeft, k);
            //if (aI < k)
            return Selection(aRight, k - aI);
        }
        static void Main(string[] args)
        {
            List<int> input = new List<int>() { 3, 50, 60, 63, 11, 4, 5, 85, 70, 99, 61, 101, 62, 19, 22, 10, 30, 1, 100, 9, 82, 21, 40, 71, 20, 80, 81, 79 };
            Console.WriteLine(Selection(input, 16));
            Console.ReadLine();
        }
    }
}
