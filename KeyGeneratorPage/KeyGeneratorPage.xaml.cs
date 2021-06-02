using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RSATutor
{
    public partial class KeyGeneratorPage : Page
    {
        private ulong[] primeNumbers = {
            2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71,
            73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173,
            179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281,
            283, 293, 307, 311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389, 397, 401, 409,
            419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499, 503, 509, 521, 523, 541,
            547, 557, 563, 569, 571, 577, 587, 593, 599, 601, 607, 613, 617, 619, 631, 641, 643, 647, 653, 659,
            661, 673, 677, 683, 691, 701, 709, 719, 727, 733, 739, 743, 751, 757, 761, 769, 773, 787, 797, 809,
            811, 821, 823, 827, 829, 839, 853, 857, 859, 863, 877, 881, 883, 887, 907, 911, 919, 929, 937, 941,
            947, 953, 967, 971, 977, 983, 991, 997 };

        private Random rand = new Random();

        private ulong n = 0;
        public ulong N
        {
            get
            {
                return n;
            }

            set
            {
                n = value;
                PubNTextBox.Text = value.ToString();
                PriNTextBox.Text = value.ToString();
            }
        }
        
        public ulong P
        {
            get
            {
                return Convert.ToUInt64(PTextBox.Text);
            }

            set
            {
                PTextBox.Text = value.ToString();
            }
        }
        
        public ulong Q
        {
            get
            {
                return Convert.ToUInt64(QTextBox.Text);
            }

            set
            {
                QTextBox.Text = value.ToString();
            }
        }

        public ulong E
        {
            get
            {
                return Convert.ToUInt64(ETextBox.Text);
            }

            set
            {
                ETextBox.Text = value.ToString();
            }
        }

        public ulong D
        {
            get
            {
                return Convert.ToUInt64(DTextBox.Text);
            }

            set
            {
                DTextBox.Text = value.ToString();
            }
        }

        public ulong Totient
        {
            get
            {
                return (Q - 1) * (P - 1);
            }
        }

        public KeyGeneratorPage()
        {
            InitializeComponent();
        }

        //https://stackoverflow.com/questions/18541832/c-sharp-find-the-greatest-common-divisor
        public static ulong GCD(ulong a, ulong b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                {
                    a %= b;
                }
                else
                {
                    b %= a;
                }
            }

            return a | b;
        }

        //https://hackernoon.com/generating-rsa-private-and-public-keys-b82a06db6d1c
        public static ulong modInverse(ulong e, ulong tot)
        {
            e = e % tot;

            for (ulong x = 1; x < tot; ++x)
            {
                if ((e * x) % tot == 1)
                {
                    return x;
                }
            }

            return 1;
        }

        private ulong randomPrime()
        {
            return primeNumbers[rand.Next(primeNumbers.Length)];
        }

        private void PRandButton_Click(object sender, RoutedEventArgs e)
        {
            P = randomPrime();
        }

        private void QRandButton_Click(object sender, RoutedEventArgs e)
        {
            Q = randomPrime();
        }
        
        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            // 1. Находим значение n.
            N = P * Q;

            // 2. Находим значение e такое что оно взаимопростое с n.
            var ee = from prime in primeNumbers
                     where prime < Totient && GCD(prime, Totient) == 1
                     select prime;
            
            E = ee.ElementAt(rand.Next(ee.Count()));

            // 3. Находим d.
            D = modInverse(E, Totient);

            // Проверка.
            Debug.Assert(GCD(E, Totient) == 1);
            Debug.Assert((E * D) % Totient == 1);
        }
    }
}
