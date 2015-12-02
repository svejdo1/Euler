using Barbar.SymbolicMath.Utilities;
using System;
using System.Collections.Generic;

namespace Barbar.Euler.Problem086
{
    public class Task : ITask
    {
        public const int LIMIT = 1000000;
        public struct Cuboid
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

        static int CuboidsCountOverflow(int dimensionLimit)
        {
            var cuboids = new HashSet<Cuboid>();
            var maxM = dimensionLimit;
            for (var m = 2; m < maxM; m++)
            {
                int nStart = (m & 1) == 1 ? 2 : 1;
                for (var n = nStart; n < m; n += 2)
                {
                    if (MathUtility.Gcd(n, m) == 1)
                    {
                        int z0 = m * m + n * n;
                        int y0 = m * m - n * n;
                        int x0 = 2 * m * n;
                        if (x0 < y0)
                        {
                            var xchg = x0;
                            x0 = y0;
                            y0 = xchg;
                        }
                        if (!HandleTriplet((int)x0, (int)y0, (int)z0, dimensionLimit, cuboids))
                        {
                            return LIMIT;
                        }
                        if (!HandleTriplet((int)y0, (int)x0, (int)z0, dimensionLimit, cuboids))
                        {
                            return LIMIT;
                        }
                        long x = x0, y = y0, z = z0;
                        while (y <= dimensionLimit || x <= dimensionLimit)
                        {
                            if (!HandleTriplet((int)x, (int)y, (int)z, dimensionLimit, cuboids))
                            {
                                return LIMIT;
                            }
                            if (!HandleTriplet((int)y, (int)x, (int)z, dimensionLimit, cuboids))
                            {
                                return LIMIT;
                            }
                            x += x0;
                            y += y0;
                            z += z0;
                        }
                    }
                }
            }
            return cuboids.Count;
        }

        private static bool HandleTriplet(int x, int y, int z, int dimensionLimit, HashSet<Cuboid> cuboids)
        {
            if (y > dimensionLimit)
            {
                return true;
            }
            long p1 = z * z;

            for (int j = 1; j < x; j++)
            {
                if (j <= dimensionLimit && x - j <= dimensionLimit)
                {
                    long a = j;
                    long b = (x - j);
                    if (a <= dimensionLimit && b <= dimensionLimit)
                    {
                        //long p1 = (a + b) * (a + b) + y * y;
                        long p2 = (a + y) * (a + y) + b * b;
                        long p3 = (y + b) * (y + b) + a * a;
                        if (p1 <= p2 && p1 <= p3)
                        {
                            var cuboid = new Cuboid((short)j, (short)(x - j), (short)y);
                            if (!cuboids.Contains(cuboid))
                            {
                                cuboids.Add(cuboid);
                                if (cuboids.Count >= LIMIT)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }

        static int Newton()
        {
            int start = 100;
            int end = 4000;
            while(start < end - 1)
            {
                var half = (end - start) / 2 + start;
                var halfValue = CuboidsCountOverflow(half);
                if (halfValue < LIMIT)
                {
                    start = half;
                    continue;
                }
                end = half;
            }
            return start;
        }

        public string Run()
        {
            return Convert.ToString(Newton() + 1);
        }
    }
}
