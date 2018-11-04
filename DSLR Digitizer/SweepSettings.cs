using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLR_Digitizer
{
    [Serializable]
    public class SweepSettings
    {
        public string Name;
        public Point DslrSize;
        public Point SweepSize;
    }
}
