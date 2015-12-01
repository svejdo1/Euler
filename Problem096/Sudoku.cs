using Barbar.SymbolicMath.Vectors;
using System;
using System.Collections.Generic;

namespace Barbar.Euler.Problem096
{
    public class Sudoku : IComparable<Sudoku>
    {
        private Matrix<byte> m_Matrix;
        private int m_Score;
        private bool m_ScoreCalculated;
        public int PathLength { get; set; }

        public Matrix<byte> Matrix
        {
            get { return m_Matrix; }
        }

        private static void Shuffle<T>(ref List<T> array)
        {
            Random rnd = new Random();
            T temp;
            for (int i = 0; i < array.Count - 1; i++)
            {
                int switchWith = rnd.Next(i + 1, array.Count);
                temp = array[i];
                array[i] = array[switchWith];
                array[switchWith] = temp;
            }
        }

        public Sudoku() : this(new Matrix<byte>(9, 9))
        {
        }

        public Sudoku(Sudoku source) : this(source.m_Matrix)
        {
        }

        public Sudoku(Matrix<byte> matrix)
        {
            m_Matrix = new Matrix<byte>(matrix);
        }

        public int Score
        {
            get
            {
                if (!m_ScoreCalculated)
                {
                    CalculateScore();
                }
                return m_Score;
            }
        }

        public byte this[int x, int y]
        {
            get { return m_Matrix[x, y]; }
            set
            {
                m_Matrix[x, y] = value;
                m_ScoreCalculated = false;
            }
        }

        private static bool Validate(byte[] buffer)
        {
            bool[] verification = new bool[9];
            for (var i = 0; i < 9; i++)
            {
                byte index = buffer[i];
                if (index == 0)
                    continue;

                if (verification[index - 1])
                    return false;
                verification[index - 1] = true;
            }
            return true;
        }

        private void CopyQuadrant(int x, int y, byte[] buffer)
        {
            int qx = (x / 3) * 3;
            int qy = (y / 3) * 3;
            int j = 0;
            for (x = qx; x < qx + 3; x++)
                for (y = qy; y < qy + 3; y++)
                {
                    buffer[j++] = m_Matrix[x, y];
                }
        }

        public bool InjectNumber(int x, int y)
        {
            m_ScoreCalculated = false;
            var numbers = new List<byte> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            byte[] buffer = new byte[9];
            if (this[x, y] == 0)
            {
                foreach (var number in numbers)
                {
                    m_Matrix[x, y] = number;
                    m_Matrix.GetRow(y, buffer);
                    if (!Validate(buffer))
                    {
                        continue;
                    }
                    m_Matrix.GetColumn(x, buffer);
                    if (!Validate(buffer))
                    {
                        continue;
                    }
                    CopyQuadrant(x, y, buffer);
                    if (!Validate(buffer))
                    {
                        continue;
                    }
                    return true;
                }
                m_ScoreCalculated = true;
                m_Score = int.MaxValue / 2;
                return false;
            }
            throw new InvalidOperationException();
        }

        private void CalculateScore()
        {
            // score consists of the sum of how wrong a number on each field is.
            // "Wrongness" is calculated by how many times the number on the field
            // appears in each line, column, square
            m_Score = 0;
            foreach (byte value in m_Matrix)
            {
                if (value == 0)
                {
                    m_Score++;
                }
            }
            m_ScoreCalculated = true;
        }

        public int CompareTo(Sudoku other)
        {
            if (other == null || other == this)
                throw new ArgumentException("Can't compare to null.");

            return (this.Score - other.Score);
        }
    }
}
