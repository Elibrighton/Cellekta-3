﻿using SongInterface;
using System.Collections.ObjectModel;
using TraktorLibraryInterface;

namespace Cellekta_3.Model
{
    public interface ISongListModel
    {
        ITraktorLibrary TraktorLibrary { get; set; }
        ObservableCollection<ISong> SongCollection { get; set; }
        int ProgressBarMax { get; set; }
        int ProgressBarValue { get; set; }
        bool ProgressBarIsIndeterminate { get; set; }
        string ProgressBarMessage { get; set; }
        int WindowHeight { get; set; }
        int WindowWidth { get; set; }
        int ListViewHeight { get; set; }
        int ListViewWidth { get; set; }
        int ProgressBarWidth { get; set; }
    }
}
