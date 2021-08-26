using GestionarePacienti.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionarePacienti.Controllers
{
    [Authorize]
    public class ChatController: Controller
    {
       
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
