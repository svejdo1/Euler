using Barbar.SymbolicMath.Policies;
using Barbar.SymbolicMath.Rationals;

namespace Barbar.Euler.Problem093
{
    public class TermNode : Node
    {
        public override Rational<long, Int64Policy>? Evaluate()
        {
            return Value;
        }

        public Rational<long, Int64Policy> Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }

        public TermNode() : base(null, null)
        { }
    }
}
