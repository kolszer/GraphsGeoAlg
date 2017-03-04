using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointAtFigure
{
    public struct Vertex
    {
        public double X;
        public double Y;
    }

    class Program
    {
        //Obliczenie orientacji punktu r wzgledem p i q
        //     1:  Jesli r lezy na lewo od |pq|
        //     -1: Jesli r lezy na prawo od |pq|
        //     0:  To punkt r moze lezec na |pq|
        static int orientationLinePoint(Vertex p, Vertex q, Vertex r)
        {
            double ori = (r.X - q.X) * (q.Y - p.Y) - (r.Y - q.Y) * (q.X - p.X);

            if (ori > 0)
                return 1;
            else if (ori < 0)
                return -1;
            else
                return 0;
        }

        static void Main(string[] args)
        {
            List<Vertex> vList = new List<Vertex>();
            int n = 0;
            while (n < 3)
            {
                Console.WriteLine("Podaj ilosc wierzcholkow figury(n>=3): ");
                n = Convert.ToInt16(Console.ReadLine());
            }

            for (int i = 0; i < n; i++)
            {
                Vertex vTmp = new Vertex();
                Console.WriteLine("Podaj x[{0}]: ", i);
                vTmp.X = Double.Parse(Console.ReadLine());
                Console.WriteLine("Podaj y[{0}]: ", i);
                vTmp.Y = Double.Parse(Console.ReadLine());
                vList.Add(vTmp);
            }

            Vertex vR = new Vertex();
            Console.WriteLine("Podaj x punktu: ");
            vR.X = Double.Parse(Console.ReadLine());
            Console.WriteLine("Podaj y punktu: ");
            vR.Y = Double.Parse(Console.ReadLine());

            //Do zmiennej minX przypisuje minimalna wartosc z listy po X
            //lub z punktu podanego po X i odejmuje -1, by zasymulowac oo
            double minX = Math.Min(vList.Min(p => p.X), vR.X) - 1;

            //vR.Y-0.00001 symulacja obrotu
            Vertex vS = new Vertex() { X = minX, Y = vR.Y-0.00001 };

            int tmp = 0;//Zmienna tymczasowa przechowujaca ilosc przeciec

            for (int i = 0; i < vList.Count; i++)
            {
                if (i < vList.Count - 1)
                {
                    //Jesli p,q,r != p,q,s && r,s,p != r,s,q to |pq| i |rs| przecinaja sie
                    if ((orientationLinePoint(vList[i], vList[i + 1], vR) != orientationLinePoint(vList[i], vList[i + 1], vS))
                        && (orientationLinePoint(vR, vS, vList[i]) != orientationLinePoint(vR, vS, vList[i + 1])))
                        tmp++;
                }
                else
                {
                    //Jesli p,q,r != p,q,s && r,s,p != r,s,q to |pq| i |rs| przecinaja sie
                    if ((orientationLinePoint(vList[i], vList[0], vR) != orientationLinePoint(vList[i], vList[0], vS))
                        && (orientationLinePoint(vR, vS, vList[i]) != orientationLinePoint(vR, vS, vList[0])))
                        tmp++;
                }
            }

            Console.WriteLine((tmp % 2 == 0) ? "Punkt lezy na zewnatrz figury" : "Punkt lezy w wewnatrz figury");
            Console.ReadLine();
        }
    }
}
