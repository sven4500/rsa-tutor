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

        private ulong[] encryptedMessage = null;
        public ulong[] EncryptedMessage
        {
            get
            {
                return encryptedMessage;
            }

            set
            {
                encryptedMessage = value;

                if (encryptedMessage != null)
                {
                    byte[] encryptedBytes = new byte[encryptedMessage.Length * sizeof(ulong)];
                    Buffer.BlockCopy(encryptedMessage, 0, encryptedBytes, 0, encryptedBytes.Length);

                    OutputTextBox.Text = System.Text.Encoding.Default.GetString(encryptedBytes);
                    OutputBinTextBox.Text = Utils.ToString(encryptedBytes);
                }
            }
        }
        
        public EncryptionPage()
        {
            InitializeComponent();
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            byte[] inputBytes = Encoding.Default.GetBytes(InputTextBox.Text);
            EncryptedMessage = Utils.Encrypt(inputBytes, E, N);

            InputBinTextBox.Text = Utils.ToString(inputBytes);
        }
    }
}
