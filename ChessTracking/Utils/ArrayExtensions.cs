using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.Utils
{
    public static class ArrayExtensions
    {
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
                        newArray[y * widthWithChannels + (widthWithChannels - (x * channels)) - channels + ch] = originalArray[y * widthWithChannels + (x * channels) + ch];
                    }
                }
            }

            return newArray;
        }
        


    }
}
