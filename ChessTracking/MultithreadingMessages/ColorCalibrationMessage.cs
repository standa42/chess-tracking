﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.MultithreadingMessages
{
    /// <summary>
    /// Message demanding color calibration change
    /// </summary>
    class ColorCalibrationMessage : Message
    {
        public double CalibrationConstant { get; set; }

        public ColorCalibrationMessage(double calibrationConstant)
        {
            CalibrationConstant = calibrationConstant;
        }
    }
}
