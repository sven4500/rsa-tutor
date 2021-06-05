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
    public partial class DecryptionPage : Page
    {
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

                    InputTextBox.Text = System.Text.Encoding.Default.GetString(encryptedBytes);
                    InputBinTextBox.Text = Utils.ToString(encryptedBytes);
                }
            }
        }

        public DecryptionPage()
        {
            InitializeComponent();
        }
    }
}
