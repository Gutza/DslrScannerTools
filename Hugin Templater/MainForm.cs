using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Hugin_Templater
{
    public partial class MainForm : Form
    {
        readonly Regex IMAGE_LINE_REGEX = new Regex(@"^i (.+)""(.+)""$");
        readonly Regex CONTROL_POINT_REGEX = new Regex(@"^c ");

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnTemplateFile_Click(object sender, EventArgs e)
        {
            if (ptoOpenFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            tbTemplateFile.Text = ptoOpenFileDialog.FileName;
        }

        private void btnTiffFolder_Click(object sender, EventArgs e)
        {
            if (tiffOpenFolderDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            tbTiffFolder.Text = tiffOpenFolderDialog.SelectedPath;
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            var output = string.Empty;
            var tiffEnumerator = Directory.EnumerateFiles(tbTiffFolder.Text, "*.tif").GetEnumerator();
            using (var fp = File.OpenText(tbTemplateFile.Text))
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
                        MessageBox.Show("Too few files!");
                        return;
                    }
                    output += line.Substring(0, imageMatch.Groups[2].Index) + Path.GetFileName(tiffEnumerator.Current) + @"""" + Environment.NewLine;
                }
            }

            if (tiffEnumerator.MoveNext())
            {
                MessageBox.Show("Too many files!");
                return;
            }

            ptoSaveFileDialog.FileName = Path.GetFullPath(tbTiffFolder.Text);
            if (ptoSaveFileDialog.ShowDialog()!=DialogResult.OK)
            {
                return;
            }

            File.WriteAllText(ptoSaveFileDialog.FileName, output);
        }
    }
}
