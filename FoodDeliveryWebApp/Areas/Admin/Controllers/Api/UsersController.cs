using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryWebApp.Areas.Admin.Controllers.Api
{
        [Route("api/[controller]")]
        [ApiController]
      //  [Authorize(Roles = "Admin")]
        public class UsersController : ControllerBase
        {
            private readonly UserManager<AppUser> _userManager;

            public UsersController(UserManager<AppUser> userManager)
            {
                _userManager = userManager;
            }


            [HttpDelete]
            public async Task<IActionResult> DeleteUser(string userId)
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                    return NotFound();

                var result = await _userManager.DeleteAsync(user);

                if (!result.Succeeded)
                    throw new Exception();

                return Ok();
            }
        }
    
}
