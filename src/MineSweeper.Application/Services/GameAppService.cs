using MineSweeper.Application.Interfaces;
using MineSweeper.Application.ViewModels;
using MineSweeper.Domain.Entities;
using MineSweeper.Domain.Interfaces.Context;
using MineSweeper.Domain.Interfaces.Repositories;
using System;
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

        public async Task<bool> CreateGame(GameViewModel game)
        {
            var _game = new Game(game.Name, game.Rows, game.Cols, game.Mines);

            if (!_game.IsValid())
                throw new ArgumentException();

            _repository.Create(_game);

            await _uow.Commit();

            return true;
        }

        public async Task<bool> VisitCell(Guid id, int row, int col)
        {
            var game = await _repository.GetById(id);

            Cell cell = game.GetCell(row, col);

            cell.SetVisited();

            if (cell.HasMine)
            {
                /**
                 * Inform all mines on the game
                 */

                /**
                 * Change game state to done
                 */
            }
            else
            {
                /**
                 * Get square cell information
                 */

                /**
                 * Check if the game is over
                 * If its over change game state to done
                 * Return all cells information
                 */

                /**
                 * If game is not over 
                 * Return square cell information
                 */
            }

            _repository.Update(game);

            await _uow.Commit();

            return true;
        }
    }
}
