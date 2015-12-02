using Barbar.SymbolicMath.Utilities;
using System.Collections.Generic;

namespace Barbar.Euler.Problem086
{
    public class Task : ITask
    {

        struct Cuboid
        {
            private short a, b, c;

            public Cuboid(short _a, short _b, short _c)
            {
                if (_a > _c)
                    swap(ref _a, ref _c);

                if (_a > _b)
                    swap(ref _a, ref _b);

                if (_b > _c)
                    swap(ref _b, ref _c);

                a = _a;
                b = _b;
                c = _c;
            }

            private static void swap(ref short _a, ref short _b)
            {
                var xchg = _a;
                _a = _b;
                _b = xchg;
            }

            public override bool Equals(object obj)
            {
                var cuboid = (Cuboid)obj;
                return a == cuboid.a && b == cuboid.b && c == cuboid.c;
            }

            public override string ToString()
            {
                return string.Format("{0},{1},{2}", a, b, c);
            }

            public override int GetHashCode()
            {
                return ((((int)a) << 16) | ((int)b)) ^ ((int)c);
            }
        }

        static long Triangles(int limit)
        {
            long result = 0;
            HashSet<Cuboid> uniques = new HashSet<Cuboid>();
            var maxM = limit;
            for (var m = 2L; m < maxM; m++)
            {
                long nStart = (m & 1) == 1 ? 2L : 1L;
                for (var n = nStart; n < m; n += 2)
                {
                    if (MathUtility.Gcd(n, m) == 1)
                    {
                        long c0 = m * m + n * n;
                        long b0 = m * m - n * n;
                        long a0 = 2 * m * n;
                        if (a0 < b0)
                        {
                            var xchg = a0;
                            a0 = b0;
                            b0 = xchg;
                        }
                        if (b0 <= limit && a0 <= 2 * limit)
                        {
                            result += HandleTriangle(a0, b0, limit, uniques);
                        }
                        long a = a0, b = b0, c = c0;
                        while (b <= limit && a <= 2 * limit)
                        {
                            result += HandleTriangle(a, b, limit, uniques);
                            a += a0;
                            b += b0;
                            c += c0;
                        }
                    }
                }
            }
            return result;
        }

        private static long HandleTriangle(long a, long b, int limit, HashSet<Cuboid> uniques)
        {
            int result = 0;
            for (int j = 1; j < a; j++)
            {
                if (j <= limit && a - j <= limit)
                {
                    int ta = j;
                    int tb = (int)(a - j);
                    int tc = (int)b;
                    if (tb <= ta && tc <= ta )
                    {
                        var cuboid = new Cuboid((short)j, (short)(a - j), (short)b);
                        if (!uniques.Contains(cuboid))
                        {
                            result++;
                            uniques.Add(cuboid);
                        }
                    }
                    else
                    {
                        //throw new Exception();
                    }
                }
            }
            return result;
        }
        public string Run()
        {
            return string.Empty;
        }
    }
}
