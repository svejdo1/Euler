namespace Barbar.Euler.Problem093
{
    public class Tree
    {
        public Node Root { get; set; }
        public TermNode[] Terms { get; set; }
        public OperandNode[] Operands { get; set; }

        public override string ToString()
        {
            return Root.ToString();
        }
    }
}
