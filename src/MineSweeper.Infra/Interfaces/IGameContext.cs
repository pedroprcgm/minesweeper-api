using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace MineSweeper.Domain.Interfaces.Context
{
    public interface IGameContext : IDisposable
    {
        Task<bool> SaveChanges();

        void AddCommand(Func<Task> func);

        IMongoCollection<T> GetCollection<T>(string collectionName);
    }

    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}
