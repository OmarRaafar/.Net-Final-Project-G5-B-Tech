using AdminDashboardB.Models;
using DTOsB.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using ModelsB.Authentication_and_Authorization_B;

namespace AdminDashboardB.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<ApplicationUserB> _userManager;
        private SignInManager<ApplicationUserB> _signInManager;

        public AdminController(UserManager<ApplicationUserB> userManager, SignInManager<ApplicationUserB> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO model)
        {

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
            if (user.UserType == "Admin")
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError(string.Empty, "This account has been locked out due to multiple failed login attempts.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    }
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    return View();
                }
            }

            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Admin");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return View("users", users);
        }

        public IActionResult LockUnlock(string? id)
        {
            return RedirectToAction();
        }

        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View("GetOne", user);
        }

        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            return View(user); // Return user for view
        }

        [HttpGet("{id}/edit")]
        public async Task<IActionResult> UpdateUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View("Update", user); // Return user for editing
        }

        [HttpPost("{id}/edit")]
        public async Task<IActionResult> UpdateUser(ApplicationUserB user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByIdAsync(user.Id);
                if (existingUser == null)
                {
                    return NotFound();
                }

                // Update user properties
                existingUser.UserName = user.UserName;
                existingUser.Email = user.Email;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.Address = user.Address;
                existingUser.City = user.City;
                existingUser.Country = user.Country;
                existingUser.PostalCode = user.PostalCode;
                existingUser.UserType = user.UserType;

                var result = await _userManager.UpdateAsync(existingUser);
                if (result.Succeeded)
                {
                    return RedirectToAction("GetAllUsers"); // Redirect to user list after update
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(user);
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("DeleteUser")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("GetAllUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return RedirectToAction("GetAllUsers");
        }


        public IActionResult CreateUser()
        {
            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterVM model)
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
                    UserType = model.UserType, 
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (model.UserType == "Admin")
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("GetAllUsers", "Admin"); 
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model); 
        }

    }
}
