using System.Numerics;

namespace Barbar.Euler.Problem100
{
    public class Task : ITask
    {

        public static BigInteger Sqrt(BigInteger n)
        {
            int bitLength = (int)BigInteger.Log(n, 2);
            var root = BigInteger.One << (bitLength >> 1);

            while (n != root * root)
            {
                root = (root + (n / root)) >> 1;
            }
            return root;
        }


        public string Run()
        {
            var ps = new BigInteger(2828427124744);
            var z = BigInteger.Pow(new BigInteger(10), 12);
            while(true)
            {
                var t = 8 * z * (z - 1) + 4;
                var sqrt = Sqrt(t);
                if (sqrt * sqrt == t)
                {
                    break;
                }
                z++;
            }
            return string.Empty;
        }
    }
}
