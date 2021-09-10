using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionarePacienti.Controllers
{
    [Authorize]
    public class CalendarController:Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
