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
        private ulong E
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

        private ulong N
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
        private ulong[] EncryptedMessage
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

        private Dictionary<string, PublicKey> publicKeys = new Dictionary<string, PublicKey>();
        public Dictionary<string, PublicKey> PublicKeys
        {
            get
            {
                return publicKeys;
            }

            set
            {
                List<string> emails = new List<string>();

                if (value != null)
                {
                    publicKeys = value;
                }
                
                foreach(KeyValuePair<string, PublicKey> pair in publicKeys)
                {
                    emails.Add(pair.Key);
                }

                EmailsCbs.ItemsSource = emails;
                EmailsCbs.SelectedIndex = 0;
            }
        }

        private Dictionary<string, List<ulong[]>> encryptedMessages = new Dictionary<string, List<ulong[]>>();
        public Dictionary<string, List<ulong[]>> EncryptedMessages
        {
            get
            {
                return encryptedMessages;
            }
        }
        
        private string Email
        {
            get
            {
                return EmailsCbs.Text;
            }

            set
            {
                if (publicKeys.ContainsKey(value))
                {
                    E = publicKeys[value].e;
                    N = publicKeys[value].n;
                }
                else
                {
                    E = 0;
                    N = 0;
                }

                // Очистить зашифрованное сообщение при смене пользователя.
                EncryptedMessage = new ulong[0];
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

        private void EmailsCbs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> emails = EmailsCbs.ItemsSource as List<string>;

            if (EmailsCbs.SelectedIndex >= 0)
            {
                Email = emails[EmailsCbs.SelectedIndex];
            }
        }
        
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (Email.Length > 0)
            {
                if (EncryptedMessage.Length > 0)
                {
                    if (!EncryptedMessages.ContainsKey(Email))
                    {
                        EncryptedMessages[Email] = new List<ulong[]>();
                    }

                    EncryptedMessages[Email].Add(EncryptedMessage);
                }
            }
        }
    }
}
