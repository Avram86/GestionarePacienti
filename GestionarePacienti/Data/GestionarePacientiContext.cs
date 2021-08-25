using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GestionarePacienti.Models;

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
    }
}
