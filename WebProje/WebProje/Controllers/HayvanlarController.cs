using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProje.Data;
using WebProje.Models;

namespace WebProje.Controllers
{
    public class HayvanlarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HayvanlarController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hayvanlars
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hayvanlar.ToListAsync());
        }

        // GET: Hayvanlars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hayvanlar = await _context.Hayvanlar
                .FirstOrDefaultAsync(m => m.hayvanId == id);
            if (hayvanlar == null)
            {
                return NotFound();
            }

            return View(hayvanlar);
        }

        // GET: Hayvanlars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hayvanlars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("hayvanId,hayvanTuruId,hayvanYasi,hayvanCinsi,hayvanCinsiyeti")] Hayvanlar hayvanlar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hayvanlar);
                await _context.SaveChangesAsync();
                TempData["hayvanID"] = hayvanlar.hayvanId;  
                return RedirectToAction("Create","Ilan");
            }
            return View(hayvanlar);
        }

        // GET: Hayvanlars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hayvanlar = await _context.Hayvanlar.FindAsync(id);
            if (hayvanlar == null)
            {
                return NotFound();
            }
            return View(hayvanlar);
        }

        // POST: Hayvanlars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("hayvanId,hayvanTuruId,hayvanYasi,hayvanCinsi,hayvanCinsiyeti")] Hayvanlar hayvanlar)
        {
            if (id != hayvanlar.hayvanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hayvanlar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HayvanlarExists(hayvanlar.hayvanId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(hayvanlar);
        }

        // GET: Hayvanlars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hayvanlar = await _context.Hayvanlar
                .FirstOrDefaultAsync(m => m.hayvanId == id);
            if (hayvanlar == null)
            {
                return NotFound();
            }

            return View(hayvanlar);
        }

        // POST: Hayvanlars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hayvanlar = await _context.Hayvanlar.FindAsync(id);
            _context.Hayvanlar.Remove(hayvanlar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HayvanlarExists(int id)
        {
            return _context.Hayvanlar.Any(e => e.hayvanId == id);
        }
    }
}
