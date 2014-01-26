using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Boombots.LevelEditor.Models
{
    public class GameBlock
    {
        public string Filename { get; set; }

        public string CharCode { get; set; }

        public Point CurrentCoord { get; set; }

        public GameBlock Clone()
        {
            return new GameBlock()
            {
                Filename = this.Filename,
                CharCode = this.CharCode,
                CurrentCoord = this.CurrentCoord,
            };
        }
    }
}
