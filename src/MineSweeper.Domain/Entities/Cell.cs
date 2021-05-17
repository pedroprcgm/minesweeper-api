using MineSweeper.Domain.Enums;

namespace MineSweeper.Domain.Entities
{
    public class Cell
    {
        public Cell()
        {
            IsVisited = false;
            Flag = CellFlagEnum.None;
        }

        public Cell(int row, int col, bool hasMine) : this()
        {
            Row = row;
            Col = col;
            HasMine = hasMine;

            BuildKey();
        }

        public string Key { get; set; }

        public bool IsVisited { get; set; }

        public int Row { get; set; }

        public int Col { get; set; }

        public bool HasMine { get; set; }

        public CellFlagEnum Flag { get; set; }

        public void BuildKey()
        {
            Key = $"{ Row }-{ Col }";
        }
    }
}
