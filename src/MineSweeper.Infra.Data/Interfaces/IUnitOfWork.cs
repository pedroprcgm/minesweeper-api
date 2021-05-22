using System;
using System.Threading.Tasks;

namespace MineSweeper.Domain.Interfaces.Context
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}
