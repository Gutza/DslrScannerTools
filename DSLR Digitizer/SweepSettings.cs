using System;
using System.Collections.Generic;
using System.Drawing;

namespace DSLR_Digitizer
{
    public class SweepSettings
    {
        public Point DslrSize;
        public Point SweepSize;
        public string HuginTemplate;

        public Point GetSweepSize()
        {
            return new Point()
            {
                X=ComputeSize(DslrSize.X, SweepSize.X),
                Y=ComputeSize(DslrSize.Y, SweepSize.Y),
            };
        }

        /*
         * We subtract a unit because we want to make sure floor(3/1) == 2,
         * which sounds messed up, but it's the only way I could find to include
         * the last segment.
         */
        private int ComputeSize(int dslrSize, int sweepSize)
        {
            return (int)Math.Floor(((double)sweepSize - 1) / dslrSize) + 1;
        }
    }

    public class SweepSettingsCollection : Dictionary<string, SweepSettings>
    {
    }
}
