using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Barbar.Euler.Problem083
{
    public class Task : ITask
    {
        public string Run()
        {
            var matrix = new List<int[]>();
            using (var stream = typeof(Task).Assembly.GetManifestResourceStream(typeof(Task), "matrix.txt"))
            {
                using (var reader = new StreamReader(stream))
                {
                    do
                    {
                        string line = reader.ReadLine();
                        if (line == null)
                            break;
                        int[] parts = line.Split(new char[] { ',' }).Select(c => Convert.ToInt32(c)).ToArray();
                        matrix.Add(parts);
                    } while (true);
                }
            }

            var dijskra = new Dijskra<Point2D>();
            for (var x = 0; x < 80; x++)
                for (var y = 0; y < 80; y++)
                {
                    var edges = new Dictionary<Point2D, long>();
                    if (x < 79)
                    {
                        edges.Add(new Point2D(x + 1, y), matrix[y][x + 1]);
                    }
                    if (y < 79)
                    {
                        edges.Add(new Point2D(x, y + 1), matrix[y + 1][x]);
                    }
                    if (x > 0)
                    {
                        edges.Add(new Point2D(x - 1, y), matrix[y][x - 1]);
                    }
                    if (y > 0)
                    {
                        edges.Add(new Point2D(x, y - 1), matrix[y - 1][x]);
                    }
                    dijskra.AddVertex(new Point2D(x, y), edges);
                }
            var path = dijskra.ShortestPath(new Point2D(0, 0), new Point2D(79, 79));
            long sum = matrix[0][0];
            foreach(var point in path)
            {
                sum += matrix[point.Y][point.X];
            }


            return Convert.ToString(sum);
        }
    }
}
