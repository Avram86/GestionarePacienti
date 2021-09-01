using GestionarePacienti.Data.Entities;
using GestionarePacienti.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionarePacienti.Controllers
{
    [Authorize]
    public class StatisticsController:Controller
    {
        private readonly IAppointmentDetailRepository _repository;
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IRepository<Patient> _patientRepository;

        public StatisticsController(IAppointmentDetailRepository repository, IRepository<Doctor> doctorRepository, IRepository<Patient> patientRepository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
        }

        public async Task<IActionResult> Index()
        {
            var queryDoctors = _doctorRepository.GetQuery();
            var doctorsList =await queryDoctors.Select(d => d.Name).ToListAsync();

            var queryAppointments = _repository.GetQuery();

            //calculam nr de consultatii avute de fiecare medic
            int[] appointmentsList = new int[doctorsList.Count];

            //le stocam intr-un dictionare cu cheia-numele doctorului
            Dictionary<string, int> result = new Dictionary<string, int>();

            int i = 0;
            foreach(var name in doctorsList)
            {
                appointmentsList[i] = queryAppointments.Where(a => a.Doctor.Name == name).Count();

                if (!result.Keys.Contains(name))
                {
                    result.Add(name, appointmentsList[i]);
                }

                i++;
            }

            TempData["doctorsList"] = doctorsList;
            TempData["resultValues"] = result.Values;

            return View();
        }
    }
}
