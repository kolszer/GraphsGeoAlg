using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalTree
{
    class Program
    {
        public struct Point
        {
            public double X;
            public double Y;
        }

        public struct Line
        {
            public Point P;
            public Point Q;
        }

        static string printTree(BinaryTree<Line> tree, int depth)
        {
            string res = "depth: " + depth + " tree m: " + tree.medX;
            if (tree.leftList != null)
            {
                res += " Lleft: ";
                foreach (var i in tree.leftList)
                {
                    res += "|(" + i.P.X + "," + i.P.Y + "),(" + i.Q.X + "," + i.Q.Y + ")| ";
                }
            }
            if (tree.rightList != null)
            {
                res += " Lright: ";
                foreach (var i in tree.rightList)
                {
                    res += "|(" + i.P.X + "," + i.P.Y + "),(" + i.Q.X + "," + i.Q.Y + ")| ";
                }
            }

            if (tree.left != null)
                res += "\nleft:" + printTree(tree.left, depth + 1);
            if (tree.right != null)
                res += "\nright:" + printTree(tree.right, depth + 1);
            return res;
        }

        static BinaryTree<Line> constructIntervalTree(List<Line> s)
        {
            //Jesli lista jest pusta zwracam pusty wezel
            if (s.Count == 0)// || s==null
                return null;

            //Stworz wezel
            BinaryTree<Line> node = new BinaryTree<Line>();

            //Oblicz medianę xmed(v) zb. koncow przedzialow i zapamietaj
            List<Point> tmpMed = new List<Point>();
            foreach (var i in s)
            {
                tmpMed.Add(i.P);
                tmpMed.Add(i.Q);
            }
            tmpMed = tmpMed.OrderBy(p => p.X).ToList();
            node.medX = tmpMed[tmpMed.Count / 2].X;

            //Wyznacz Slewy
            List<Line> sLeft = new List<Line>();

            //Wyznacz Smed
            List<Line> sMed = new List<Line>();

            //Wyznacz Sprawy
            List<Line> sRight = new List<Line>();

            //Wyznaczenie sLeft, sMed i sRight i przypisanie ich
            foreach (Line i in s)
            {
                //Jesli sprawdzana linia lezy calkowicie na lewo od linii mediany
                if (i.P.X < node.medX && i.Q.X < node.medX)
                    sLeft.Add(i);
                //Jesli sprawdzana linia lezy calkowicie na prawo od linii mediany
                else if (i.P.X > node.medX && i.Q.X > node.medX)
                    sRight.Add(i);
                //W przeciwnym wypadku linia lezy przy linii mediany
                else
                    sMed.Add(i);
            }

            //Stworz dwie posortowane listy dla Smed: liste lstlewy posortowana wzgledem lewych koncow i prawych koncow
            //Zapamietaj 2 powyzsze listy w v
            node.leftList = sMed.OrderBy(p => p.P.X).ToList();
            node.rightList = sMed.OrderByDescending(p => p.Q.X).ToList();

            //v.left = construct(Slewy)
            node.left = constructIntervalTree(sLeft);

            //v.right = construct(Sprawy)
            node.right = constructIntervalTree(sRight);

            return node;
        }

        static List<Line> QueryIntervalTree(BinaryTree<Line> tree, double x)
        {
            if (tree == null)
                return new List<Line>();
            List<Line> lst = new List<Line>();
            if (x <= tree.medX)
            {
                foreach (var i in tree.leftList)
                {
                    if ((i.P.X <= x && i.Q.X >= x) || (i.P.X <= x && i.Q.X >= x))
                        lst.Add(i);
                    else
                        break;
                }
                lst.AddRange(QueryIntervalTree(tree.left, x));
            }
            else
            {
                foreach (var i in tree.rightList)
                {
                    if ((i.P.X <= x && i.Q.X >= x) || (i.P.X <= x && i.Q.X >= x))
                        lst.Add(i);
                    else
                        break;
                }
                lst.AddRange(QueryIntervalTree(tree.right, x));
            }

            return lst;
        }
        static void Main(string[] args)
        {
            List<Line> lineList = new List<Line>() {
                new Line() { P=new Point() {X= 2,Y=5}, Q=new Point() {X= 8,Y=5} },
                new Line() { P=new Point() {X= 2,Y=7}, Q=new Point() {X= 13,Y=7} },
                new Line() { P=new Point() {X= 2,Y=8}, Q=new Point() {X= 11,Y=8} },
                new Line() { P=new Point() {X= 9,Y=3}, Q=new Point() {X= 11,Y=3} },
                new Line() { P=new Point() {X= 11,Y=5}, Q=new Point() {X= 13,Y=5} },
                new Line() { P=new Point() {X= 5,Y=3}, Q=new Point() {X= 5,Y=9} },
                new Line() { P=new Point() {X= 7,Y=3}, Q=new Point() {X= 7,Y=9} },
                new Line() { P=new Point() {X= 10,Y=5}, Q=new Point() {X= 10,Y=10} }
                };

            BinaryTree<Line> res = constructIntervalTree(lineList);
            Console.WriteLine(printTree(res, 0));
            List<Line> ress = QueryIntervalTree(res, 5);
            foreach (var i in ress)
                Console.WriteLine(i.P.X + " " + i.P.Y + " " + i.Q.X + " " + i.Q.Y);


            Console.ReadKey();
        }
    }
}
