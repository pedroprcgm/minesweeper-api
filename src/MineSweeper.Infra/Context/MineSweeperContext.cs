using Microsoft.Extensions.Options;
using MineSweeper.Domain.Interfaces.Context;
using MineSweeper.Infra.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineSweeper.Infra.Context
{
    public class MineSweeperContext : IMineSweeperContext
    {
        private List<Func<Task>> _commands;
        private readonly DatabaseConnectionSettings _databaseConnectionSettings;

        private IMongoDatabase Database { get; set; }
        
        public IClientSessionHandle Session { get; set; }
        
        public MongoClient MongoClient { get; set; }

        public MineSweeperContext(IOptions<DatabaseConnectionSettings> settings)
        {
            _databaseConnectionSettings = settings.Value;
            Configure();
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return Database.GetCollection<T>(collectionName);
        }

        public async Task<bool> SaveChanges()
        {
            using (Session = MongoClient.StartSession())
            {
                Session.StartTransaction();

                IEnumerable<Task> commandTasks = _commands.Select(task => task());

                await Task.WhenAll(commandTasks);

                await Session.CommitTransactionAsync();
            }

            bool isCommited = _commands.Any();

            _commands.Clear();

            return isCommited;
        }

        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }

        private void Configure()
        {
            MongoClient = new MongoClient(_databaseConnectionSettings.ConnectionString);
            Database = MongoClient.GetDatabase(_databaseConnectionSettings.Database);

            _commands = new List<Func<Task>>();
        }

        public void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
