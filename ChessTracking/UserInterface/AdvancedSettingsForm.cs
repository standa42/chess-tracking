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
using MaterialSkin;
using MaterialSkin.Controls;

namespace ChessTracking.UserInterface
{
    partial class AdvancedSettingsForm : MaterialForm
    {
        private MainGameForm MainForm { get; }
        private UserDefinedParametersPrototypeFactory UserParameters { get; }

        public AdvancedSettingsForm(MainGameForm mainForm, UserDefinedParametersPrototypeFactory userParameters)
        {
            MainForm = mainForm;
            UserParameters = userParameters;
            InitializeComponent();
            
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            
        }

        private void AdvancedSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm?.AdvancedFormClosing();
        }

        private void MilimetersClippedTrackBar_ValueChanged(object sender, EventArgs e)
        {
            MilimetersClippedValueLabel.Text = MilimetersClippedTrackBar.Value.ToString();
            UserParameters.ChangePrototype(x => x.MilimetersClippedFromFigure = MilimetersClippedTrackBar.Value);
        }

        private void PointsIndicatingFigureTrackBar_ValueChanged(object sender, EventArgs e)
        {
            PointsIndicatingFigureValueLabel.Text = PointsIndicatingFigureTrackBar.Value.ToString();
            UserParameters.ChangePrototype(x => x.NumberOfPointsIndicatingFigure = PointsIndicatingFigureTrackBar.Value);
        }

        private void MilisecondsTasksTrackBar_ValueChanged(object sender, EventArgs e)
        {
            MilisecondsTasksValueLabel.Text = MilisecondsTasksTrackBar.Value.ToString();
            UserParameters.ChangePrototype(x => x.MinimalTimeBetweenTrackingTasksInMiliseconds = MilisecondsTasksTrackBar.Value);
        }
    }
}
