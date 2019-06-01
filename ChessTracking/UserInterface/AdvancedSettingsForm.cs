using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessTracking.ImageProcessing.PipelineData;

namespace ChessTracking.UserInterface
{
    partial class AdvancedSettingsForm : Form
    {
        private MainGameForm MainForm { get; }
        private UserDefinedParametersPrototypeFactory UserParameters { get; }

        public AdvancedSettingsForm(MainGameForm mainForm, UserDefinedParametersPrototypeFactory userParameters)
        {
            MainForm = mainForm;
            UserParameters = userParameters;
            InitializeComponent();
        }

        private void AdvancedSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm?.AdvancedFormClosing();
        }
    }
}
