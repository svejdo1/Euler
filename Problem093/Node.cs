using Barbar.SymbolicMath.Policies;
using Barbar.SymbolicMath.Rationals;

namespace Barbar.Euler.Problem093
{
    public abstract class Node
    {
        public Node Left { get; set; }
        public Node Right { get; set; }

        public abstract Rational<long, Int64Policy>? Evaluate();

        public Node(Node left, Node right)
        {
            Left = left;
            Right = right;
        }
    }
}
