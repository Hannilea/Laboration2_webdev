using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TownSquareAuth.Data;
using TownSquareAuth.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;




namespace TownSquareAuth.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var events = _context.Events.Where(e => e.ApplicationUserId == user.Id).ToList();
            return View(events);
        }

        
    }
}