using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TownSquareAuth.Data;
using TownSquareAuth.Models;
using System.Threading.Tasks;

namespace TownSquareAuth.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //get: events
        public async Task<IActionResult> Index()
        {
            return View(await _context.Events.ToListAsync());
        }

        //get: events/details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var ev = await _context.Events.FirstOrDefaultAsync(e => e.Id ==id);
            if (ev == null) return NotFound();
            return View(ev);
        }

        //get: events/create
        public IActionResult Create()
        {
            return View();
        }

        //post: events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event ev)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ev);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ev);
        }

        //get: events/edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return NotFound();
            return View(ev);
        }

        //post: events/edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Event ev)
        {
            if (id !=ev.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(ev);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ev);
        }

        //get: event/delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var ev = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
            if (ev == null) return NotFound();
            return View(ev);
        }

        //post: event/delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}