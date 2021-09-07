using GestionarePacienti.Data;
using GestionarePacienti.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChatController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        //https://stackoverflow.com/questions/33057838/httpcontext-is-null-for-mvc-controller/33057885
        public async Task<IActionResult> Index()
        {
            if (HttpContext == null)
                throw new ArgumentNullException("HttpContext");
            else if (HttpContext.User == null)
                throw new ArgumentNullException("HttpContext.User");
            else if (HttpContext.User.Identity == null)
                throw new ArgumentNullException("HttpContext.User.Identity");
            else
            {
                ViewData["userName"]=_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c=>c.Type=="name").Value.Split('@')[0];
            }

            return View();
        }
    }
}
