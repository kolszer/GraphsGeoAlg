using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxPoints
{
    class Program
    {
        public struct Point
        {
            public double X;
            public double Y;
        }
        public static List<Point> DetectMaxima(List<Point> s)
        {
            List<Point> sSort = s.OrderBy(p => p.X).ToList();
            List<Point> d = new List<Point>();
            d.Add(sSort[0]);
            double yMax = sSort[0].Y, yMin = sSort[0].Y;
            for (int i = 1; i < sSort.Count; i++)
            {
                if (sSort[i].Y > yMax)
                {
                    d.Add(sSort[i]);
                    yMax = sSort[i].Y;
                }
                if (sSort[i].Y < yMin)
                {
                    d.Add(sSort[i]);
                    yMin = sSort[i].Y;
                }
            }
            d.Add(sSort.Last());
            yMax = sSort.Last().Y;
            yMin = sSort.Last().Y;
            for (int i = sSort.Count-1; i > 1; i--)
            {
                if (sSort[i].Y > yMax)
                {
                    d.Add(sSort[i]);
                    yMax = sSort[i].Y;
                }
                if (sSort[i].Y < yMin)
                {
                    d.Add(sSort[i]);
                    yMin = sSort[i].Y;
                }
            }
            d = d.Distinct().ToList();
            return d;
        }
        static void Main(string[] args)
        {
            List<Point> pktList = new List<Point>() {
                new Point() {X=4, Y=4 },
                new Point() {X=8, Y=5 },
                new Point() {X=3, Y=6 },
                new Point() {X=5, Y=5 },
                new Point() {X=1, Y=3 },
                new Point() {X=5, Y=1 },
                new Point() {X=2, Y=2 },
                new Point() {X=6, Y=7 },
                new Point() {X=5, Y=3 },
                new Point() {X=7, Y=4 }};
            foreach (var i in pktList)
                Console.WriteLine(i.X + " " + i.Y);
            Console.WriteLine("==========");
            List<Point> res = DetectMaxima(pktList);
            foreach (var i in res)
                Console.WriteLine(i.X + " " + i.Y);

            Console.ReadLine();

        }
    }
}
