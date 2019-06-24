using System;
using System.Collections.Generic;
using ChessTracking.Utils;
using Emgu.CV.Structure;

namespace ChessTracking.ImageProcessing.ChessboardAlgorithms
{
    /// <summary>
    /// Contains algorithms from selection of two distinct groups of lines based on similar angle, that have largest line count in sum
    /// </summary>
    static class LinesIntoGroups
    {
        /// <summary>
        /// Selects two distinct groups (according to similar angle) of lines from input with largest line count in sum
        /// </summary>
        /// <param name="lines">recognized lines from image</param>
        /// <param name="angleToleranceInsideGroup">Angle withing which lines can be considered as one group</param>
        /// <returns></returns>
        public static Tuple<LineSegment2D[], LineSegment2D[]> FilterLinesBasedOnAngle(LineSegment2D[] lines, int angleToleranceInsideGroup)
        {
            int possibleAngleDegreesForALine = 180;
            var firstGroupOfLines = new List<LineSegment2D>();
            var secondGroupOfLines = new List<LineSegment2D>();

            // prepare list that contains arrays of lines grouped by their rotation angle - degree precisely
            List<LineSegment2D>[] linesByAngle = new List<LineSegment2D>[possibleAngleDegreesForALine];
            for (int i = 0; i < linesByAngle.Length; i++)
                linesByAngle[i] = new List<LineSegment2D>();

            // put each line into corresponding array
            foreach (var line in lines)
            {
                var diffX = line.P1.X - line.P2.X;
                var diffY = line.P1.Y - line.P2.Y;

                int thetaAngle = ((int) Math.Atan2(diffY, diffX).ConvertRadiansToDegrees()).MathMod(possibleAngleDegreesForALine);
                linesByAngle[thetaAngle].Add(line);
            }

            // find first maximal interval
            int indexOfIntervalWithHightestLineCount = 
                GetStartingIndexOfIntervalWithHighestLineCount(
                    linesByAngle,
                    possibleAngleDegreesForALine, 
                    angleToleranceInsideGroup
                );

            // fill first group and remove those groups of lines from list
            FillGroupWithCorrespondingLinesAndRemoveThemFromList(
                linesByAngle,
                firstGroupOfLines,
                indexOfIntervalWithHightestLineCount,
                possibleAngleDegreesForALine,
                angleToleranceInsideGroup
            );

            // get second maximal interval
            indexOfIntervalWithHightestLineCount =
                GetStartingIndexOfIntervalWithHighestLineCount(
                    linesByAngle,
                    possibleAngleDegreesForALine,
                    angleToleranceInsideGroup
                );

            // fill second group and remove those groups of lines from list
            FillGroupWithCorrespondingLinesAndRemoveThemFromList(
                linesByAngle,
                secondGroupOfLines,
                indexOfIntervalWithHightestLineCount,
                possibleAngleDegreesForALine,
                angleToleranceInsideGroup
            );

            return new Tuple<LineSegment2D[], LineSegment2D[]>(firstGroupOfLines.ToArray(), secondGroupOfLines.ToArray());
        }

        /// <summary>
        /// Searches through all posible intervals of size angleToleranceInsideGroup for the one with highest line count in sum
        /// </summary>
        /// <returns></returns>
        private static int GetStartingIndexOfIntervalWithHighestLineCount(List<LineSegment2D>[] linesByAngle, int possibleAngleDegreesForALine, int angleToleranceInsideGroup)
        {
            // accumulators init
            int maxCount = -1;
            int maxIndex = -1;

            for (int i = 0; i < possibleAngleDegreesForALine; i++)
            {
                int number = 0;
                int index = i;

                // sum ober interval
                // modular arithmetics ensures correct behavior for intervals crossing values 180/0
                for (int j = i; j < i + angleToleranceInsideGroup; j++)
                {
                    number += linesByAngle[j.MathMod(possibleAngleDegreesForALine)].Count;
                }

                if (number > maxCount)
                {
                    maxCount = number;
                    maxIndex = index;
                }
            }

            return maxIndex;
        }

        /// <summary>
        /// Fills group with lines from given interval and erases them from list - so they are not involved in further computation
        /// </summary>
        private static void FillGroupWithCorrespondingLinesAndRemoveThemFromList(List<LineSegment2D>[] linesByAngle, List<LineSegment2D> filledGroup, 
            int startingIndex, int possibleAngleDegreesForALine, int angleToleranceInsideGroup)
        {
            for (int j = startingIndex; j < startingIndex + angleToleranceInsideGroup; j++)
            {
                int modulatedIndex = j.MathMod(possibleAngleDegreesForALine);

                var linesOfCertainAngle = linesByAngle[modulatedIndex];

                filledGroup.AddRange(linesOfCertainAngle);

                linesByAngle[modulatedIndex].Clear();
            }
        }
    }
}
