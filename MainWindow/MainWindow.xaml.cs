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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IntroductionPage introdutionPage = new IntroductionPage();
        private KeyGeneratorPage keyGeneratorPage = new KeyGeneratorPage();
        private EncryptionPage encryptionPage = new EncryptionPage();
        private DecryptionPage decryptionPage = new DecryptionPage();

        private int pageNumber = 0;

        public MainWindow()
        {
            InitializeComponent();
            setPage(0);
        }

        void setPage(int pageNumber)
        {
            if (pageNumber < 0 || pageNumber >= 4)
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
                    encryptionPage.E = keyGeneratorPage.E;
                    encryptionPage.N = keyGeneratorPage.N;
                    break;
                case 3:
                    page = decryptionPage;
                    decryptionPage.EncryptedMessage = encryptionPage.EncryptedMessage;
                    decryptionPage.D = keyGeneratorPage.D;
                    decryptionPage.N = keyGeneratorPage.N;
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
            
            updateExpanderWidth();

            this.pageNumber = pageNumber;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            setPage(pageNumber + 1);
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            setPage(pageNumber - 1);
        }

        private void updateExpanderWidth()
        {
            if (pageNumber == 0)
            {
                PageDescriptionExpander.MaxWidth = MainGrid.ActualWidth;
            }
            else
            {
                PageDescriptionExpander.MaxWidth = MainGrid.ActualWidth / 3;
            }
        }

        private void PageDescriptionExpander_Expanded(object sender, RoutedEventArgs e)
        {
            updateExpanderWidth();
        }
    }
}
