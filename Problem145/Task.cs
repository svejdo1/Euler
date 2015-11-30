using System;

namespace Barbar.Euler.Problem145
{
    public class Task : ITask
    {
        public string Run()
        {
            byte[] buffer = new byte[10];
            int index = 0;
            int maxIndex = 0;
            long count = 0;
            while(true)
            {
                if (buffer[index] == 9)
                {
                    while(buffer[index] == 9)
                    {
                        buffer[index] = 0;
                        index++;
                        maxIndex = Math.Max(index, maxIndex);
                    }
                    buffer[index]++;
                    index = 0;
                }
                else
                {
                    buffer[index]++;
                }
                if (buffer[0] != 0)
                {
                    int carry = 0;
                    bool fail = false;
                    for(var j = 0; j <= maxIndex; j++)
                    {
                        int value = buffer[j] + buffer[maxIndex - j] + carry;
                        carry = value / 10;
                        value -= carry * 10;
                        if ((value & 1) == 0)
                        {
                            fail = true;
                            break;
                        }
                    }
                    if (!fail && carry > 0)
                    {
                        fail = ((carry & 1) == 0);
                    }
                    if (!fail)
                        count++;
                }

                if (buffer[9] == 1)
                    break;
            }

            return Convert.ToString(count);
        }
    }
}
