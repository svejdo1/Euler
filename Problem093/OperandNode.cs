using Barbar.SymbolicMath.Policies;
using Barbar.SymbolicMath.Rationals;
using System;

namespace Barbar.Euler.Problem093
{
    public sealed class OperandNode : Node
    {
        public Operand Operand { get; set; }

        public override string ToString()
        {
            char o;
            switch (Operand)
            {
                case Operand.Add:
                    o = '+';
                    break;
                case Operand.Divide:
                    o = '/';
                    break;
                case Operand.Multiply:
                    o = '*';
                    break;
                case Operand.Substract:
                    o = '-';
                    break;
                default:
                    throw new NotImplementedException();
            }

            return string.Format("({0}{1}{2})", Left, o, Right);
        }

        public override Rational<long, Int64Policy>? Evaluate()
        {
            var left = Left.Evaluate();
            var right = Right.Evaluate();
            if (!right.HasValue || !left.HasValue)
            {
                return null;
            }
            switch (Operand)
            {
                case Operand.Add:
                    return left.Value + right.Value;
                case Operand.Divide:
                    if (right.Value.Numerator == 0)
                    {
                        return null;
                    }
                    return left.Value / right.Value;
                case Operand.Multiply:
                    return left.Value * right.Value;
                case Operand.Substract:
                    return left.Value - right.Value;
                default:
                    throw new NotImplementedException();
            }
        }
        
        public OperandNode(Node left, Node right) : base(left, right)
        {
        }
    }
}
