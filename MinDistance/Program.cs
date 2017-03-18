using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinDistance
{
    class Program
    {
        public struct Point
        {
            public double X;
            public double Y;
        }

        public static double minDist(List<Point> sX, List<Point> sY)
        {
            if (sX.Count == 1)
                return double.MaxValue;
            else if (sX.Count == 2)
                return Math.Sqrt(Math.Pow((sX[0].X - sX[1].X), 2) + Math.Pow((sX[0].Y - sX[1].Y), 2));
            else if (sX.Count == 3)
                return Math.Min(Math.Min(Math.Sqrt(Math.Pow((sX[0].X - sX[1].X), 2) + Math.Pow((sX[0].Y - sX[1].Y), 2)),
                    Math.Sqrt(Math.Pow((sX[0].X - sX[2].X), 2) + Math.Pow((sX[0].Y - sX[2].Y), 2))),
                    Math.Sqrt(Math.Pow((sX[1].X - sX[2].X), 2) + Math.Pow((sX[1].Y - sX[2].Y), 2)));

            int tmp = 0;
            if (sX.Count % 2 == 1)
                tmp = 1;

            List<Point> s1 = sX.GetRange(0, sX.Count / 2 + tmp);
            List<Point> s2 = sX.GetRange(sX.Count / 2 + tmp, sX.Count / 2);
            List<Point> sY1 = new List<Point>();
            List<Point> sY2 = new List<Point>();

            foreach (var i in sY)
                if (i.X <= s1.Last().X)
                    sY1.Add(i);
                else
                    sY2.Add(i);

            double delta = Math.Min(minDist(s1, sY1), minDist(s2, sY2));

            List<Point> deltaList = new List<Point>();
            foreach (var i in sY)
                if (delta > Math.Abs(i.X - sX[sX.Count / 2 + tmp].X))
                    deltaList.Add(i);

            double tmpD;
            for (int i = 0; i < deltaList.Count - 2; i++)
                for (int j = i + 1; delta > (deltaList[j].Y - deltaList[i].Y) && j < deltaList.Count; j++)
                {
                    tmpD = Math.Sqrt(Math.Pow((deltaList[i].X - deltaList[j].X), 2) + Math.Pow((deltaList[i].Y - deltaList[j].Y), 2));
                    if (delta > tmpD)
                        delta = tmpD;
                }

            return delta;
        }

        static void Main(string[] args)
        {
            List<Point> pList = new List<Point>();
            Console.WriteLine("Podaj ilosc punktow: ");
            int n = Convert.ToInt32(Console.ReadLine());
            for(int i=0;i<n;i++)
            {
                Point pTmp = new Point();
                Console.WriteLine("Podaj x[{0}]: ", i);
                pTmp.X = Double.Parse(Console.ReadLine());
                Console.WriteLine("Podaj y[{0}]: ", i);
                pTmp.Y = Double.Parse(Console.ReadLine());
                pList.Add(pTmp);
            }

            pList = pList.OrderBy(p => p.X).ToList();
            List<Point> pktY = pList.OrderBy(p => p.Y).ToList();

            Console.WriteLine("Minimalna odleglosc pomiedzy punktami wynosi: {0}", minDist(pList, pktY));
            Console.ReadLine();
        }
    }
}
