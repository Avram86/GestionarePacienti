using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GestionarePacienti.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using GestionarePacienti.Enumerations;

namespace GestionarePacienti.Data
{
    public class GestionarePacientiContext : DbContext
    {
        public GestionarePacientiContext (DbContextOptions<GestionarePacientiContext> options)
            : base(options)
        {
        }

        public DbSet<GestionarePacienti.Models.Patient> Patient { get; set; }

        public DbSet<GestionarePacienti.Models.Doctor> Doctor { get; set; }

        public DbSet<GestionarePacienti.Models.AppointmentDetails> AppointmentDetails { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Doctor>().HasData(
        //       new Doctor { Id = 1, Name = "Dr. Test Unu", Specialization = Specialization.Dentist },
        //       new Doctor { Id = 2, Name = "Dr. Test Doi", Specialization = Specialization.Orthopedist },
        //       new Doctor { Id = 2, Name = "Dr. Test Trei", Specialization = Specialization.Psychiatrist });

        //    modelBuilder.Entity<Patient>().HasData(
        //        new Patient
        //        {
        //            Id = 1,
        //            Name = "John Doe",
        //            DateOfBirth = new DateTime(1980, 07, 23),
        //            MaritalStatus = MaritalStatus.Single,
        //            Gender = Gender.Male,
        //            County = "Bihor",
        //            City = "Oradea",
        //            Address = "no 51, Test Street",
        //            Phone = "0777777777"
        //        },
        //        new Patient
        //        {
        //            Id = 2,
        //            Name = "Jane Doe",
        //            DateOfBirth = new DateTime(1990, 11, 05),
        //            MaritalStatus = MaritalStatus.Married,
        //            Gender = Gender.Female,
        //            County = "Bihor",
        //            City = "Alesd",
        //            Address = "no 10, Test2 Street",
        //            Phone = "0777111222"
        //        },
        //        new Patient
        //        {
        //            Id = 3,
        //            Name = "Jessica Doe",
        //            DateOfBirth = new DateTime(2000, 01, 28),
        //            MaritalStatus = MaritalStatus.Single,
        //            Gender = Gender.Female,
        //            County = "Bihor",
        //            City = "Marghita",
        //            Address = "no 20, Test3 Street",
        //            Phone = "0772333444"
        //        });

        //    modelBuilder.Entity<AppointmentDetails>().HasData(
        //        new AppointmentDetails
        //        {
        //            Id = 1,
        //            DateOfAppointment = DateTime.Now,
        //            Symptoms = "aaaa aaa aaaa aaa aaaaa",
        //            Diagnostic = "aaaa aaa aaaa aaa aaaaa",
        //            DiagnosticCode = "Abc",
        //            Recommandation = "aaaa aaa aaaa aaa aaaaa"
        //        },
        //        new AppointmentDetails
        //        {
        //            Id = 2,
        //            DateOfAppointment = DateTime.Now,
        //            Symptoms = "bbb bbb bbbb ",
        //            Diagnostic = "bbb bbb bbbb ",
        //            DiagnosticCode = "Abc",
        //            Recommandation = "bbb bbb bbbb "
        //        },
        //        new AppointmentDetails
        //        {
        //            Id = 3,
        //            DateOfAppointment = DateTime.Now,
        //            Symptoms = "cccc ccc cccccc ",
        //            Diagnostic = "cccc ccc cccccc ",
        //            DiagnosticCode = "Def",
        //            Recommandation = "cccc ccc cccccc "
        //        });
        //}
    }
}
