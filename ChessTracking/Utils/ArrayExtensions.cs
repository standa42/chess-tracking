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

        /// <summary>
        /// Horizontal flip on rectangular array
        /// </summary>
        public static T[,] FlipHorizontally<T>(this T[,] originalArray)
        {
            var size = originalArray.GetLength(0);
            var newArray = new T[size, size];

            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                {
                    newArray[x, y] = originalArray[size - x - 1, y];
                }

            return newArray;
        }

        /// <summary>
        /// Rotation of square array n-times by 90 degrees counterclockwise
        /// </summary>
        public static T[,] RotateArray90DegCcwNTimes<T>(this T[,] matrix, int n)
        {
            for (int i = 0; i < n; i++)
                matrix = matrix.RotateArray90DegCcw();

            return matrix;
        }

        /// <summary>
        /// Rotation of square array by 90 degrees counterclockwise
        /// derived from https://stackoverflow.com/questions/42519/how-do-you-rotate-a-two-dimensional-array
        /// </summary>
        public static T[,] RotateArray90DegCcw<T>(this T[,] matrix)
        {
            var size = matrix.GetLength(0);
            var array = new T[size, size];

            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    array[i, j] = matrix[j,size - i - 1];
                }
            }

            return array;
        }
    }
}
