using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ACHCodeGenerator
{
    public partial class AnalyzerProgressBar : Form
    {
        public int ProgressValue
        {
            get
            {
                return pbarProgress.Value;
            }
            set
            {
                pbarProgress.Value = value;
            }
        }
        
        public AnalyzerProgressBar(int MaxValue)
        {
            InitializeComponent();
            pbarProgress.Maximum = MaxValue;
        }
    }
}
