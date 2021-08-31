using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using GestionarePacienti.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace GestionarePacienti.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<GestionarePacientiUser> _userManager;

        public ConfirmEmailModel(UserManager<GestionarePacientiUser> userManager)
        {
            _userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [TempData]
        public bool ConfirmSuccess { get; set; } = false;

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);


            //added for default role
            var addRoleToUser = await _userManager.AddToRoleAsync(user, "operator");
            if (!addRoleToUser.Succeeded)
            {
                StatusMessage= "Error confirming your email: user was not enrolled in the default role!";
                return Page();
            }

            StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";
            ConfirmSuccess = true;
            return Page();
        }
    }
}
