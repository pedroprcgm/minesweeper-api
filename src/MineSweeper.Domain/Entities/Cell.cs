using MineSweeper.Domain.Enums;
using System;

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

        public string Key { get; private set; }

        public bool IsVisited { get; private set; }

        public int Row { get; private set; }

        public int Col { get; private set; }

        public bool HasMine { get; private set; }

        public CellFlagEnum Flag { get; private set; }

        public void BuildKey()
        {
            Key = $"{ Row }-{ Col }";
        }

        public void SetVisited()
        {
            IsVisited = true;
        }
    }
}
