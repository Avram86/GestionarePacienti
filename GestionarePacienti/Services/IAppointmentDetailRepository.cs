using GestionarePacienti.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionarePacienti.Services
{
    public interface IAppointmentDetailRepository
    {
        //auxilliaries
        bool ResourceExists(int id);
        IQueryable<AppointmentDetails> GetQuery();


        //main
        Task<IEnumerable<AppointmentDetails>> GetListAsync();
        Task<AppointmentDetails> GetAsync(int Id);
        Task Create(AppointmentDetails resourceToBeCreated);
        Task<AppointmentDetails> Update(int Id, AppointmentDetails resourceToUpdate);
        Task Delete(int Id);
    }
}
