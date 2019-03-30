using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Kinect;

using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Kinect_v0._1
{
    public partial class Form1 : Form
    {
        KinectHandler KinHandler = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            KinHandler = new KinectHandler(this);
            InitChangableItems();
        }

        private void InitChangableItems()
        {
            List<UIChangableItem> items = new List<UIChangableItem>
            {
                new UIChangableItem("Color change on distance", 5, 15, 7, ColorChange),
                new UIChangableItem("FramesToAverage", 1, 15, Config.NumberOfFramesToAverage, FramesToAverage),
                new UIChangableItem("Ransac plane thickness [mm]", 1, 15, (int)(Config.RansacThickness * 1000), RANSACPlaneThickness),
                new UIChangableItem("Regression plane thickness [mm]", 1, 15, (int)(Config.RegreseTloustka * 1000), Regression),
                new UIChangableItem("Number of paralel calculations (logical cores: " + Environment.ProcessorCount + " )",1,100,Config.paralelTasksCount, Paralelization),
                new UIChangableItem("Minimal Depth [m]", 50, 800, (int)(Config.minDepth * 100), MinDepth),
                new UIChangableItem("Minimal Depth [m]", 50, 800, (int)(Config.maxDepth * 100), MaxDepth),
                new UIChangableItem("Tracking algorithm is performed eveny n-th frame, n: ", 1, 30, Config.TrackingIsPerformedEveryXthFrame,AlgorithmPerformedEveryNthFrame ),
                new UIChangableItem("Regression is evaluated on every n-th table pixel, n:",1,30,Config.RegressionIsEvaluatedOnEveryXthTablePixel,RegressionOnEveryNthTablePixel),
                new UIChangableItem("Size of grid for testing plane fit in RANSAC",1,20,Config.GridSizeForTestingPlaneFitInRANSAC, GridSizeForPlaneTestingInRANSAC)
            };

            for (int i = 0; i < items.Count; i++)
            {
                UIChangableItem item = items[i];
                int translation = i * 50;

                TrackBar t = new TrackBar();
                Label l1 = new Label();
                Label l2 = new Label();

                ChangableItemsPanel.Controls.Add(t);
                ChangableItemsPanel.Controls.Add(l1);
                ChangableItemsPanel.Controls.Add(l2);

                l1.AutoSize = true;
                l2.AutoSize = true;
                t.AutoSize = false;
                t.Size = new Size((int)(ChangableItemsPanel.Width * 0.8f), (int)(t.Size.Height * 0.55f));

                t.LargeChange = 1;
                t.SmallChange = 1;

                l1.Text = item.Name;
                t.Minimum = item.MinValue;
                t.Maximum = item.MaxValue;

                l1.Top = translation;
                l2.Top = translation;
                t.Top = translation + 20;

                l1.Left = 5;
                l2.Left = 300;

                t.ValueChanged += (object sender, EventArgs e) => { item.OnValueChanged(t, l2); };
                
                t.Value = item.InitValue;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            resetRichTextField = true;
        }

        private bool resetRichTextField = true;

        public void SetNormal(double x, double y, double z, double xx, double yy, double zz)
        {
            if (resetRichTextField)
            {
                richTextBox1.Text = string.Format("{0}, {1}, {2}, {3}, {4}, {5}", x, y, z, xx, yy, zz);
                resetRichTextField = false;
            }
        }


        private void ColorChange(TrackBar t, Label l)
        {
            Config.DepthColorChange = (int)Math.Pow(2, t.Value);
            l.Text = string.Format("{0}...(2048/{1})", t.Value, Config.DepthColorChange);
        }

        private void FramesToAverage(TrackBar t, Label l)
        {
            Config.NumberOfFramesToAverage = t.Value;
            Config.frameNr = 0;
            l.Text = Config.NumberOfFramesToAverage.ToString();
            Config.CreateArrayForFramesToBeAveraged();
        }

        private void RANSACPlaneThickness(TrackBar t, Label l)
        {
            Config.RansacThickness = t.Value * 0.001f;
            l.Text = t.Value.ToString();
        }

        private void Regression(TrackBar t, Label l)
        {
            Config.RegreseTloustka = t.Value * 0.001f;
            l.Text = t.Value.ToString();
        }

        private void Paralelization(TrackBar t, Label l)
        {
            Config.paralelTasksCount = t.Value;
            l.Text = t.Value.ToString();
        }

        private void MinDepth(TrackBar t, Label l)
        {
            Config.minDepth = t.Value / 100f;
            l.Text = Config.minDepth.ToString();
        }

        private void MaxDepth(TrackBar t, Label l)
        {
            Config.maxDepth = t.Value / 100f;
            l.Text = Config.maxDepth.ToString();
        }

        private void AlgorithmPerformedEveryNthFrame(TrackBar t, Label l)
        {
            Config.TrackingIsPerformedEveryXthFrame = t.Value;
            l.Text = t.Value.ToString();
        }

        private void RegressionOnEveryNthTablePixel(TrackBar t, Label l)
        {
            Config.RegressionIsEvaluatedOnEveryXthTablePixel = t.Value;
            l.Text = t.Value.ToString();
        }

        private void GridSizeForPlaneTestingInRANSAC(TrackBar t, Label l)
        {
            Config.GridSizeForTestingPlaneFitInRANSAC = t.Value;
            l.Text = t.Value.ToString();
        }






        public void UpdateFrameCounter()
        {
            FrameCountLabel.Text = "FrameCount: " + Config.FrameCount;
            Config.FrameCount++;
        }

        public void UpdateFPSCounter()
        {
            if ((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) > (Config.FPSLastMeasuredTime + 1000))
            {
                Config.FPSLastMeasuredTime += 1000;
                Config.CurrentFPS = Config.FrameCount - Config.FPSLastMeasuredFrameCount;
                Config.FPSLastMeasuredFrameCount = Config.FrameCount;
            }
            FPSLabel.Text = "FPS: " + Config.CurrentFPS;
        }

        private void RekalibraceBtn_Click(object sender, EventArgs e)
        {
            Config.finalD = 0;
            Config.finalNormal = new MyVector3D(0, 0, 0);
            Config.maxObservedPlaneSize = 0;
        }

        private void VykreslitTrojuhelnikyBtn_Click(object sender, EventArgs e)
        {
            Config.vykreslovatTrojuhelniky = !Config.vykreslovatTrojuhelniky;
        }

        public void Sensor_IsAvailableChanged(bool available)
        {
            availabilityLabel.Text = available ? "Kinect available" : "Kinect not found";
        }











        private void LocateObjectsBtn_Click(object sender, EventArgs e)
        {
            Config.LocationEnabled = true;
        }

        public void UpdateObjectPositions(List<int> list)
        {
            string s = "Object candidates found: " + list.Count + Environment.NewLine;
            foreach (var item in list)
            {
                s += item.ToString() + Environment.NewLine;
            }

            ObjectsSizeLabel.Text = s;
        }

        private void ObjectsSizeLabel_Click(object sender, EventArgs e)
        {

        }

        private void GaussBtn_Click(object sender, EventArgs e)
        {
            Config.Gaussian = !Config.Gaussian;
            GaussBtn.Text = "Gaussian filter" + "(" + Config.Gaussian.ToString() + ")";
        }

        private void SerializationBtn_Click(object sender, EventArgs e)
        {
            Config.SerializeThisFrameToFile = true;
        }

        private void PlaneTrackBtn_Click(object sender, EventArgs e)
        {
            Config.PlaneIsTracked = !Config.PlaneIsTracked;

            if (Config.PlaneIsTracked)
            {
                PlaneTrackBtn.Text = "Plane track enabled";
            }
            else
            {
                PlaneTrackBtn.Text = "Plane track disabled";
            }
        }

       
    }

    public delegate void TrackbarEvent(TrackBar t, Label l);

    public struct UIChangableItem
    {
        public UIChangableItem(string name, int minValue, int maxValue, int initValue, TrackbarEvent onValueChanged)
        {
            Name = name;
            MinValue = minValue;
            MaxValue = maxValue;
            InitValue = initValue;
            OnValueChanged = onValueChanged;
        }

        public string Name;
        public int MinValue;
        public int MaxValue;
        public int InitValue;
        public TrackbarEvent OnValueChanged;
    }
}
