using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var i = Sibling(123);
        }

        /**
           Given a positive integer, compute the largest Sibling number. A sibling is any number that 
           contains all of the same digits in the same counts. 

           For example: 
           1323, 1233, and 3321 are all siblings with 3321 as the largest sibling, and 1233 as the smallest.
         */
        public static int Sibling(int N)
        {
            var digits = new List<int>();
            // decompose the list into the digits, in order. 
            while (N > 0)
            {
                digits.Add(N % 10);
                N /= 10;
            }
            // sorts the digits ascending, could have sorted descending, but this is just as effective. 
            // Perhaps a smidge slower than a pure enumeration implemention. Timings would need to be run to be sure.
            digits.Sort();
            // now basically base-10 left shift and add until we're out of digits. (technically we're adding then left 
            // shifting... but since we only add the final digit the same result is obtained.)
            N = 0;
            for (int i = digits.Count - 1; i > 0; i--)
            {
                N += digits[i];
                N *= 10;
            }
            N += digits[0];
            return N;
        }

        public class Tree
        {
            public int x;
            public Tree l;
            public Tree r;
        };

        /**
            Given a binary tree (of type Tree above) compute the visible node count.
            A node is visible if it's ancestor's values (x) are less than or equal to its own value (x)
            The root of the tree is assumed to be always be visible.
         */
        public static int TreeVisibility(Tree T)
        {
            // write your code in C# 6.0 with .NET 4.5 (Mono)
            return TreeVisibility(T,int.MinValue);
        }

        public static int TreeVisibility(Tree T, int maxVis)
        {
            // This is a purely recursive implementation. It won't scale well (performance, nor running to completion) for large trees.
            // This is because a) CLR-stack pushes and pops are more expensive than manipulating the Stack generic class; and 
            // b) we will run out of stack space on trees that are thousands of nodes deep. Even in the CLR stack space is 
            // more precious than "heap."
            int lc = 0;
            int rc = 0;

            // if we have a left node, recursively count the visible nodes beneath. 
            // We also reset maximum value for determining visibility in this call. If, by chance, this node is larger, the child nodes need to know it.
            if (T.l != null) lc = TreeVisibility(T.l, maxVis >= T.x ? maxVis : T.x);
            // if we have a right node, recursively count the visible nodes beneath.
            // We also reset maximum value for determining visibility in this call. If, by chance, this node is larger, the child nodes need to know it.
            if (T.r != null) rc = TreeVisibility(T.r, maxVis >= T.x ? maxVis : T.x);
            // return the result of if this node is visible summed with the left and right counts.
            return lc+rc+T.x>=maxVis ? 1 : 0;
        }

    }
}