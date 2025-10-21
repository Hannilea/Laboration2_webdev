using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TownSquareAuth.Data;
using TownSquareAuth.Models;

namespace TownSquareAuth.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        // Visa alla användare
        public async Task<IActionResult> Users()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var roles = await _userManager.GetRolesAsync(currentUser);

            var users = await _db.Users
                .Where(u => u.Id != currentUser.Id) // Exkludera den inloggade adminanvändaren
                .ToListAsync();
            return View(users);
        }

        //Ta bort användare
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            //Hitta användaren
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {

                ModelState.AddModelError("", "User not found.");
                return RedirectToAction("Users");

            }
            else
            {

                // Hämta användarens event från databasen
                var userEvents = await _db.Events
                    .Where(e => e.ApplicationUserId == user.Id)
                    .ToListAsync();

                foreach (var ev in userEvents)
                {
                    ev.ApplicationUserId = null; // Koppla bort användaren från eventet

                }

                var userRSVPs = await _db.EventRSVPs
                    .Where(r => r.ApplicationUserId == user.Id)
                    .ToListAsync();
                _db.EventRSVPs.RemoveRange(userRSVPs);

                //Spara andringar i databasen
                await _db.SaveChangesAsync();

                // Ta bort användaren
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {


                    ModelState.AddModelError("", "Error deleting user.");
                    return RedirectToAction("Users");
                }
                return RedirectToAction("Users");
            }
        }

        public async Task<IActionResult> Events()
        {
            var events = await _db.Events
                .Include(e => e.ApplicationUser)
                .ToListAsync();
            
            
            return View(events);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var ev = await _db.Events
                .Include(e => e.RSVPs) 
                .FirstOrDefaultAsync(e => e.Id == id);

            if (ev == null) return NotFound();

            _db.EventRSVPs.RemoveRange(ev.RSVPs);

            _db.Events.Remove(ev);

            await _db.SaveChangesAsync();

            return RedirectToAction("Events");
        }
        
         
        
    }
}