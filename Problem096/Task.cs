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
        private IList<Matrix<byte>> ReadMatrixes()
        {
            var matrixes = new List<Matrix<byte>>();
            using (var stream = typeof(Task).Assembly.GetManifestResourceStream(typeof(Task), "sudoku.txt"))
            {
                using (var reader = new StreamReader(stream))
                {
                    Matrix<byte> current = null;
                    int row = 0;
                    do
                    {
                        string line = reader.ReadLine();
                        if (line == null)
                        {
                            if (current != null)
                            {
                                matrixes.Add(current);
                            }
                            break;
                        }
                        if (line.StartsWith("Grid", StringComparison.OrdinalIgnoreCase))
                        {
                            if (current != null)
                            {
                                matrixes.Add(current);
                            }
                            current = new Matrix<byte>(9, 9);
                            row = 0;
                            continue;
                        }

                        byte[] parts = line.ToCharArray().Select(a => (byte)((int)a - '0')).ToArray();
                        current.SetRow(row, parts);
                        row++;
                    } while (true);
                }
            }
            return matrixes;
        }

        public string Run()
        {
            var matrixes = ReadMatrixes();
            foreach (var matrix in matrixes)
            {
                var solution = SolveMatrix(matrix);
            }

            return Convert.ToString(0);
        }



        private Matrix<byte> SolveMatrix(Matrix<byte> matrix)
        {
            var minHeap = new MinHeap<Sudoku>();
            var sudoku = new Sudoku(matrix);
            minHeap.Add(sudoku);
            long iterations = 0;

            Sudoku best = sudoku;
            while (best.Score != 0)
            {
                iterations++;
                best = minHeap.ExtractDominating();
                var toBeAdded = new List<Sudoku>();
                for (var x = 0; x < 9; x++)
                    for (var y = 0; y < 9; y++)
                    {
                        if (best[x, y] == 0)
                        {
                            var clone = new Sudoku(best) { PathLength = best.PathLength + 1 };
                            if (clone.InjectNumber(x, y))
                            {
                                toBeAdded.Add(clone);
                            }
                            else
                            {
                                toBeAdded = null;
                                x = 9;
                                y = 9;
                                break;
                            }
                        }
                    }
                if (toBeAdded != null)
                {
                    foreach (var s in toBeAdded)
                    {
                        minHeap.Add(s);
                    }
                }
            }
            return best.Matrix;
        }
    }
}
