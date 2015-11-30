using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Barbar.Euler.Problem104
{
    public class DecimalInteger
    {
        private Collection<byte> m_Value;

        public int Count
        {
            get { return m_Value.Count; }
        }

        public IList<byte> Value
        {
            get { return new ReadOnlyCollection<byte>(m_Value); }
        }

        public DecimalInteger(DecimalInteger source)
        {
            byte[] buffer = new byte[source.m_Value.Count];
            source.m_Value.CopyTo(buffer, 0);
            m_Value = new Collection<byte>(buffer);
        }

        public DecimalInteger(int value)
        {
            m_Value = new Collection<byte>();
            while(value != 0)
            {
                m_Value.Add((byte)(value % 10));
                value /= 10;
            }
        }

        public void Add(DecimalInteger integer)
        {
            DecimalInteger big, small;
            if (m_Value.Count >= integer.m_Value.Count)
            {
                big = this;
                small = integer;
            }
            else
            {
                big = integer;
                small = this;
            }
            int carry = 0;
            for (var i = 0; i < big.m_Value.Count; i++)
            {
                int value = big.m_Value[i] + carry;
                if (i < small.m_Value.Count)
                {
                    value += small.m_Value[i];
                }
                carry = value / 10;

                if (i < m_Value.Count)
                {
                    m_Value[i] = (byte)(value - carry * 10);
                }
                else
                {
                    m_Value.Add((byte)(value - carry * 10));
                }
            }
            if (carry > 0)
            {
                m_Value.Add((byte)carry);
            }
        }

        public override string ToString()
        {
            char[] buffer = new char[m_Value.Count];
            for(var i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (char)((int)'0' + m_Value[buffer.Length - i - 1]);
            }
            return new string(buffer);
        }

        public DecimalInteger Clone()
        {
            return new DecimalInteger(this);
        }
    }
}
