using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCenter
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
        static List<Point> kCenter(List<Point> p, int k)
        {
            Point[] s = new Point[k];
            double[] d = new double[p.Count];
            double[] r = new double[p.Count];

            for (int i = 0; i < p.Count; i++) d[i] = double.PositiveInfinity;

            s[0] = p[0];

            for (int j = 0; j < k; j++)
            {
                r[j] = 0;
                for (int i = 0; i < p.Count; i++)
                {
                    d[i] = Math.Min(d[i], dist(p[i], s[j]));
                    if (r[j] < d[i] && j + 1 < k)
                    {
                        r[j] = d[i];
                        s[j + 1] = p[i];
                    }
                }
            }

            return s.ToList();
        }

        public static double dist(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        static void Main(string[] args)
        {
            List<Point> pkt = new List<Point>() { new Point(9, 9), new Point(-1, -1), new Point(11, 11), new Point(10, 10), new Point(0, 0), new Point(1, 1), new Point(100, 100) };
            List<Point> res = kCenter(pkt, 3);
            res.ForEach(p => Console.WriteLine(p.ToString()));

            Console.ReadKey();
        }
    }
}
