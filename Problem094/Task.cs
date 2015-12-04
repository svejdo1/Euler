using System;

namespace Barbar.Euler.Problem094
{
    /// <summary>
    /// We know that T=\frac{b}{4}\sqrt{4a^2-b^2} where a is the size of two sides of triangle
    /// 
    /// </summary>
    public class Task : ITask
    {
        const int LIMIT = 1000000000;
        private static bool IsPerfectSquare(long n, out int result)
        {
            if (n < 0 || ((n & 2) != 0) || ((n & 7) == 5) || ((n & 11) == 8))
            {
                result = 0;
                return false;
            }

            result = (int)Math.Sqrt(n);
            return (long)result * result == n;
        }

        private static bool IsValid(long _4a2, long a, long b)
        {
            if (2 * a + b < LIMIT)
            {
                var b2 = ((long)b) * b;
                var root = _4a2 - b2;
                int sqrt;
                if (IsPerfectSquare(root, out sqrt) && (((sqrt * b) & 3) == 0))
                {
                    return true;
                }
            }
            return false;
        }

        public string Run()
        {
            long result = 0;
            const int SIDE_LIMIT = LIMIT / 3 + 1;

            for (var a = 2L; a <= SIDE_LIMIT; a++)
            {
                var _4a2 = (a * a) << 2;
                if (IsValid(_4a2, a, a - 1))
                {
                    result += 3 * a -  1;
                }
                if (IsValid(_4a2, a, a + 1))
                {
                    result += 3 * a + 1;
                }
            }
            return result.ToString();
        }
    }
}
