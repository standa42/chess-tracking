using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.UserInterface
{
    class UserInterfaceOutputFacade
    {
        private MainGameForm MainForm { get; }
        private VizualizationForm VizualizationForm { get; }

        public UserInterfaceOutputFacade(MainGameForm mainForm, VizualizationForm vizualizationForm)
        {
            MainForm = mainForm;
            VizualizationForm = vizualizationForm;
        }

        public void UpdateImmediateBoard(Bitmap bitmap)
        {
            MainForm?.UpdateImmediateBoard(bitmap);
        }

        public void UpdateAveragedBoard(Bitmap bitmap)
        {
            MainForm?.UpdateAveragedBoard(bitmap);
        }

        public void DisplayVizuaization(Bitmap bitmap)
        {
            VizualizationForm?.DisplayVizulization(bitmap);
        }

        public void UpdateFps(int fps)
        {
            MainForm?.UpdateFps(fps);
        }

        public void HandDetected(bool detection)
        {
            MainForm?.HandDetectionUpdate(detection);
        }

        public void UpdateBoardState(Bitmap bitmap)
        {
            MainForm?.UpdateBoardState(bitmap);
        }

        public void UpdateRecordState(IList<string> records)
        {
            MainForm?.UpdateRecordState(records);
        }

        public void AddToUserLog(string line)
        {
            MainForm?.AddToUserLog(line);
        }

        public void AddToTrackingLog(string line)
        {
            MainForm?.AddToTrackingLog(line);
        }

        public void Clear()
        {
            MainForm?.Clear();
        }

        public void GameRunningLockState()
        {
            MainForm?.GameRunningLockState();
        }

        public void InitialUiLockState()
        {
            MainForm?.InitialUiLockState();
        }

        public void StartedTrackingLockState()
        {
            MainForm?.StartedTrackingLockState();
        }

        public void TrackingLockState()
        {
            MainForm?.TrackingLockState();
        }

        public void GameFinishedLockState()
        {
            MainForm?.GameFinishedLockState();
        }
    }
}
