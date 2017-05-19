using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitonicTSP
{
    public struct Point
    {
        public double X, Y;
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public override string ToString()
        {
            return (String.Format("({0},{1})", X, Y));
        }
    }

    class Program
    {
        public static List<double> bTSP(List<Point> p)
        {
            double[] b = new double[p.Count];
            double min, sum, d;
            b[1] = dist(p[0], p[1]);
            for(int j=2;j<p.Count;j++)
            {
                min = double.PositiveInfinity;
                sum = 0;
                for(int i=j-1;i>0;i--)
                {
                    d = b[i + 1] + dist(p[i], p[j]) + sum;
                    if (d < min)
                        min = d;
                    sum = sum + dist(p[i], p[i + 1]);
                }
                b[j] = min;
            }
            return b.ToList();
        }

        public static double dist(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        static void Main(string[] args)
        {
            List<Point> pkt = new List<Point>() { new Point(1, 1), new Point(8,8), new Point(4,4), new Point(2,2) };
            List<double> res = bTSP(pkt);
            res.ForEach(p => Console.WriteLine(p));
            Console.ReadKey();
        }
    }
}
