using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.Utils
{
    /// <summary>
    /// Modification of http://www.labbookpages.co.uk/software/imgProc/otsuThreshold.html by Dr. Andrew Greensted
    /// -> to archieve otsu binarization with mask option that ain't usually supported
    /// </summary>
    static class CustomOtsuBinarization
    {
        /// <summary>
        /// Returns ideal threshold according to otzu binarization
        /// </summary>
        public static int GetOtsuThreshold(Bitmap bmp, int? maskedValue = null)
        {
            var data = bmp.ImageToByteArray();
            var histData = new int[256];
            
            // Calculate histogram
            if (maskedValue.HasValue)
            {
                var value = maskedValue.Value;
                foreach (var d in data)
                {
                    if (d != value)
                    {
                        histData[d]++;
                    }
                }
            }
            else
            {
                foreach (var d in data)
                {
                    histData[d]++;
                }
            }
               
            
            // Total number of pixels
            int total = histData.Sum();

            float sum = 0;
            for (int t = 0; t < 256; t++) sum += t * histData[t];

            float sumB = 0;
            int wB = 0;
            int wF = 0;

            float varMax = 0;
            var threshold = 0;

            for (int t = 0; t < 256; t++)
            {
                wB += histData[t];               // Weight Background
                if (wB == 0) continue;

                wF = total - wB;                 // Weight Foreground
                if (wF == 0) break;

                sumB += (float)(t * histData[t]);

                float mB = sumB / wB;            // Mean Background
                float mF = (sum - sumB) / wF;    // Mean Foreground

                // Calculate Between Class Variance
                float varBetween = (float)wB * (float)wF * (mB - mF) * (mB - mF);

                // Check if new maximum found
                if (varBetween > varMax)
                {
                    varMax = varBetween;
                    threshold = t;
                }
            }

            return threshold;
        }
    }
}
