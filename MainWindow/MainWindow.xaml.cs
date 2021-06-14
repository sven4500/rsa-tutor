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
using RSATutor;

namespace RSATutor
{
    public partial class MainWindow : Window
    {
        private IntroductionPage introdutionPage = new IntroductionPage();
        private KeyGeneratorPage keyGeneratorPage = new KeyGeneratorPage();
        private EncryptionPage encryptionPage = new EncryptionPage();
        private DecryptionPage decryptionPage = new DecryptionPage();

        private int pageNumber = 0;
        private int maxPageNumber = 3;

        public MainWindow()
        {
            InitializeComponent();
            setPage(0);
        }

        void setPage(int pageNumber)
        {
            if (pageNumber < 0 || pageNumber > maxPageNumber)
                return;

            Page page = null;

            switch (pageNumber)
            {
                case 0:
                    page = introdutionPage;
                    break;
                case 1:
                    page = keyGeneratorPage;
                    break;
                case 2:
                    page = encryptionPage;
                    encryptionPage.PublicKeys = keyGeneratorPage.PublicKeys;
                    break;
                case 3:
                    page = decryptionPage;
                    decryptionPage.EncryptedMessage = encryptionPage.EncryptedMessage;
                    break;
                default:
                    page = null;
                    break;
            }

            if (page != null)
            {
                PageTitle.Text = page.Title;
                PageDescription.Text = page.Tag as string;

                ContentsFrame.Navigate(page);
            }
            
            this.pageNumber = pageNumber;

            ToolsTab.IsEnabled = (pageNumber != 0);
            PrevButton.IsEnabled = (pageNumber != 0);
            NextButton.IsEnabled = (pageNumber != maxPageNumber);
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            setPage(pageNumber + 1);
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            setPage(pageNumber - 1);
        }
    }
}
