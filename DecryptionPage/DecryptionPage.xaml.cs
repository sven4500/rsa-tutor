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
        private ulong D
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

                    InputTextBox.Text = System.Text.Encoding.Default.GetString(encryptedBytes);
                    InputBinTextBox.Text = Utils.ToString(encryptedBytes);
                }

                OutputTextBox.Text = "";
                OutputBinTextBox.Text = "";
            }
        }

        private Dictionary<string, PrivateKey> privateKeys = new Dictionary<string, PrivateKey>();
        public Dictionary<string, PrivateKey> PrivateKeys
        {
            set
            {
                if (value != null)
                {
                    privateKeys = value;
                }
            }

            get
            {
                return privateKeys;
            }
        }

        private Dictionary<string, List<ulong[]>> encryptedMessages = new Dictionary<string, List<ulong[]>>();
        public Dictionary<string, List<ulong[]>> EncryptedMessages
        {
            set
            {
                List<string> emails = new List<string>();

                if (value != null)
                {
                    encryptedMessages = value;
                }

                foreach (KeyValuePair<string, List<ulong[]>> pair in encryptedMessages)
                {
                    // Добавляем в список только тех пользователей от которых
                    // были получены сообщения.
                    if (pair.Value != null && pair.Value.Count > 0)
                    {
                        emails.Add(pair.Key);
                    }
                }

                EmailsCbs.ItemsSource = emails;
                EmailsCbs.SelectedIndex = 0;
            }

            get
            {
                return encryptedMessages;
            }
        }

        private string email = "";
        private string Email
        {
            set
            {
                List<string> messageTitles = new List<string>();

                email = value;

                if (privateKeys.ContainsKey(value))
                {
                    D = privateKeys[value].d;
                    N = privateKeys[value].n;
                }
                else
                {
                    D = 0;
                    N = 0;
                }

                if (EncryptedMessages.ContainsKey(value))
                {
                    for (var i = 0; i < EncryptedMessages[value].Count; ++i)
                    {
                        messageTitles.Add(string.Format("Сообщение {0}", i + 1));
                    }
                }

                MessageCbs.ItemsSource = null;

                if (messageTitles.Count > 0)
                {
                    MessageCbs.ItemsSource = messageTitles;
                    MessageCbs.SelectedIndex = 0;
                }
            }

            get
            {
                return email;
            }
        }

        public DecryptionPage()
        {
            InitializeComponent();
        }
        
        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (EncryptedMessage != null)
            {
                byte[] decryptedBytes = Utils.Decrypt(EncryptedMessage, D, N);

                OutputTextBox.Text = System.Text.Encoding.Default.GetString(decryptedBytes);
                OutputBinTextBox.Text = Utils.ToString(decryptedBytes);
            }
        }
        
        private void EmailsCbs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> emails = EmailsCbs.ItemsSource as List<string>;

            if (EmailsCbs.SelectedIndex >= 0)
            {
                Email = emails[EmailsCbs.SelectedIndex];
            }
        }

        private void MessageCbs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EncryptedMessages.ContainsKey(Email) && MessageCbs.SelectedIndex >= 0)
            {
                EncryptedMessage = EncryptedMessages[Email][MessageCbs.SelectedIndex];
            }
        }
    }
}
