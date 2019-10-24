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

namespace Randewoo.TestTask.DesktopClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void M_FilterSelector_OnSelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            if ( m_ProductTable != null ) {
                CollectionViewSource.GetDefaultView( m_ProductTable.ItemsSource ).Refresh();
            }
        }
    }
}
