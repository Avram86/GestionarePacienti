using GestionarePacienti.Data;
using GestionarePacienti.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionarePacienti.Services
{
    public class Repository<T> : IRepository<T>
        where T : BaseClass
    {
      
        private readonly ILogger<Repository<T>> _logger;
        private DbSet<T> _type;

        public Repository(GestionarePacientiContext context, ILogger<Repository<T>> logger)
        {
            DbContext = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _type = DbContext.Set<T>();
        }

        protected GestionarePacientiContext DbContext { get; }

        public async Task Create(T resourceToBeCreated)
        {
            DbContext.Add(resourceToBeCreated);

            await DbContext.SaveChangesAsync();

            _logger.LogInformation($"Resource created with ID: {resourceToBeCreated.Id}");
        }

        public async Task Delete(int id)
        {
            if (id <=0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (ResourceExists(id))
            {
                var resourceToBeDeleted=await _type.FirstOrDefaultAsync(t=>t.Id==id);

                _logger.LogInformation($"Resource to be deleted is: {resourceToBeDeleted.Id}");

                _type.Remove(resourceToBeDeleted);

                await DbContext.SaveChangesAsync();
            }
        }

        public async Task<T> GetAsync(int id)
        {
            return await _type.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<T>> GetListAsync()
        {
            return await _type.ToListAsync();
        }

        public IQueryable<T> GetQuery()
        {
            return _type.AsQueryable<T>();
        }

        public bool ResourceExists(int id)
        {
            return _type.Any(T => T.Id == id);
        }

        public async Task<T> Update(int id, T resourceToUpdate)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (resourceToUpdate is null)
            {
                throw new ArgumentNullException(nameof(resourceToUpdate));
            }

            _type.Update(resourceToUpdate);

            await DbContext.SaveChangesAsync();

            _logger.LogInformation($"Resource updated is: {resourceToUpdate.Id}");

            return await _type.FirstOrDefaultAsync(r => r.Id == id);
            
        }
    }
}
