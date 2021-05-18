namespace MineSweeper.Application.ViewModels
{
    public class CellViewModel
    {
        public CellViewModel()
        {
        }

        public CellViewModel(int row, int col, int? value, bool hasMine)
        {
            Row = row;
            Col = col;
            Value = value;
            HasMine = hasMine;
        }

        public int Row { get; set; }

        public int Col { get; set; }

        public int? Value { get; set; }

        public bool HasMine { get; set; }
    }
}
