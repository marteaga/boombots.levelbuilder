using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boombots.LevelEditor.Models
{
    public class GameBlock
    {
        public string Filename { get; set; }

        public string CharCode { get; set; }

        public System.Windows.Point CurrentCoord { get; set; }
    }
}
