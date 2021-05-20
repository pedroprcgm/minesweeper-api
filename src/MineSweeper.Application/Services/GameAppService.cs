using MineSweeper.Application.Interfaces;
using MineSweeper.Application.ViewModels;
using MineSweeper.Domain.Entities;
using MineSweeper.Domain.Enums;
using MineSweeper.Domain.Interfaces.Context;
using MineSweeper.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineSweeper.Application.Services
{
    public class GameAppService : IGameAppService
    {
        private readonly IGameRepository _repository;
        private readonly IUnitOfWork _uow;

        public GameAppService(IGameRepository repository,
                              IUnitOfWork uow)
        {
            _uow = uow;
            _repository = repository;
        }

        public async Task<Guid> CreateGame(GameViewModel game)
        {
            var _game = new Game(game.Name, game.Rows, game.Cols, game.Mines);

            if (!_game.IsValid())
                throw new ArgumentException();

            _repository.Create(_game);

            await _uow.Commit();

            return _game.Id;
        }

        public async Task<bool> PauseGame(Guid id)
        {
            Game game = await _repository.GetById(id);

            if (game == null)
                throw new ArgumentException("Informed game doesn't exists!");

            game.Pause();

            _repository.Update(game);

            if (!await _uow.Commit())
                throw new Exception("Error to commit changes");

            return true;
        }

        public async Task<bool> ResumeGame(Guid id)
        {
            Game game = await _repository.GetById(id);

            if (game == null)
                throw new ArgumentException("Informed game doesn't exists!");

            game.Resume();

            _repository.Update(game);

            if (!await _uow.Commit())
                throw new Exception("Error to commit changes");

            return true;
        }

        public async Task<VisitCellResultViewModel> VisitCell(Guid id, int row, int col)
        {
            var visitCellResult = new VisitCellResultViewModel();

            Game game = await _repository.GetById(id);

            if (game == null)
                throw new ArgumentException("Informed game doesn't exists!");

            if (!game.ExistsCell(row, col))
                throw new ArgumentException("Informed cell doesn't exists!");

            Cell cell = game.GetCell(row, col);

            cell.SetVisited();

            if (cell.HasMine)
            {
                visitCellResult.HasMine = true;
                visitCellResult.IsWinner = false;
                visitCellResult.IsGameDone = true;

                game.SetDone(isWinner: false);

                var mines = game.GetMines();

                visitCellResult.Mines = mines.Select(mine => new CellViewModel(mine.Row, mine.Col, value: null, hasMine: true)).ToList();

            }
            else
            {
                visitCellResult.HasMine = false;

                /**
                 * Get square cell information
                 */
                if (cell.NumberOfMinesOnSquare == 0)
                {
                    List<Cell> cells = game.ExploreForCellsFromCell(cell).ToList();

                    cells.ForEach(c => c.SetVisited());

                    visitCellResult.Cells = cells.Select(c => new CellViewModel(c.Row, c.Col, value: c.NumberOfMinesOnSquare, hasMine: false)).ToList();
                }

                /**
                 * Check if the game is over
                 * If its over change game state to done
                 * Return all cells information
                 */
                if (game.IsOver())
                {
                    game.SetDone(isWinner: true);

                    visitCellResult.IsWinner = true;
                    visitCellResult.IsGameDone = true;

                    var mines = game.GetMines();

                    visitCellResult.Mines = mines.Select(mine => new CellViewModel(mine.Row, mine.Col, value: null, hasMine: true)).ToList();
                }

                /**
                 * If game is not over 
                 * Return square cell information
                 */
                visitCellResult.Cells.Add(new CellViewModel(cell.Row, cell.Col, cell.NumberOfMinesOnSquare, cell.HasMine));
            }

            _repository.Update(game);

            if (!await _uow.Commit())
                throw new Exception("Error to commit changes");

            return visitCellResult;
        }

        public async Task<bool> FlagCell(Guid id, int row, int col, FlagCellViewModel flagCell)
        {
            Game game = await _repository.GetById(id);

            if (!game.ExistsCell(row, col))
                throw new ArgumentException();

            if (!Enum.IsDefined(typeof(CellFlagEnum), flagCell.Flag))
                throw new ArgumentException("Invalid flag!", nameof(flagCell.Flag));

            Cell cell = game.GetCell(row, col);

            var flag = (CellFlagEnum)flagCell.Flag;

            cell.SetFlag(flag);

            _repository.Update(game);

            if (!await _uow.Commit())
                throw new Exception("Error to commit changes");

            return true;
        }
    }
}
