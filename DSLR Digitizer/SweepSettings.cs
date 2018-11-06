using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace DSLR_Digitizer
{
    public class SweepSettings
    {
        public Size DslrSize { get { return _dslrSize; } set { _cached = false; _dslrSize = value; } }
        public Size FilmSize { get { return _filmSize; } set { _cached = false; _filmSize = value; } }
        public string HuginTemplate;

        [JsonIgnore]
        public Size SweepCount { get { return GetSweepCount(); } }

        [JsonIgnore]
        public Size SweepDelta { get { return GetSweepDelta(); } }

        private bool _cached = false;
        private Size _dslrSize; // Given
        private Size _filmSize; // Given
        private Size _sweepCount; // Computed in ComputeCache()
        private Size _sweepDelta; // Computed in ComputeCache()

        private Size GetSweepCount()
        {
            if (_cached)
            {
                return _sweepCount;
            }

            ComputeCache();
            return _sweepCount;
        }

        private Size GetSweepDelta()
        {
            if (_cached)
            {
                return _sweepDelta;
            }

            ComputeCache();
            return _sweepDelta;
        }

        private void ComputeCache()
        {
            var actualFilmSize = FilmSize + DslrSize; // This is counter-intuitive, but it's correct

            _sweepCount = new Size()
            {
                Width = GetSweepCount(actualFilmSize.Width, _dslrSize.Width),
                Height = GetSweepCount(actualFilmSize.Height, _dslrSize.Height),
            };

            _sweepDelta = new Size()
            {
                Width = GetOptimalDelta(_filmSize.Width, _sweepCount.Width),
                Height = GetOptimalDelta(_filmSize.Height, _sweepCount.Height),
            };
        }

        private int GetSweepCount(int actualFilmSize, int dslrSize)
        {
            return (int)Math.Ceiling(((double)actualFilmSize) / dslrSize);
        }

        private int GetOptimalDelta(int rawFilmSize, int sweepCount)
        {
            if (sweepCount == 1)
            {
                return 0;
            }

            return (int)Math.Ceiling(rawFilmSize / ((double)sweepCount - 1));
        }
    }

    public class SweepSettingsCollection : Dictionary<string, SweepSettings>
    {
    }
}
