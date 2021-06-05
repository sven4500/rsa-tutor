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
        private Page[] pages = { new IntroductionPage(), new KeyGeneratorPage(), new EncryptionPage(), new DecryptionPage() };
        private int pageNumber = 0;

        public MainWindow()
        {
            InitializeComponent();
            setPage(0);
        }

        void setPage(int pageNumber)
        {
            if (pageNumber < 0 || pageNumber >= pages.Length)
                return;

            Page page = pages[pageNumber];
            Page prevPage = (pageNumber > 0) ? pages[pageNumber - 1] : null;

            PageTitle.Text = page.Title;
            PageDescription.Text = page.Tag as string;
            
            ContentsFrame.Navigate(page);
            
            PageDescriptionExpander.IsExpanded = (pageNumber == 0);
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
