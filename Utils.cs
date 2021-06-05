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

        public static string toString(byte[] input)
        {
            string str = "";

            foreach (byte b in input)
            {
                str += string.Format("0x{0:X2} ", b);
            }

            return str;
        }
    }
}
