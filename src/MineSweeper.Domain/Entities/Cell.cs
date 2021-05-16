using MineSweeper.Domain.Enums;

namespace MineSweeper.Domain.Entities
{
    public class Cell
    {
        public string Key { get; set; }

        public bool IsVisited { get; set; }

        public int Row { get; set; }

        public int Col { get; set; }

        public bool HasMine { get; set; }

        public CellFlagEnum Flag { get; set; }
    }
}
