using GestionarePacienti.Data;
using GestionarePacienti.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

        //https://stackoverflow.com/questions/33057838/httpcontext-is-null-for-mvc-controller/33057885
        public IActionResult Index()
        {
            if (HttpContext == null)
                throw new ArgumentNullException("HttpContext");
            else if (HttpContext.User == null)
                throw new ArgumentNullException("HttpContext.User");
            else if (HttpContext.User.Identity == null)
                throw new ArgumentNullException("HttpContext.User.Identity");
            else
            {
                //https://chsakell.com/2013/05/02/4-basic-ways-to-pass-data-from-controller-to-view-in-asp-net-mvc/
                TempData["userName"] = HttpContext.User.Identity.Name.Split('@')[0];
            }

            return View();
        }
    }
}
