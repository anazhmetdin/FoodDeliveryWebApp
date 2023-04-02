// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace FoodDeliveryWebApp.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ISellerRepo _seller;
        private readonly ICustomerRestaurantsRepo _customer;

        public IndexModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ISellerRepo seller,
            ICustomerRestaurantsRepo customer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _seller = seller;
            _customer = customer;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            //[Display(Name = "Address")]
            //public string Address { get; set; }
            public byte[] ProfilePicture { get; set; }
        }

        private async Task LoadAsync(AppUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            byte[] profilePicture = null;

            var seller = _seller.GetById(user.Id);
            var customer = _customer.GetCustomer(user.Id);

            if (seller != null)
            {
                FileStream fs = new("wwwroot/images/restaurant.jpg", FileMode.Open, FileAccess.Read);
                BinaryReader br = new(fs);
                byte[] imageBytes = br.ReadBytes((int)fs.Length);
                profilePicture = seller.Logo ?? imageBytes;
            }
            else if (customer != null)
            {
                FileStream fs = new("wwwroot/images/user.jpg", FileMode.Open, FileAccess.Read);
                BinaryReader br = new(fs);
                byte[] imageBytes = br.ReadBytes((int)fs.Length);
                profilePicture = customer.ProfilePicture ?? imageBytes;
            }

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                ProfilePicture = profilePicture
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile formFile)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            // Handle image upload
            if (Request.Form.Files != null && Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files[0];
                if (file != null && file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await file.CopyToAsync(ms);
                        Input.ProfilePicture = ms.ToArray();
                    }
                }
            }

            // Update profile picture
            if (Input.ProfilePicture != null && Input.ProfilePicture.Length > 0)
            {
                var seller = _seller.GetById(user.Id);
                seller.Logo = Input?.ProfilePicture ?? null;
                var updateResult = await _userManager.UpdateAsync(user);
                var updateSeller = _seller.TryUpdate(seller);
                if (!updateResult.Succeeded && !updateSeller)
                {
                    StatusMessage = "Unexpected error when trying to update profile picture.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
            //return RedirectToAction("Index", "Products", new { area = "Seller" });
        }
    }
}
