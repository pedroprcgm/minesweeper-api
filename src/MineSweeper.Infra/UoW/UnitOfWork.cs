using MineSweeper.Domain.Interfaces.Context;
using System.Threading.Tasks;

namespace MineSweeper.Infra.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IGameContext _context;

        public UnitOfWork(IGameContext context)
        {
            _context = context;
        }

        public async Task<bool> Commit()
        {
            return await _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
