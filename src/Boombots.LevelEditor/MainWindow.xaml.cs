using Boombots.LevelEditor.Models;
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

namespace Boombots.LevelEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<GameBlock> _blocks;

        public MainWindow()
        {
            InitializeComponent();

            CreateBlocksDataSource();
            lstBlocks.ItemsSource = _blocks;

            // wire up the save button
            btnSave.Click += btnSave_Click;

            // build the grid
            BuildGrid();
        }

        void btnSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("TODO");
        }

        private void BuildGrid()
        {
            // board game is 30x16
            for(int x = 0;x<30; x++)
                gameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            for (int x = 0; x < 16; x++)
                gameGrid.RowDefinitions.Add(new RowDefinition());
        }

        private void CreateBlocksDataSource()
        {
            _blocks = new List<GameBlock>()
            {
                new GameBlock() { Filename = "Images\\blocks\\blue.png",      CharCode = "1", CurrentCoord = new Point(0,0) },
                new GameBlock() { Filename = "Images\\blocks\\green.png",     CharCode = "2", CurrentCoord = new Point(0,0) },
                new GameBlock() { Filename = "Images\\blocks\\red.png",       CharCode = "3", CurrentCoord = new Point(0,0) },
                new GameBlock() { Filename = "Images\\blocks\\yellow.png",    CharCode = "4", CurrentCoord = new Point(0,0) },
            };
        }
    }
}
