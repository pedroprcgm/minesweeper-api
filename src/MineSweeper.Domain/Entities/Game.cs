using MineSweeper.Domain.Entities.Base;
using MineSweeper.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

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
                    int cellNumber = CalculateCellNumber(x, y);
                    bool hasMine = mineCells.ContainsKey(cellNumber);
                    int? numberOfMinesOnSquare = null;

                    if (!hasMine)
                        numberOfMinesOnSquare = GetNumberOfMinesOnSquare(x, y, mineCells);

                    Cells.Add(new Cell(x, y, hasMine, numberOfMinesOnSquare));
                }
            }
        }

        public Cell GetCell(int row, int col)
            => Cells.Where(wh => wh.Row == row && wh.Col == col).FirstOrDefault();

        /**
         * TODO: Add FluentValidation
         */
        public bool IsValid()
        {
            if (Rows < 1 || Cols < 1 || Mines < 1)
                return false;

            if (string.IsNullOrEmpty(Name))
                return false;

            return true;
        }

        private int GetNumberOfMinesOnSquare(int row, int col, Dictionary<int, int> mines)
        {
            int numberOfMines = 0;

            for (int x = row - 1; x < row + 2; x++)
            {
                if (x < 0 || x >= Rows)
                    continue;

                for (int y = col - 1; y < col + 2; y++)
                {
                    if ((x == row && y == col) || y < 0 || y >= Cols)
                        continue;

                    if (mines.ContainsKey(CalculateCellNumber(x, y)))
                        numberOfMines++;
                }
            }

            return numberOfMines;
        }

        private int GenerateCellForMine(Dictionary<int, int> mineCells)
        {
            Random random = new Random();
            int mineCell = random.Next(0, NumberOfCells);

            while (mineCells.ContainsKey(mineCell))
                mineCell = random.Next(0, Mines);

            return mineCell;
        }

        private int CalculateCellNumber(int x, int y)
        {
            return (x * Cols) + y;
        }
    }
}
