using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace MineSweeper.Domain.Interfaces.Context
{
    public interface IMineSweeperContext : IDisposable
    {
        Task<bool> SaveChanges();

        void AddCommand(Func<Task> func);

        IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}
