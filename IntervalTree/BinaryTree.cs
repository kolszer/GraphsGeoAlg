using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalTree
{
    public class BinaryTree<T>
    {
        public T line;
        public List<T> leftList;
        public List<T> rightList;
        public double medX;
        public BinaryTree<T> left;
        public BinaryTree<T> right;
    }
}
