using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSATutor
{
    class Utils
    {
        public static ulong PowMod(ulong baseVal, ulong powerVal, ulong modVal)
        {
            ulong answer = 1;

            for (ulong i = 0; i < powerVal; ++i)
            {
                answer *= baseVal;
                answer %= modVal;
            }

            return answer;
        }

        public static ulong[] Encrypt(byte[] input, ulong e, ulong n)
        {
            ulong[] output = new ulong[input.Length];

            for(int i = 0; i < input.Length; ++i)
            {
                output[i] = PowMod(input[i], e, n);
            }

            return output;
        }

        public static byte[] Decrypt(ulong[] input, ulong d, ulong n)
        {
            byte[] output = new byte[input.Length];

            for (int i = 0; i < input.Length; ++i)
            {
                output[i] = (byte)PowMod(input[i], d, n);
            }

            return output;
        }

        public static string ToString(byte[] input)
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
