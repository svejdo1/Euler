using Barbar.SymbolicMath.Utilities;
using System;

namespace Barbar.Euler.Problem077
{
    public class Task : ITask
    {
        public string Run()
        {
            int target = 2;
            int[] primes = new int[1000];
            int j = 0;
            for (var i = 2; i < int.MaxValue; i++)
            {
                if (MathUtility.IsPrime(i))
                {
                    primes[j++] = i;
                    if (j == primes.Length)
                        break;
                }
            }

            while (true)
            {
                int[] ways = new int[target + 1];
                ways[0] = 1;

                for (int i = 0; i < primes.Length; i++)
                {
                    for (j = primes[i]; j <= target; j++)
                    {
                        ways[j] += ways[j - primes[i]];
                    }
                }

                if (ways[target] > 5000)
                {
                    return Convert.ToString(target);
                }
                target++;
            }
        }
    }
}
