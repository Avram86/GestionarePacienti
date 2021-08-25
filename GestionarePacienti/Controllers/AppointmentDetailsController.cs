using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionarePacienti.Models;
using GestionarePacienti.Services;
using Microsoft.AspNetCore.Authorization;

namespace GestionarePacienti
{
    [Authorize]
    public class AppointmentDetailsController : Controller
    {
        private readonly IAppointmentDetailRepository _repository;
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IRepository<Patient> _patientRepository;

        public AppointmentDetailsController(IAppointmentDetailRepository repository, IRepository<Doctor> doctorRepository, IRepository<Patient> patientRepository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
        }

        // GET: AppointmentDetails
        public async Task<IActionResult> Index()
        {
            var result = await _repository.GetListAsync();

            return View(result);
        }

        // GET: AppointmentDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentDetails = await _repository.GetAsync(id ?? 0);

            if (appointmentDetails == null)
            {
                return NotFound();
            }

            return View(appointmentDetails);
        }

        // GET: AppointmentDetails/Create
        public async Task<IActionResult> Create()
        {

            //1.incarcam lista tuturor doctorilor
            var doctors = await _doctorRepository.GetListAsync();

            //2.incarcam lista tuturor pacientilor
            var patients = await _patientRepository.GetListAsync();

            //3.pe baza listei de doctori generam optiunile pt select
            var viewModel = new CreateAppointmentViewModel();

            //SelectListItem=(string text,string value) => legam Numele de Id
            viewModel.Doctors = doctors.Select(d => new SelectListItem(d.Name, d.Id.ToString())).ToList();

            //4.generam optiunile pt pacienti
            viewModel.Patients = patients.Select(p => new SelectListItem(p.Name, p.Id.ToString())).ToList();

            //5.randam view folosind CreateViewModel
            return View(viewModel);
        }

        // POST: AppointmentDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,DateOfAppointment,Symptoms,Diagnostic,DiagnosticCode,Recommandation,SelectedDoctorId,SelectedPatientId")] 
        CreateAppointmentViewModel appointmentDetails)
        {
            if (ModelState.IsValid)
            {
                var appointment = new AppointmentDetails()
                {
                    DateOfAppointment = appointmentDetails.DateOfAppointment,
                    Symptoms = appointmentDetails.Symptoms,
                    Diagnostic = appointmentDetails.Diagnostic,
                    DiagnosticCode = appointmentDetails.DiagnosticCode,
                    Recommandation = appointmentDetails.Recommandation
                };

                var doctor = await _doctorRepository.GetAsync(appointmentDetails.SelectedDoctorId);
                var patient = await _patientRepository.GetAsync(appointmentDetails.SelectedPatientId);

                if(doctor is null )
                {
                    throw new ArgumentNullException(nameof(doctor), "The doctor selected was not found!");
                }

                if(patient is null)
                {
                    throw new ArgumentNullException(nameof(patient), "The patient selected was not found!");
                }

                appointment.Doctor = doctor;
                appointment.Patient = patient;

                await _repository.Create(appointment);
                return RedirectToAction(nameof(Index));
            }
            return View(appointmentDetails);
        }

        // GET: AppointmentDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var appointmentDetails = await _repository.GetAsync(id ?? 0);

            var viewModel = new EditAppointmentViewModel();

            viewModel.DateOfAppointment = appointmentDetails.DateOfAppointment;
            viewModel.Symptoms = appointmentDetails.Symptoms;
            viewModel.Diagnostic = appointmentDetails.Diagnostic;
            viewModel.DiagnosticCode = appointmentDetails.DiagnosticCode;
            viewModel.Recommandation = appointmentDetails.Recommandation;
            viewModel.SelectedDoctorId = appointmentDetails.Doctor.Id;
            viewModel.SelectedPatientId = appointmentDetails.Patient.Id;

            var patients = await _patientRepository.GetListAsync();
            var doctors = await _doctorRepository.GetListAsync();

            viewModel.Patients = patients.Select(p => new SelectListItem(p.Name, p.Id.ToString())).ToList();
            viewModel.Doctors = doctors.Select(d => new SelectListItem(d.Name, d.Id.ToString())).ToList();
           
            if (appointmentDetails == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // POST: AppointmentDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateOfAppointment,Symptoms,Diagnostic,DiagnosticCode,Recommandation,SelectedDoctorId,SelectedPatientId")] 
        EditAppointmentViewModel appointmentDetails)
        {
            if (id != appointmentDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var appointment = new AppointmentDetails()
                    {
                        DateOfAppointment = appointmentDetails.DateOfAppointment,
                        Symptoms = appointmentDetails.Symptoms,
                        Diagnostic = appointmentDetails.Diagnostic,
                        DiagnosticCode = appointmentDetails.DiagnosticCode,
                        Recommandation = appointmentDetails.Recommandation
                    };

                    var doctor = await _doctorRepository.GetAsync(appointmentDetails.SelectedDoctorId);
                    var patient = await _patientRepository.GetAsync(appointmentDetails.SelectedPatientId);

                    if (doctor is null)
                    {
                        throw new ArgumentNullException(nameof(doctor), "The doctor selected was not found!");
                    }

                    if (patient is null)
                    {
                        throw new ArgumentNullException(nameof(patient), "The patient selected was not found!");
                    }

                    appointment.Doctor = doctor;
                    appointment.Patient = patient;
                    
                    //!!!!!!!!!!!!!???????
                    //error=> dubleaza inregistrarea daca nu ii asignam ID-ul
                    appointment.Id = id;

                    var result=await _repository.Update(id, appointment);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_repository.ResourceExists(appointmentDetails.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(appointmentDetails);
        }

        // GET: AppointmentDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentDetails = await _repository.GetAsync(id ?? 0);
            if (appointmentDetails == null)
            {
                return NotFound();
            }

            return View(appointmentDetails);
        }

        // POST: AppointmentDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
