using MineSweeper.Domain.Entities.Base;
using MineSweeper.Domain.Enums;
using System.Collections.Generic;

namespace MineSweeper.Domain.Entities
{
    public class Game : Entity
    {
        public Game()
        {
            Cells = new List<Cell>();
        }

        public string Name { get; set; }

        public GameStatusEnum Status { get; set; }

        public int Rows { get; set; }

        public int Cols { get; set; }

        public ICollection<Cell> Cells { get; set; }
    }
}
