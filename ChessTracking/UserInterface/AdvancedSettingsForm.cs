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
using ChessTracking.Localization;
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
            
            PrepareComponentsValues();
        }
        
        private void AdvancedSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm?.AdvancedFormClosing();
        }

        private void PrepareComponentsValues()
        {
            var parameters = UserParameters.GetShallowCopy();

            MilimetersClippedValueLabel.Text = parameters.MilimetersClippedFromFigure.ToString();
            MilimetersClippedTrackBar.Value = parameters.MilimetersClippedFromFigure;

            PointsIndicatingFigureValueLabel.Text = parameters.NumberOfPointsIndicatingFigure.ToString();
            PointsIndicatingFigureTrackBar.Value = parameters.NumberOfPointsIndicatingFigure;

            MilisecondsTasksValueLabel.Text = parameters.MinimalTimeBetweenTrackingTasksInMiliseconds.ToString();
            MilisecondsTasksTrackBar.Value = parameters.MinimalTimeBetweenTrackingTasksInMiliseconds;

            BinarizationThresholdValueLabel.Text = parameters.BinarizationThreshold.ToString();
            BinarizationThresholdTrackbar.Value = parameters.BinarizationThreshold;

            OtzuToggleButton.Text = parameters.OtzuActiveInBinarization ? ProgramLocalization.DisableOtzu : ProgramLocalization.EnableOtzu;

            FiguresColorMetricButton.Text = parameters.IsFiguresColorMetricExperimental 
                ? ProgramLocalization.SetDefaultMetric
                : ProgramLocalization.SetQuadraticMetric;

            DistanceMetricFittingChessboardButton.Text = parameters.IsDistanceMetricInChessboardFittingExperimental
                ? ProgramLocalization.SetDefaultMetric
                : ProgramLocalization.SetQuadraticMetric;
            DistanceMetricFittingChessboardTrackBar.Value = parameters.ClippedDistanecInChessboardFittingMetric;
            DistanceMetricFittingChessboardButtonValueLabel.Text = parameters.ClippedDistanecInChessboardFittingMetric.ToString();

            InfluenceColorTrackbar.Value = parameters.GameStateInfluenceOnColor;

            InfluencePresenceTrackBar.Value = parameters.GameStateInfluenceOnPresence;
            InfluencePresenceTrackBar.Maximum = parameters.NumberOfPointsIndicatingFigure - 1;
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
            
            if (InfluencePresenceTrackBar.Value >= PointsIndicatingFigureTrackBar.Value)
                InfluencePresenceTrackBar.Value = PointsIndicatingFigureTrackBar.Value - 1;
            InfluencePresenceTrackBar.Maximum = PointsIndicatingFigureTrackBar.Value - 1;
            UserParameters.ChangePrototype(x => x.GameStateInfluenceOnPresence = InfluencePresenceTrackBar.Value);
        }

        private void MilisecondsTasksTrackBar_ValueChanged(object sender, EventArgs e)
        {
            MilisecondsTasksValueLabel.Text = MilisecondsTasksTrackBar.Value.ToString();
            UserParameters.ChangePrototype(x => x.MinimalTimeBetweenTrackingTasksInMiliseconds = MilisecondsTasksTrackBar.Value);
        }

        private void OtzuToggleButton_Click(object sender, EventArgs e)
        {
            if (OtzuToggleButton.Text == ProgramLocalization.DisableOtzu)
            {
                OtzuToggleButton.Text = ProgramLocalization.EnableOtzu;
                UserParameters.ChangePrototype(x => x.OtzuActiveInBinarization = false);
            }
            else
            {
                OtzuToggleButton.Text = ProgramLocalization.DisableOtzu;
                UserParameters.ChangePrototype(x => x.OtzuActiveInBinarization = true);
            }
        }

        private void BinarizationThresholdTrackbar_ValueChanged(object sender, EventArgs e)
        {
            BinarizationThresholdValueLabel.Text = BinarizationThresholdTrackbar.Value.ToString();
            UserParameters.ChangePrototype(x => x.BinarizationThreshold = BinarizationThresholdTrackbar.Value);
        }

        private void FiguresColorMetricButton_Click(object sender, EventArgs e)
        {
            if (FiguresColorMetricButton.Text == ProgramLocalization.SetDefaultMetric)
            {
                FiguresColorMetricButton.Text = ProgramLocalization.SetQuadraticMetric;
                UserParameters.ChangePrototype(x => x.IsFiguresColorMetricExperimental = false);
            }
            else
            {
                FiguresColorMetricButton.Text = ProgramLocalization.SetDefaultMetric;
                UserParameters.ChangePrototype(x => x.IsFiguresColorMetricExperimental = true);
            }
        }

        private void DistanceMetricFittingChessboardButton_Click(object sender, EventArgs e)
        {
            if (DistanceMetricFittingChessboardButton.Text == ProgramLocalization.SetDefaultMetric)
            {
                DistanceMetricFittingChessboardButton.Text = ProgramLocalization.SetQuadraticMetric;
                UserParameters.ChangePrototype(x => x.IsDistanceMetricInChessboardFittingExperimental = false);
            }
            else
            {
                DistanceMetricFittingChessboardButton.Text = ProgramLocalization.SetDefaultMetric;
                UserParameters.ChangePrototype(x => x.IsDistanceMetricInChessboardFittingExperimental = true);
            }
        }

        private void DistanceMetricFittingChessboardTrackBar_ValueChanged(object sender, EventArgs e)
        {
            DistanceMetricFittingChessboardButtonValueLabel.Text = DistanceMetricFittingChessboardTrackBar.Value.ToString();
            UserParameters.ChangePrototype(x => x.ClippedDistanecInChessboardFittingMetric = DistanceMetricFittingChessboardTrackBar.Value);
        }

        private void InfluenceColorTrackbar_ValueChanged(object sender, EventArgs e)
        {
            UserParameters.ChangePrototype(x => x.GameStateInfluenceOnColor = InfluenceColorTrackbar.Value);
        }

        private void InfluencePresenceTrackBar_ValueChanged(object sender, EventArgs e)
        {
            UserParameters.ChangePrototype(x => x.GameStateInfluenceOnPresence = InfluencePresenceTrackBar.Value);
        }
    }
}
