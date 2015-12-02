using Barbar.SymbolicMath.Utilities;
using System.Numerics;

namespace Barbar.Euler.Problem100
{
    /// <summary>
    /// We know that \frac{x}{z}\frac{x-1}{z-1}=\frac{1}{2}
    /// which leads to 2x^2-2x-z^2+z=0
    /// x can be than expressed as
    /// x=\frac{1}{2} + \frac{\sqrt{2z^2-2z+1}}{2}
    /// if we substitue T^2=2z^2-2z+1 (where T is integer)
    /// we can express z as z=\frac{1}{2}+\frac{\sqrt{2T^2-1}}{2}
    /// and we substitue again 2T^2-1=U^2 (where U is integer)
    /// we get U^2-2T^2=-1 (negative Pell's equation)
    /// funtamental solution is for this equation is (1,1) and
    /// recurrent pattern is 
    /// x_{n+1}=3x_n+4y_n 
    /// y_{n+1}=2x_n+3y_n
    /// </summary>
    public class Task : ITask
    {

        public string Run()
        {
            var x = BigInteger.One;
            var y = BigInteger.One;
            var limit = new BigInteger(1000000000000);
            while (true)
            {
                var newX = 3 * x + 4 * y;
                y = 2 * x + 3 * y;
                x = newX;

                var z = MathUtility.Sqrt(2 * y * y - 1);
                if (!z.IsEven)
                {
                    z = (z + 1) / 2;

                    var t = 2 * z * z - 2 * z + 1;
                    var st = MathUtility.Sqrt(t);
                    if (st * st == t && !st.IsEven)
                    {
                        var part = (st + 1) / 2;
                        if (z > limit)
                        {
                            return part.ToString();
                        }
                    }
                }
            };
        }
    }
}
