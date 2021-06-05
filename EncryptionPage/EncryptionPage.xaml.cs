using System;
using System.Collections.Generic;
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
    public partial class EncryptionPage : Page
    {
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

        public ulong N
        {
            get
            {
                return Convert.ToUInt64(NTextBox.Text);
            }

            set
            {
                NTextBox.Text = value.ToString();
            }
        }

        public byte[] EncryptedData { get; set; }
        
        public EncryptionPage()
        {
            InitializeComponent();
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            byte[] inputBin = Encoding.ASCII.GetBytes(InputTextBox.Text);
            double[] encrypted = Utils.encrypt(inputBin, E, N);

            EncryptedData = new byte[encrypted.Length * sizeof(double)];
            Buffer.BlockCopy(encrypted, 0, EncryptedData, 0, EncryptedData.Length);

            InputBinTextBox.Text = Utils.toString(inputBin);
            
            OutputTextBox.Text = System.Text.Encoding.Default.GetString(EncryptedData);
            OutputBinTextBox.Text = Utils.toString(EncryptedData);
        }
    }
}
