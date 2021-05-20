using MineSweeper.Application.ViewModels;
using System;
using System.Threading.Tasks;

namespace MineSweeper.Application.Interfaces
{
    public interface IGameAppService
    {
        Task<Guid> CreateGame(GameViewModel game);

        Task<VisitCellResultViewModel> VisitCell(Guid id, int row, int col);
    }
}
