using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TownSquareAuth.Data;
using TownSquareAuth.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Text.Json;

namespace TownSquareAuth.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpClientFactory _httpClientFactory;


        public EventsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _userManager = userManager;
            _httpClientFactory = httpClientFactory;

        }

        //get: events
        [AllowAnonymous]
        public async Task<IActionResult> Index(string sortOrder)
        {
            var events = _context.Events
                                 .Include(e => e.RSVPs)
                                 .AsQueryable();

            switch(sortOrder)
            {
                case "date_asc":
                    events = events.OrderBy(e => e.Date);
                    break;
                case "date_desc":
                    events = events.OrderByDescending(e => e.Date);
                    break;
                case "attending_asc":
                    events = events.OrderBy(e => e.RSVPs.Count);
                    break;
                case "attending_desc":
                    events = events.OrderByDescending(e => e.RSVPs.Count);
                    break;
            }

            return View(await events.ToListAsync());
        }

        [AllowAnonymous]
        //get: events/details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var ev = await _context.Events
                            .Include(e => e.RSVPs)
                            .ThenInclude(r => r.ApplicationUser)
                            .FirstOrDefaultAsync(e => e.Id == id);
            if (ev == null) return NotFound();

            ViewBag.EventImage = $"https://picsum.photos/seed/{ev.Id}/600/400";


            return View(ev);
        }

        //get: events/create
        public IActionResult Create()
        {
            return View();
        }

        //post: events/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event ev)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                ev.ApplicationUserId = user.Id;

                _context.Add(ev);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MyEvents));
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }

            return View(ev);
        }
        
        //get: events/edit
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return NotFound();

            var user = await _userManager.GetUserAsync(User);
            if (ev.ApplicationUserId != user.Id)
                return Forbid(); 

            return View(ev);
        }

        //post: events/edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Event ev)
        {
            var dbEvent = await _context.Events.FindAsync(id);
            if (dbEvent == null) return NotFound();

            var user = await _userManager.GetUserAsync(User);
            if (dbEvent.ApplicationUserId != user.Id)
                return Forbid(); 

            if (ModelState.IsValid)
            {
                dbEvent.Title = ev.Title;
                dbEvent.Description = ev.Description;
                dbEvent.Date = ev.Date;
                // update other fields if needed

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MyEvents));
            }

            return View(ev);
        }

        //get: event/delete
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return NotFound();

            var user = await _userManager.GetUserAsync(User);
            if (ev.ApplicationUserId != user.Id)
                return Forbid();

            return View(ev);
        }

        // POST: Events/Delete
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return NotFound();

            var user = await _userManager.GetUserAsync(User);
            if (ev.ApplicationUserId != user.Id)
                return Forbid();

            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MyEvents));
        }

        // RSVP
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RSVP(int eventId, bool isAttending)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var ev = await _context.Events
                        .Include(e => e.RSVPs)
                        .ThenInclude(r => r.ApplicationUser)
                        .FirstOrDefaultAsync(e => e.Id == eventId);

            if (ev == null) return NotFound();

            if (user.Id == ev.ApplicationUserId)
                return Forbid();

            var existingRSVP = ev.RSVPs.FirstOrDefault(r => r.ApplicationUserId == user.Id);

            if (existingRSVP != null)
            {
                // Uppdatera RSVP (toggle)
                existingRSVP.IsAttending = isAttending;

                // Om användaren avbryter RSVP, ta bort notifikationen
                if (!isAttending)
                {
                    ev.Notifications.RemoveAll(n => n.Contains(user.UserName));
                }
            }
            else if (isAttending)
            {
                ev.RSVPs.Add(new EventRSVP
                {
                    ApplicationUserId = user.Id,
                    EventId = ev.Id,
                    IsAttending = true
                });

                // Notification till eventägaren
                ev.Notifications ??= new List<string>();
                ev.Notifications.Add($"{user.UserName} RSVPed to your event '{ev.Title}'");
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = eventId });
        }




        [Authorize]
        public async Task<IActionResult> MyEvents()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            // Hämta alla events som skapats av den inloggade användaren
            var myEvents = await _context.Events
                .Where(e => e.ApplicationUserId == user.Id)
                .ToListAsync();

            return View(myEvents);
        }
        
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClearNotifications()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            // Hämta alla events som användaren skapat
            var events = await _context.Events
                .Where(e => e.ApplicationUserId == user.Id)
                .ToListAsync();

            foreach (var ev in events)
            {
                ev.Notifications?.Clear();
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MyEvents));
        }


    }
}