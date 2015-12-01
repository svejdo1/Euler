using Barbar.SymbolicMath.Vectors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbar.Euler.Problem096
{
    public class Task : ITask
    {
        private IList<string> ReadMatrixes()
        {
            var matrixes = new List<string>();
            using (var stream = typeof(Task).Assembly.GetManifestResourceStream(typeof(Task), "sudoku.txt"))
            {
                using (var reader = new StreamReader(stream))
                {
                    StringBuilder current = null;
                    do
                    {
                        string line = reader.ReadLine();
                        if (line == null)
                        {
                            if (current != null)
                            {
                                matrixes.Add(current.ToString());
                            }
                            break;
                        }
                        if (line.StartsWith("Grid", StringComparison.OrdinalIgnoreCase))
                        {
                            if (current != null)
                            {
                                matrixes.Add(current.ToString());
                            }
                            current = new StringBuilder();
                            continue;
                        }
                        current.Append(line);
                    } while (true);
                }
            }
            return matrixes;
        }

        const string rows = "ABCDEFGHI";
        static string cols = "123456789";
        static string digits = "123456789";
        static string[] squares = Cross(rows, cols);
        static Dictionary<string, IEnumerable<string>> peers;
        static Dictionary<string, IGrouping<string, string[]>> units;

        /*
         * def cross(A, B):
         *   return [a+b for a in A for b in B]
         */
        static string[] Cross(string A, string B)
        {
            return (from a in A from b in B select "" + a + b).ToArray();
        }

        static Task()
        {
            var unitlist = ((from c in cols select Cross(rows, c.ToString()))
                               .Concat(from r in rows select Cross(r.ToString(), cols))
                               .Concat(from rs in (new[] { "ABC", "DEF", "GHI" }) from cs in (new[] { "123", "456", "789" }) select Cross(rs, cs)));

            units = (from s in squares from u in unitlist where u.Contains(s) group u by s into g select g).ToDictionary(g => g.Key);
            peers = (from s in squares from u in units[s] from s2 in u where s2 != s group s2 by s into g select g).ToDictionary(g => g.Key, g => g.Distinct());

        }

        static string[][] Zip(string[] A, string[] B)
        {
            var n = Math.Min(A.Length, B.Length);
            string[][] sd = new string[n][];
            for (var i = 0; i < n; i++)
            {
                sd[i] = new string[] { A[i].ToString(), B[i].ToString() };
            }
            return sd;
        }

        /// <summary>Given a string of 81 digits (or . or 0 or -), return a dict of {cell:values}</summary>
        public static Dictionary<string, string> ParseGrid(string grid)
        {
            var grid2 = from c in grid where "0.-123456789".Contains(c) select c;
            var values = squares.ToDictionary(s => s, s => digits); //To start, every square can be any digit

            foreach (var sd in Zip(squares, (from s in grid select s.ToString()).ToArray()))
            {
                var s = sd[0];
                var d = sd[1];

                if (digits.Contains(d) && Assign(values, s, d) == null)
                {
                    return null;
                }
            }
            return values;
        }

        /// <summary>Using depth-first search and propagation, try all possible values.</summary>
        public static Dictionary<string, string> Search(Dictionary<string, string> values)
        {
            if (values == null)
            {
                return null; // Failed earlier
            }
            if (IsEveryElementNotNull(from s in squares select values[s].Length == 1 ? "" : null))
            {
                return values; // Solved!
            }

            // Chose the unfilled square s with the fewest possibilities
            var s2 = (from s in squares where values[s].Length > 1 orderby values[s].Length ascending select s).First();

            return FirstNotNullOrDefault(from d in values[s2]
                        select Search(Assign(new Dictionary<string, string>(values), s2, d.ToString())));
        }

        /// <summary>Eliminate all the other values (except d) from values[s] and propagate.</summary>
        static Dictionary<string, string> Assign(Dictionary<string, string> values, string s, string d)
        {
            if (IsEveryElementNotNull(
                    from d2 in values[s]
                    where d2.ToString() != d
                    select Eliminate(values, s, d2.ToString())))
            {
                return values;
            }
            return null;
        }


        /// <summary>Eliminate d from values[s]; propagate when values or places &lt;= 2.</summary>
        static Dictionary<string, string> Eliminate(Dictionary<string, string> values, string s, string d)
        {
            if (!values[s].Contains(d))
            {
                return values;
            }
            values[s] = values[s].Replace(d, "");
            if (values[s].Length == 0)
            {
                return null; //Contradiction: removed last value
            }
            else if (values[s].Length == 1)
            {
                //If there is only one value (d2) left in square, remove it from peers
                var d2 = values[s];
                if (!IsEveryElementNotNull(from s2 in peers[s] select Eliminate(values, s2, d2)))
                {
                    return null;
                }
            }

            //Now check the places where d appears in the units of s
            foreach (var u in units[s])
            {
                var dplaces = from s2 in u where values[s2].Contains(d) select s2;
                if (dplaces.Count() == 0)
                {
                    return null;
                }
                else if (dplaces.Count() == 1)
                {
                    // d can only be in one place in unit; assign it there
                    if (Assign(values, dplaces.First(), d) == null)
                    {
                        return null;
                    }
                }
            }
            return values;
        }

        static bool IsEveryElementNotNull<T>(IEnumerable<T> sequence)
        {
            foreach (var e in sequence)
            {
                if (e == null)
                {
                    return false;
                }
            }
            return true;
        }

        static T FirstNotNullOrDefault<T>(IEnumerable<T> sequence)
        {
            foreach (var e in sequence)
            {
                if (e != null)
                {
                    return e;
                }
            }
            return default(T);
        }

        public string Run()
        {
            var matrixes = ReadMatrixes();
            int sum = 0;
            foreach (var matrix in matrixes)
            {
                var result = Search(ParseGrid(matrix));
                sum += Convert.ToInt32(result["A1"]) * 100 +
                    Convert.ToInt32(result["A2"]) * 10 +
                    Convert.ToInt32(result["A3"]);
            }

            return Convert.ToString(sum);
        }
    }
}
