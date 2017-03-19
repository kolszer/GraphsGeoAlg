using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDTree
{
    class Program
    {
        public struct Point
        {
            public double X;
            public double Y;
        }
        static string printTree(BinaryTree<Point> tree, int depth)
        {
            string res = tree.d.ToString();
            if(tree.left==null && tree.right==null)
                res += "   (" + tree.point.X + " , " + tree.point.Y + ")\n";
            else
                res += "  "+tree.line+ "\n";
            if (tree.left != null)
                res += "left    " + printTree(tree.left, depth + 1);
            if (tree.right != null)
                res += "right   " + printTree(tree.right, depth + 1);
            return res;
        }
        static Point getLeft(BinaryTree<Point> tree)
        {
            if (tree.left == null)
                return tree.point;
            return getLeft(tree.left);
        }
        static Point getRight(BinaryTree<Point> tree)
        {
            if (tree.right == null)
                return tree.point;
            return getRight(tree.right);
        }
        static List<Point> treeToList(BinaryTree<Point> tree)
        {
            if (tree.left == null & tree.right == null)
                return new List<Point>() { tree.point };
            List<Point> left = treeToList(tree.left);
            List<Point> right = treeToList(tree.right);
            return left.Concat(right).ToList();
        }
        static bool pointInRect(Point p, Point r1, Point r2)
        {
            Point p1 = r1;
            Point p2 = new Point() { X = r1.X, Y = r2.Y };
            Point p3 = r2;
            Point p4 = new Point() { X = r2.X, Y = r1.Y };

            //p1-p2
            double or1 = (p1.X - p2.X) * (p2.Y - p.Y) - (p1.Y - p2.Y) * (p2.X - p.X);
            //p2-p3
            double or2 = (p2.X - p3.X) * (p3.Y - p.Y) - (p2.Y - p3.Y) * (p3.X - p.X);
            //p3-p4
            double or3 = (p3.X - p4.X) * (p4.Y - p.Y) - (p3.Y - p4.Y) * (p4.X - p.X);
            //p4-p1
            double or4 = (p4.X - p1.X) * (p1.Y - p.Y) - (p4.Y - p1.Y) * (p1.X - p.X);

            if ((or1 > 0 && or2 > 0 && or3 > 0 && or4 > 0) ||
                (or1 < 0 && or2 < 0 && or3 < 0 && or4 < 0))
                return true;
            if (or1 == 0 &&
                (p.X <= Math.Max(p1.X, p2.X) && p.X >= Math.Min(p1.X, p2.X) && p.Y <= Math.Max(p1.Y, p2.Y) && p.Y >= Math.Min(p1.Y, p2.Y)))
                return true;
            if (or2 == 0 &&
                (p.X <= Math.Max(p2.X, p3.X) && p.X >= Math.Min(p2.X, p3.X) && p.Y <= Math.Max(p2.Y, p3.Y) && p.Y >= Math.Min(p2.Y, p3.Y)))
                return true;
            if (or3 == 0 &&
                (p.X <= Math.Max(p3.X, p4.X) && p.X >= Math.Min(p3.X, p4.X) && p.Y <= Math.Max(p3.Y, p4.Y) && p.Y >= Math.Min(p3.Y, p4.Y)))
                return true;
            if (or4 == 0 &&
                (p.X <= Math.Max(p4.X, p1.X) && p.X >= Math.Min(p4.X, p1.X) && p.Y <= Math.Max(p4.Y, p1.Y) && p.Y >= Math.Min(p4.Y, p1.Y)))
                return true;

            return false;
        }
        static BinaryTree<Point> kdTree(List<Point> points, int depth)
        {
            if (points.Count == 1)
                return new BinaryTree<Point>() { point = points[0], line = double.NaN, d = depth };
            
            if (depth % 2 == 0)
                points = points.OrderBy(p => p.X).ToList();
            else
                points = points.OrderBy(p => p.Y).ToList();

            int median = points.Count / 2;

            BinaryTree<Point> node = new BinaryTree<Point>();
            node.d = depth;
            if (depth % 2 == 0)
                node.line = points[median].X;
            else
                node.line = points[median].Y;

            node.left = kdTree(points.GetRange(0, median), depth + 1);
            node.right = kdTree(points.GetRange(median, points.Count - median), depth + 1);

            return node;
        }
        static List<Point> searchKdTree(BinaryTree<Point> tree, Point r1, Point r2)
        {
            if (tree.left == null && tree.right == null)
                if (pointInRect(tree.point,r1,r2))
                    return new List<Point>() { new Point() { X = tree.point.X, Y = tree.point.Y } };
                else
                    return null;
            if (pointInRect(getLeft(tree), r1, r2) && pointInRect(getRight(tree), r1, r2))
                return treeToList(tree);
            List<Point> res = new List<Point>();
            List<Point> left = searchKdTree(tree.left, r1, r2);
            List<Point> right = searchKdTree(tree.right, r1, r2);
            if (left != null)
                res.AddRange(left);
            if (right != null)
                res.AddRange(right);
            return res;
        }
        static BinaryTree<Point> kdTree(List<Point> pointsX, List<Point> pointsY, int depth)
        {
            if (pointsX.Count == 1)
                return new BinaryTree<Point>() { point = pointsX[0], line = double.NaN, d = depth };
            int median = pointsX.Count / 2;
            BinaryTree<Point> node = new BinaryTree<Point>();
            node.d = depth;
            List<Point> pointsTmpLeft = new List<Point>();
            List<Point> pointsTmpRight = new List<Point>();
            if (depth % 2 == 0)
            {
                node.line = pointsX[median].X;
                foreach (var i in pointsY)
                    if (i.X < pointsX[median].X)
                        pointsTmpLeft.Add(i);
                    else
                        pointsTmpRight.Add(i);
                node.left = kdTree(pointsX.GetRange(0, median), pointsTmpLeft, depth + 1);
                node.right = kdTree(pointsX.GetRange(median, pointsX.Count - median), pointsTmpRight, depth + 1);
            }
            else
            {
                foreach (var i in pointsX)
                    if (i.Y < pointsY[median].Y)
                        pointsTmpLeft.Add(i);
                    else
                        pointsTmpRight.Add(i);
                node.line = pointsY[median].Y;
                node.left = kdTree(pointsTmpLeft, pointsY.GetRange(0, median), depth + 1);
                node.right = kdTree(pointsTmpRight, pointsY.GetRange(median, pointsY.Count - median), depth + 1);
            }
            return node;
        }

        static void Main(string[] args)
        {
            List<Point> points = new List<Point>() { new Point() {X=2,Y=3},new Point() {X=5,Y=4},
                new Point() {X=9,Y=6},new Point() {X=4,Y=7},
                new Point() {X=8,Y=1},new Point() {X=7,Y=2} };
            Point r1 = new Point() { X = 3, Y = 3 };
            Point r2 = new Point() { X = 5, Y = 7 };

            List<Point> pointsX = points.OrderBy(p => p.X).ToList();
            List<Point> pointsY = points.OrderBy(p => p.Y).ToList();
            BinaryTree<Point> res = kdTree(pointsX, pointsY, 0);
            //BinaryTree<Point> res = kdTree(points, 0);
            Console.WriteLine(printTree(res,0));

            Console.WriteLine("!!!");

            List<Point> resS = searchKdTree(res, r1, r2);
            if (resS != null)
                resS.ForEach(p => Console.WriteLine(p.X + " " + p.Y));

            Console.ReadLine();
        }
    }
}
