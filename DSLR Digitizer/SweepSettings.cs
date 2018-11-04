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
            Point result = new Point();

            result.X = (int)Math.Ceiling((double)SweepSize.X / DslrSize.X);
            if (result.X > 1)
            {
                result.X++;
            }

            result.Y = (int)Math.Ceiling((double)SweepSize.Y / DslrSize.Y);
            if (result.Y > 1)
            {
                result.Y++;
            }
            return result;
        }
    }

    public class SweepSettingsCollection : Dictionary<string, SweepSettings>
    {
    }
}
