using MineSweeper.Application.Interfaces;
using MineSweeper.Application.ViewModels;
using MineSweeper.Domain.Entities;
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

        public async Task<VisitCellResultViewModel> VisitCell(Guid id, int row, int col)
        {
            var visitCellResult = new VisitCellResultViewModel();

            Game game = await _repository.GetById(id);

            if (!game.ExistsCell(row, col))
                throw new ArgumentException();

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

            await _uow.Commit();

            return visitCellResult;
        }
    }
}
