using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.Utils
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Flips 2D structure represented by originalArray, width and height horizontally
        /// Intended use is flipping of images
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalArray">Data of structure</param>
        /// <param name="width">Width of structure</param>
        /// <param name="height">Height of structure</param>
        /// <returns>Flipped structure</returns>
        public static T[] FlipHorizontally<T>(this T[] originalArray, int width, int height)
        {
            T[] newArray = new T[width * height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    newArray[y * width + (width - x - 1)] = originalArray[y * width + x];
                }
            }

            return newArray;
        }

        /// <summary>
        /// Flips 2D structure represented by originalArray, width, height and channels horizontally
        /// Intended use is flipping of images
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalArray">Data of structure</param>
        /// <param name="width">Width of structure</param>
        /// <param name="height">Height of structure</param>
        /// <param name="channels">Number of channels of 2D structure</param>
        /// <returns>Flipped structure</returns>
        public static T[] FlipHorizontally<T>(this T[] originalArray, int width, int height, int channels)
        {
            var widthWithChannels = width * channels;
            T[] newArray = new T[widthWithChannels * height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int ch = 0; ch < channels; ch++)
                    {
                        newArray[y * widthWithChannels + (widthWithChannels - (x * channels)) - channels + ch] 
                            = originalArray[y * widthWithChannels + (x * channels) + ch];
                    }
                }
            }

            return newArray;
        }
        
    }
}
