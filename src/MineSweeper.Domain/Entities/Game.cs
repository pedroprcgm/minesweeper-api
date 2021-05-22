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
            LastStartDateTime = DateTime.UtcNow;
        }

        public Game(string name, int rows, int cols, int mines, Guid userId) : this()
        {
            Name = name;
            Rows = rows;
            Cols = cols;
            Mines = mines;
            UserId = userId;

            BuildCells();
        }

        public string Name { get; set; }

        public GameStatusEnum Status { get; set; }

        public GameResultEnum? Result { get; set; }

        public int Rows { get; set; }

        public int Cols { get; set; }

        public int Mines { get; set; }

        public long TotalTimePlayed { get; set; }

        public DateTime LastStartDateTime { get; set; }

        public Guid UserId { get; set; }

        internal long CurrentSession
        {
            get
            {
                if (Status != GameStatusEnum.InProgress)
                    return 0;

                return (long)(DateTime.UtcNow - LastStartDateTime).TotalSeconds;
            }
        }

        internal int NumberOfCells
        {
            get
            {
                return Rows * Cols;
            }
        }

        public long GetCurrentTotalTimePlayed()
            => TotalTimePlayed + CurrentSession;

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

        public void Pause()
        {
            TotalTimePlayed += CurrentSession;
            Status = GameStatusEnum.Paused;
        }

        public void Resume()
        {
            Status = GameStatusEnum.InProgress;
            LastStartDateTime = DateTime.UtcNow;
        }

        public bool ExistsCell(int row, int col)
            => Cells.Any(wh => wh.Row == row && wh.Col == col);

        public IEnumerable<Cell> GetMines()
            => Cells.Where(wh => wh.HasMine);

        public void SetDone(bool isWinner)
        {
            Status = GameStatusEnum.Done;
            Result = isWinner
                   ? GameResultEnum.UserWon
                   : GameResultEnum.UserLost;
        }

        public IEnumerable<Cell> ExploreForCellsFromCell(Cell cell)
        {
            IEnumerable<Cell> exploredCells = new List<Cell>();

            for (int x = cell.Row - 1; x < cell.Row + 2; x++)
            {
                if (x < 0 || x >= Rows)
                    continue;

                for (int y = cell.Col - 1; y < cell.Col + 2; y++)
                {
                    if ((x == cell.Row && y == cell.Col) || y < 0 || y >= Cols)
                        continue;

                    var currentCell = GetCell(x, y);
                    exploredCells = exploredCells.Append(currentCell);

                    if (currentCell.NumberOfMinesOnSquare == 0 && !currentCell.IsVisited)
                    {
                        currentCell.SetVisited();
                        exploredCells = exploredCells.Union(ExploreForCellsFromCell(currentCell));
                    }
                }
            }

            return exploredCells;
        }

        public bool IsOver()
        {
            IEnumerable<Cell> cells = Cells.Where(wh => !wh.HasMine);

            return cells.All(wh => wh.IsVisited);
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
            => (x * Cols) + y;
    }
}
