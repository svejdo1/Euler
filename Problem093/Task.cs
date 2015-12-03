using Barbar.SymbolicMath.Policies;
using Barbar.SymbolicMath.Rationals;
using Barbar.SymbolicMath.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Barbar.Euler.Problem093
{
    public class Task : ITask
    {
        private static IEnumerable<OperandNode> AllBinaryTrees(int size)
        {
            if (size == 0)
                return new OperandNode[] { null };
            return from i in Enumerable.Range(0, size)
                   from left in AllBinaryTrees(i)
                   from right in AllBinaryTrees(size - 1 - i)
                   select new OperandNode(left, right);
        }

        private static IList<Tree> BuildTrees()
        {
            var trees = new List<Tree>();
            foreach (var node in AllBinaryTrees(3))
            {
                var operands = new List<OperandNode>();
                var terms = new List<TermNode>();
                var stack = new Stack<Node>();
                stack.Push(node);
                while (stack.Count > 0)
                {
                    var stackNode = stack.Pop();
                    operands.Add((OperandNode)stackNode);
                    if (stackNode.Left == null)
                    {
                        stackNode.Left = new TermNode();
                        terms.Add((TermNode)stackNode.Left);
                    }
                    else
                    {
                        stack.Push(stackNode.Left);
                    }

                    if (stackNode.Right == null)
                    {
                        stackNode.Right = new TermNode();
                        terms.Add((TermNode)stackNode.Right);
                    }
                    else
                    {
                        stack.Push(stackNode.Right);
                    }
                }
                var tree = new Tree { Root = node, Operands = operands.ToArray(), Terms = terms.ToArray() };
                trees.Add(tree);
            }
            return trees;
        }

        public string Run()
        {
            var indexes = new List<byte[]>();
            MathUtility.GetPermutations(new byte[] { 0, 1, 2, 3 }, indexes);

            int max = 0;
            string solution = null;

            bool[] buffer = new bool[9 * 8 * 7 * 6 + 1];
            var operands = new Operand[] { Operand.Add, Operand.Divide, Operand.Multiply, Operand.Substract };
            var trees = BuildTrees();
            for (var a = 1; a < 10; a++)
                for (var b = a + 1; b < 10; b++)
                    for (var c = b + 1; c < 10; c++)
                        for (var d = c + 1; d < 10; d++)
                        {
                            Array.Clear(buffer, 0, buffer.Length);
                            foreach (var tree in trees)
                                foreach (var index in indexes)
                                {
                                    tree.Terms[index[0]].Value = new Rational<long, Int64Policy>(a, 1);
                                    tree.Terms[index[1]].Value = new Rational<long, Int64Policy>(b, 1);
                                    tree.Terms[index[2]].Value = new Rational<long, Int64Policy>(c, 1);
                                    tree.Terms[index[3]].Value = new Rational<long, Int64Policy>(d, 1);

                                    foreach (var o1 in operands)
                                        foreach (var o2 in operands)
                                            foreach (var o3 in operands)
                                            {
                                                tree.Operands[0].Operand = o1;
                                                tree.Operands[1].Operand = o2;
                                                tree.Operands[2].Operand = o3;
                                                var result = tree.Root.Evaluate();
                                                if (result.HasValue)
                                                {
                                                    var term = result.Value.Normalize();
                                                    if (term.Denominator == 1 && term.Numerator >= 0)
                                                    {
                                                        buffer[term.Numerator] = true;
                                                    }
                                                }
                                            }
                                }
                            int i = 1;
                            while (buffer[i++]) ;
                            if (i > max)
                            {
                                solution = string.Format("{0}{1}{2}{3}", a, b, c, d);
                                max = i;
                            }
                        }
            return solution;
        }
    }
}
