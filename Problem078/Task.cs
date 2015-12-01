using System;
using System.Collections.Generic;

namespace Barbar.Euler.Problem078
{
    public class Task : ITask
    {

        public string Run()
        {
            var p = new List<int> { 1 };

            int n = 1;
            while (true)
            {
                int i = 0;
                int penta = 1;
                p.Add(0);

                while (penta <= n)
                {
                    int sign = (i % 4 > 1) ? -1 : 1;
                    p[n] += sign * p[n - penta];
                    p[n] %= 1000000;
                    i++;

                    int j = (i % 2 == 0) ? i / 2 + 1 : -(i / 2 + 1);
                    penta = j * (3 * j - 1) / 2;
                }

                if (p[n] == 0) break;
                n++;
            }
            return Convert.ToString(n);
        }
    }
}
