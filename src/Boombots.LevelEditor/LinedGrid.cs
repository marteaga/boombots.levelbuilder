using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Boombots.LevelEditor
{
    public class LinedGrid : Grid
    {
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            List<RowDefinition> rowDefinitions = this.RowDefinitions.ToList();
            List<ColumnDefinition> columnDefinitions = this.ColumnDefinitions.ToList();

            Pen pen = new Pen(Brushes.Black, 1.0);

            foreach (ColumnDefinition columnDefinition in columnDefinitions)
                dc.DrawLine(pen, new Point(columnDefinition.Offset, 0), new Point(columnDefinition.Offset, this.ActualHeight));
            foreach (RowDefinition rowDefinition in rowDefinitions)
                dc.DrawLine(pen, new Point(0, rowDefinition.Offset), new Point(this.ActualWidth, rowDefinition.Offset));
        }
    }
}
