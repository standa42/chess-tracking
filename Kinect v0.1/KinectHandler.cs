using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using Microsoft.Kinect;
using System.Drawing;
using System.Drawing.Imaging;

using System.Windows.Forms;

using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Kinect_v0._1
{
    class KinectHandler
    {

        #region Variables

        /// <summary>
        /// Graphics interface
        /// </summary>
        Form1 form = null;

        /// <summary>
        /// Canvas for Bitmap drawing
        /// </summary>
        Graphics formGraphics;

        /// <summary>
        /// Active Kinect sensor
        /// </summary>
        private KinectSensor kinectSensor = null;

        /// <summary>
        /// Reader for depth frames
        /// </summary>
        private DepthFrameReader depthFrameReader = null;

        /// <summary>
        /// Description of the data contained in the depth frame
        /// </summary>
        private FrameDescription depthFrameDescription = null;

        /// <summary>
        /// Bitmap to display
        /// </summary>
        private Bitmap b1;
        private Bitmap b2;
        private Bitmap b3;

        /// <summary>
        /// Intermediate storage for frame data converted to color
        /// </summary>
        private byte[] depthPixels = null;

        #endregion

        #region Necessary Functions

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public KinectHandler(Form form)
        {
            this.form = (Form1)form;
            this.formGraphics = form.CreateGraphics();

            // Dispose Kinect OnClose
            form.FormClosing += MainWindow_Closing;

            // get the kinectSensor object
            this.kinectSensor = KinectSensor.GetDefault();

            // open the reader for the depth frames
            this.depthFrameReader = this.kinectSensor.DepthFrameSource.OpenReader();

            // wire handler for frame arrival
            this.depthFrameReader.FrameArrived += this.Reader_FrameArrived;

            // get FrameDescription from DepthFrameSource
            this.depthFrameDescription = this.kinectSensor.DepthFrameSource.FrameDescription;

            // allocate space to put the pixels being received and converted
            this.depthPixels = new byte[this.depthFrameDescription.Width * this.depthFrameDescription.Height];

            // create the bitmap to display
            this.b1 = new Bitmap(this.depthFrameDescription.Width, this.depthFrameDescription.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb); //PixelFormats.Gray8
            this.b2 = new Bitmap(this.depthFrameDescription.Width, this.depthFrameDescription.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            this.b3 = new Bitmap(this.depthFrameDescription.Width, this.depthFrameDescription.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // set IsAvailableChanged event notifier
            this.kinectSensor.IsAvailableChanged += this.Sensor_IsAvailableChanged;
            Sensor_IsAvailableChanged(null, null);

            // open the sensor 
            this.kinectSensor.Open();

            Load();
        }

        /// <summary>
        /// Execute shutdown tasks
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (this.depthFrameReader != null)
            {
                // DepthFrameReader is IDisposable
                this.depthFrameReader.Dispose();
                this.depthFrameReader = null;
            }

            if (this.kinectSensor != null)
            {
                this.kinectSensor.Close();
                this.kinectSensor = null;
            }
        }

        public void UpdateCounters()
        {
            form.UpdateFrameCounter();
            form.UpdateFPSCounter();
        }

        public void DoApplicationEvents()
        {
            Application.DoEvents();
        }

        public void Load()
        {
            InitMedianFilterVariables();

            Config.FPSLastMeasuredTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }


        public void DrawRANSACPointsAsTriangles()
        {
            int pocetIteraci = Config.triangleCount;

            Random rnd = new Random();

            for (int i = 0; i < pocetIteraci; i++)
            {
                Pen pen = new Pen(Color.FromArgb(rnd.Next(10, 255), rnd.Next(10, 255), rnd.Next(10, 255), 0));

                Position2D prvni = new Position2D(Config.tringlePointsForRANSAC[i * 3 + 0] % Config.DepthImageWidth, Config.tringlePointsForRANSAC[i * 3 + 0] / Config.DepthImageWidth);
                Position2D druhy = new Position2D(Config.tringlePointsForRANSAC[i * 3 + 1] % Config.DepthImageWidth, Config.tringlePointsForRANSAC[i * 3 + 1] / Config.DepthImageWidth);
                Position2D treti = new Position2D(Config.tringlePointsForRANSAC[i * 3 + 2] % Config.DepthImageWidth, Config.tringlePointsForRANSAC[i * 3 + 2] / Config.DepthImageWidth);

                formGraphics.DrawLine(pen, prvni.X, prvni.Y, druhy.X, druhy.Y);
                formGraphics.DrawLine(pen, druhy.X, druhy.Y, treti.X, treti.Y);
                formGraphics.DrawLine(pen, treti.X, treti.Y, prvni.X, prvni.Y);
            }
        }

        public void InitMedianFilterVariables()
        {
            if (Config.queues == null)
            {
                Config.queues = new float[Config.DepthImageHeight * Config.DepthImageWidth][];
                Config.medians = new float[Config.DepthImageHeight * Config.DepthImageWidth][];

                for (int i = 0; i < Config.DepthImageHeight * Config.DepthImageWidth; i++)
                {
                    Config.queues[i] = new float[Config.NumberOfFramesForMedian];
                    Config.medians[i] = new float[Config.NumberOfFramesForMedian];
                }
            }
        }

        private void Sensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs e)
        {
            form.Sensor_IsAvailableChanged(this.kinectSensor.IsAvailable);
        }

        #endregion

        /// <summary>
        /// Handles the depth frame data arriving from the sensor
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Reader_FrameArrived(object sender, DepthFrameArrivedEventArgs e)
        {
            using (DepthFrame depthFrame = e.FrameReference.AcquireFrame())
            {
                if (depthFrame != null)
                {
                    using (Microsoft.Kinect.KinectBuffer depthBuffer = depthFrame.LockImageBuffer())
                    {
                        Data data = new Data(kinectSensor, depthBuffer, depthFrameDescription);

                        data.CutOffMinMaxDepth(Config.minDepth, Config.maxDepth);

                        data.MakeAverageOverSeveralFrames(Config.NumberOfFramesToAverage);
                        //data.MedianovyFiltr();

                        data.Render(b2, formGraphics, new Position2D(0, -54));

                        if (
                            (Config.FrameCount > Config.FrameThatStartTracking) &&
                            ((Config.FrameCount % Config.TrackingIsPerformedEveryXthFrame) == 0) &&
                            Config.PlaneIsTracked
                            )
                        {
                            try
                            {
                                //if (Config.Gaussian) data.GaussianFilterOnZBasedOnBitmapCoordinates();
                                data.RANSAC();
                                data.LargestTableArea();
                                data.Render(b2, formGraphics, new Position2D(512, -54));
                                data.LinearRegression();
                                data.LargestTableArea();

                                data.Render(b2, formGraphics, new Position2D(0, 370));
                                data.LinearRegression();
                                data.LargestTableArea();
                                data.LinearRegression();
                                data.LargestTableArea();
                                data.LinearRegression();
                                data.LargestTableArea();

                                data.RotationTo2D(form);

                                if (Config.Gaussian) data.GaussianFilterOnZBasedOnBitmapCoordinates();

                                data.ConvexHullAlgorithmModified();

                                if (Config.LocationEnabled) { Config.LocationEnabled = false; data.LocateObjects(form); }

                                //data.SerializeToFile(""); // @"D:\Desktop\"

                                data.Render(b1, formGraphics, new Position2D(512, 370));
                            }
                            catch (Exception ex)
                            {
                            }

                        }







                        if (Config.vykreslovatTrojuhelniky) DrawRANSACPointsAsTriangles();

                        UpdateCounters();
                        DoApplicationEvents();
                    }
                }
            }
        }



    }
}
