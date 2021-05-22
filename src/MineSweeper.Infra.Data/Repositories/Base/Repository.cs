using MineSweeper.Domain.Entities.Base;
using MineSweeper.Domain.Interfaces.Context;
using MineSweeper.Domain.Interfaces.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MineSweeper.Infra.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : IEntity
    {
        protected readonly IMineSweeperContext _context;
        protected readonly IMongoCollection<T> DbSet;

        protected Repository(IMineSweeperContext context)
        {
            _context = context;

            DbSet = _context.GetCollection<T>(typeof(T).Name);
        }

        public void Create(T entity)
        {
            _context.AddCommand(() => DbSet.InsertOneAsync(entity));
        }

        public void Delete(Guid id)
        {
            _context.AddCommand(() => DbSet.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id)));
        }

        public async Task<IEnumerable<T>> Get()
        {
            return (await DbSet.FindAsync(Builders<T>.Filter.Empty)).ToEnumerable();
        }

        public async Task<T> GetById(Guid id)
        {
            var data = await DbSet.FindAsync(Builders<T>.Filter.Eq("Id", id));
            return data.SingleOrDefault();
        }

        public void Update(T entity)
        {
            _context.AddCommand(() => DbSet.ReplaceOneAsync(document => document.Id == entity.Id, entity));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
