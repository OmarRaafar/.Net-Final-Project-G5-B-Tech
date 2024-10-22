using DTOsB.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModelsB.Authentication_and_Authorization_B;

namespace UserApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUserB> _userManager;
        private readonly SignInManager<ApplicationUserB> _signInManager;

        public AccountController(UserManager<ApplicationUserB> userManager, SignInManager<ApplicationUserB> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUserB
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    City = model.City,
                    Country = model.Country,
                    PostalCode = model.PostalCode,
                    UserType = model.UserType, // This captures Admin or User from the form
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (model.UserType == "User")
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Ok(new { message = "User registered successfully" });
                }

                return BadRequest(result.Errors);
            }

            return BadRequest(ModelState); // Return validation errors
        }





        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO model)
        {

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Ok(model);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (result.Succeeded)
            {
                if (user.UserType == "Admin")
                {
                    return RedirectToAction("Index", "Home");
                }

                else if (user.UserType == "User")
                {
                    return RedirectToAction("Index", "HomeSite"); // User is redirected to home site page
                }

                return RedirectToAction("Register", "User"); // Redirect to homepage after successful login
            }
            else if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "This account has been locked out due to multiple failed login attempts.");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }


            return Ok(model);
        }
    }


}
