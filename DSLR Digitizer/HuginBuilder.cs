using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Hugin_Templater
{
    public class HuginUtils
    {
        private const string TIFF_PATTERN = @"*.tif";
        private const string PTO_EXTENSTION_SUFFIX = @".pto";
        readonly Regex IMAGE_LINE_REGEX = new Regex(@"^i (.+)""(.+)""$");
        readonly Regex CONTROL_POINT_REGEX = new Regex(@"^c ");

        public bool BuildRawMosaicFromTemplate(string templatePtoFilename, string baseFolder, string outputPtoBasename)
        {
            var output = string.Empty;
            var tiffEnumerator = Directory.EnumerateFiles(baseFolder, TIFF_PATTERN).GetEnumerator();

            using (var fp = File.OpenText(templatePtoFilename))
            {
                while (!fp.EndOfStream)
                {
                    string line = fp.ReadLine();
                    if (CONTROL_POINT_REGEX.IsMatch(line))
                    {
                        // Ignore control points in the template
                        continue;
                    }

                    var imageMatch = IMAGE_LINE_REGEX.Match(line);
                    if (!imageMatch.Success)
                    {
                        output += line + Environment.NewLine;
                        continue;
                    }

                    if (!tiffEnumerator.MoveNext())
                    {
                        throw new InvalidOperationException("Too few files in folder " + baseFolder + "!");
                    }

                    output += line.Substring(0, imageMatch.Groups[2].Index) + Path.GetFileName(tiffEnumerator.Current) + @"""" + Environment.NewLine;
                }
            }

            if (tiffEnumerator.MoveNext())
            {
                throw new InvalidOperationException("Too many TIFF files in folder " + baseFolder + "!");
            }

            File.WriteAllText(Path.Combine(baseFolder, outputPtoBasename + PTO_EXTENSTION_SUFFIX), output);
            return true;
        }
    }
}
