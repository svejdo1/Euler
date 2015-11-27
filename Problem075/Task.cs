using Barbar.SymbolicMath.Utilities;
using System;

namespace Barbar.Euler.Problem075
{
    public class Task : ITask
    {
        public struct Triangle
        {
            public int A, B, C;

            public int L
            {
                get { return A + B + C; }
            }

            public override bool Equals(object obj)
            {
                var triangle = (Triangle)obj;
                return triangle.A == A && triangle.B == B;
            }

            public override int GetHashCode()
            {
                return A ^ B;
            }
        }
        
        const int LIMIT = 1500000;

        public string Run()
        {
            int result = 0;
            int[] triangles = new int[LIMIT + 1];
            var maxM = (long)Math.Sqrt(LIMIT / 2);
            for(var m = 2L; m < maxM; m++)
            {
                long nStart = (m & 1) == 1 ? 2L : 1L;
                for(var n = nStart; n < m; n += 2)
                {
                    if (MathUtility.Gcd(n, m) == 1)
                    {
                        long a = m * m + n * n;
                        long b = m * m - n * n;
                        long c = 2 * m * n;
                        long p = a + b + c;
                        while (p <= LIMIT)
                        {
                            triangles[p]++;
                            if (triangles[p] == 1)
                            {
                                result++;
                            }
                            if (triangles[p] == 2)
                            {
                                result--;
                            }
                            p += a + b + c;
                        }
                    }
                }
            }

            return Convert.ToString(result);
        }
    }
}
