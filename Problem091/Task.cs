using System;
using System.Collections.Generic;

namespace Barbar.Euler.Problem091
{
    public class Task : ITask
    {
        const int LIMIT = 50;

        public string Run()
        {
            var solutions = new HashSet<int>();
            var sizes = new Dictionary<int, HashSet<Point2D>>();
            for (var x2 = 0; x2 <= LIMIT; x2++)
                for (var y2 = 0; y2 <= LIMIT; y2++)
                    for (var x3 = 0; x3 <= LIMIT; x3++)
                        for (var y3 = 0; y3 <= LIMIT; y3++)
                        {
                            if ((0 != x2 || 0 != y2) && (0 != x3 || 0 != y3) && (x2 != x3 || y2 != y3))
                            {
                                int a = (x2) * (x2) + (y2) * (y2);
                                int b = (x3) * (x3) + (y3) * (y3);
                                int c = (x3 - x2) * (x3 - x2) + (y3 - y2) * (y3 - y2);
                                int xchg;
                                if (a > c)
                                {
                                    xchg = a;
                                    a = c;
                                    c = xchg;
                                }
                                if (b > c)
                                {
                                    xchg = b;
                                    b = c;
                                    c = xchg;
                                }
                                if (c == a + b)
                                {
                                    int solution = x2 | (y2 << 8) | (x3 << 16) | (y3 << 24);
                                    solutions.Add(solution);
                                }
                            }

                        }

            return Convert.ToString(solutions.Count / 2);
        }
    }
}
