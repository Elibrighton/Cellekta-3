using Cellekta_3.ViewModel;
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
using System.Windows.Shapes;

namespace Cellekta_3.View
{
    /// <summary>
    /// Interaction logic for SongListView.xaml
    /// </summary>
    public partial class SongListView : Window
    {
        public SongListView(ISongListViewModel songListViewModel)
        {
            InitializeComponent();
            DataContext = songListViewModel;
        }
    }
}
