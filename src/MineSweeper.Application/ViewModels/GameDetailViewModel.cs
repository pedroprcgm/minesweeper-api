using System;

namespace MineSweeper.Application.ViewModels
{
    public class GameDetailViewModel : GameViewModel
    {
        public GameDetailViewModel()
        {
        }

        public GameDetailViewModel(Guid id, string name, int rows, int cols, int mines, long totalTimePlayed, string status)
        {
            Id = id;
            Name = name;
            Rows = rows;
            Cols = cols;
            Mines = mines;
            TotalTimePlayed = totalTimePlayed;
            Status = status;
        }

        public long TotalTimePlayed { get; set; }

        public string Status { get; set; }
    }
}
