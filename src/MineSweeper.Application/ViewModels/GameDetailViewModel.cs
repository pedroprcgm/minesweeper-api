namespace MineSweeper.Application.ViewModels
{
    public class GameDetailViewModel : GameViewModel
    {
        public GameDetailViewModel()
        {
        }

        public GameDetailViewModel(string name, int rows, int cols, int mines, long totalTimePlayed, string status)
        {
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
