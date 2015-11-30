using System;
using System.Collections.Generic;
using System.Linq;

namespace Barbar.Euler.Problem104
{
    public class Task : ITask
    {
        public string Run()
        {
            var set = new HashSet<byte>
            {
                1,2,3,4,5,6,7,8,9
            };
            var f0 = new DecimalInteger(1);
            var f1 = new DecimalInteger(1);
            for(var i = 3; i < int.MaxValue; i++)
            {
                var xchg = f0.Clone();
                f0.Add(f1);
                f1 = xchg;
                if (f0.Count >= 18)
                {
                    var start = f0.Value.Take(9);
                    var end = f0.Value.Skip(f0.Count - 9).Take(9);
                    if (set.SetEquals(start) && set.SetEquals(end))
                        return Convert.ToString(i);
                }

            }
            return string.Empty;

        }
    }
}
