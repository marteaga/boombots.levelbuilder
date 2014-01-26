using Boombots.LevelEditor.Models;
using Microsoft.Expression.Interactivity.Layout;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interactivity;
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

            // create the block list
            SetupBlockList();

            // wire up the save button
            btnSave.Click += btnSave_Click;

            // build the grid
            BuildGrid();
        }

        void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var lines = new List<string>();
            var sb = new StringBuilder();
            // loop all the rows and columns
            for (int x = 0; x < gameGrid.RowDefinitions.Count; x++)
            {
                for (int y = 0; y < gameGrid.ColumnDefinitions.Count; y++)
                {

                    var item = gameGrid.Children
                                              .Cast<Image>()
                                              .Where(i => (i.DataContext as GameBlock).CurrentCoord.Equals(new Point(x, y)))
                                              .FirstOrDefault();

                    if (item == null)
                        sb.Append('.');
                    else
                        sb.Append((item.DataContext as GameBlock).CharCode);
                }
                // add a line fo rthe next row
                lines.Add(sb.ToString());
                sb.Clear();
            }

            // save to a file
            var dlg = new SaveFileDialog();
            dlg.FileName = "level"; // Default file name
            dlg.DefaultExt = ".text"; // Default file extension
            dlg.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
                System.IO.File.WriteAllLines(dlg.FileName, lines.ToArray());

        }

        private void SetupBlockList()
        {
            CreateBlocksDataSource();
            lstBlocks.ItemsSource = _blocks;
        }
                
        private void BuildGrid()
        {
            // board game is 30x16
            for(int x = 0;x<30; x++)
                gameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            for (int x = 0; x < 16; x++)
                gameGrid.RowDefinitions.Add(new RowDefinition());

            gameGrid.MouseLeftButtonUp += (s, args) =>
            {
                var p = args.GetPosition(gameGrid);
                // create an iamge element and add it to the the grid
                var block = lstBlocks.SelectedItem as GameBlock;
                if (block != null)
                {
                    var img = new Image();
                    var uriSource = new Uri(block.Filename, UriKind.Relative);
                    img.Source = new BitmapImage(uriSource);
                    img.DataContext = block.Clone();
                    img.MouseLeftButtonUp += img_MouseLeftButtonUp;
                    gameGrid.Children.Add(img);
                    this.SnapToGrid(img, p);
                }
            };
        }

        void img_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var img = sender as Image;
            gameGrid.Children.Remove(img);
            e.Handled = true;
        }

        private void SnapToGrid(Image element, Point point)
        {

            var column = -1;
            double total = 0;
            foreach (ColumnDefinition clm in gameGrid.ColumnDefinitions)
            {
                if (point.X < total)
                {
                    break;
                }
                column++;
                total += clm.ActualWidth;
            }

            var row = -1;
            total = 0;
            foreach (RowDefinition rowDef in gameGrid.RowDefinitions)
            {
                if (point.Y < total)
                {
                    break;
                }
                row++;
                total += rowDef.ActualHeight;
            }

            Grid.SetRow(element, row);
            Grid.SetColumn(element, column);

            // set the data context
            (element.DataContext as GameBlock).CurrentCoord = new Point(row, column);
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
