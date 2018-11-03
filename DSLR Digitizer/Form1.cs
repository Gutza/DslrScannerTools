using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSLR_Digitizer
{
    public partial class Form1 : Form
    {
        List<ScannerIcon> NavigationIcons;

        public Form1()
        {
            InitializeComponent();
            NavigationIcons = new List<ScannerIcon>()
            {
                iconLeft,
                iconRight,
                iconUp,
                iconDown,
                iconStop,
            };
            ResetNavigation(ScannerIcon.IconStates.Disabled);
        }

        private void ResetNavigation(ScannerIcon.IconStates iconState = ScannerIcon.IconStates.Active)
        {
            foreach(var icon in NavigationIcons)
            {
                icon.IconState = iconState;
            }
        }
    }
}
