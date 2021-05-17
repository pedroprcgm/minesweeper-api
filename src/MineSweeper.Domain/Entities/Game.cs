using MineSweeper.Domain.Entities.Base;
using MineSweeper.Domain.Enums;
using System;
using System.Collections.Generic;

namespace MineSweeper.Domain.Entities
{
    public class Game : Entity
    {
        public Game()
        {
            Cells = new List<Cell>();
        }

        public Game(string name, int rows, int cols, int mines) : this()
        {
            Name = name;
            Rows = rows;
            Cols = cols;
            Mines = mines;

            BuildCells();
        }

        public string Name { get; set; }

        public GameStatusEnum Status { get; set; }

        public int Rows { get; set; }

        public int Cols { get; set; }

        public int Mines { get; set; }

        internal int NumberOfCells
        {
            get
            {
                return Rows * Cols;
            }
        }

        public ICollection<Cell> Cells { get; set; }

        public void BuildCells()
        {
            if (Mines > NumberOfCells)
                Mines = NumberOfCells;

            var mineCells = new Dictionary<int, int>();

            for (int i = 0; i < Mines; i++)
            {
                int mineCell = GenerateCellForMine(mineCells);

                mineCells.Add(mineCell, mineCell);
            }

            for (int x = 0; x < Rows; x++)
            {
                for (int y = 0; y < Cols; y++)
                {
                    int cellNumber = (x * (Cols-1)) + y;
                    bool hasMine = mineCells.ContainsKey(cellNumber);

                    Cells.Add(new Cell(x, y, hasMine));
                }
            }
        }

        private int GenerateCellForMine(Dictionary<int, int> mineCells)
        {
            Random random = new Random();
            int mineCell = random.Next(0, NumberOfCells);

            while (mineCells.ContainsKey(mineCell))
                mineCell = random.Next(0, Mines);

            return mineCell;
        }
    }
}
