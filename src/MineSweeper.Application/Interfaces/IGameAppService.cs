using MineSweeper.Application.ViewModels;
using System;
using System.Threading.Tasks;

namespace MineSweeper.Application.Interfaces
{
    public interface IGameAppService
    {
        Task<Guid> CreateGame(GameViewModel game);

        Task<GameDetailViewModel> GetById(Guid id);

        Task<VisitCellResultViewModel> VisitCell(Guid id, int row, int col);

        Task<bool> FlagCell(Guid id, int row, int col, FlagCellViewModel flag);

        Task<bool> PauseGame(Guid id);

        Task<bool> ResumeGame(Guid id);
    }
}
