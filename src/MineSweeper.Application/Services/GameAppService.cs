using MineSweeper.Application.Interfaces;
using MineSweeper.Application.ViewModels;
using MineSweeper.Domain.Entities;
using MineSweeper.Domain.Interfaces.Context;
using MineSweeper.Domain.Interfaces.Repositories;
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

        public async Task CreateGame(GameViewModel game)
        {
            var _game = new Game(game.Name, game.Rows, game.Cols, game.Mines);

            _repository.Create(_game);

            await _uow.Commit();
        }
    }
}
