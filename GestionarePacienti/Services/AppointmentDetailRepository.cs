using GestionarePacienti.Models;
using GestionarePacienti.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace GestionarePacienti.Services
{
    public class AppointmentDetailRepository:IAppointmentDetailRepository
    {
        private readonly GestionarePacientiContext _context;
        private readonly ILogger<AppointmentDetailRepository> _logger;

        public AppointmentDetailRepository(GestionarePacientiContext context, ILogger<AppointmentDetailRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<AppointmentDetails>> GetListAsync()
        {
            var data = await _context.AppointmentDetails.Include(a => a.Patient).Include(a => a.Doctor).ToListAsync();

            return data;
        }

        public  async Task<AppointmentDetails> GetAsync(int id)
        {
            var appointment =await _context.AppointmentDetails.Include(a => a.Patient).Include(a => a.Doctor).FirstOrDefaultAsync(a => a.Id == id);

            return appointment;
        }

        public bool ResourceExists(int id)
        {
            return _context.AppointmentDetails.Any(T => T.Id == id);
        }

        public async Task Create(AppointmentDetails resourceToBeCreated)
        {
            _context.AppointmentDetails.Add(resourceToBeCreated);

            await _context.SaveChangesAsync();
        }

        public async Task<AppointmentDetails> Update(int id, AppointmentDetails resourceToUpdate)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (resourceToUpdate is null)
            {
                throw new ArgumentNullException(nameof(resourceToUpdate));
            }

            _context.AppointmentDetails.Update(resourceToUpdate);

            await _context.SaveChangesAsync();

            _logger.LogInformation($"Resource updated is: {resourceToUpdate.Id}");

            return await _context.AppointmentDetails.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (ResourceExists(id))
            {
                var resourceToBeDeleted = await _context.AppointmentDetails.FirstOrDefaultAsync(t => t.Id == id);

                _logger.LogInformation($"Resource to be deleted is: {resourceToBeDeleted.Id}");

                _context.AppointmentDetails.Remove(resourceToBeDeleted);

                await _context.SaveChangesAsync();
            }
        }

        public IQueryable<AppointmentDetails> GetQuery()
        {
            return _context.AppointmentDetails.AsQueryable();
        }
    }
}
