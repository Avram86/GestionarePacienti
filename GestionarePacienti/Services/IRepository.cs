using GestionarePacienti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionarePacienti.Services
{
    public interface IRepository<T>
        where T:BaseClass
    {
        //auxilliaries
        bool ResourceExists(int id);

        //main
        Task<IEnumerable<T>> GetListAsync();
        Task<T> GetAsync(int Id);
        Task Create(T resourceToBeCreated);
        Task<T> Update(int Id, T resourceToUpdate);
        Task Delete(int Id);
    }
}
