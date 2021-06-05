using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSATutor
{
    class Utils
    {
        public static double[] encrypt(byte[] input, ulong e, ulong n)
        {
            double[] output = new double[input.Length];

            for(int i = 0; i < input.Length; ++i)
            {
                output[i] = Math.Pow(input[i], e) % n;
            }

            return output;
        }
    }
}
