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
    public class IlanlarYeniController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IlanlarYeniController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IlanlarYeni
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Ilanlar.Include(i => i.hayvan);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: IlanlarYeni/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilanlar = await _context.Ilanlar
                .Include(i => i.hayvan)
                .FirstOrDefaultAsync(m => m.ilanID == id);
            if (ilanlar == null)
            {
                return NotFound();
            }

            return View(ilanlar);
        }

        // GET: IlanlarYeni/Create
        public IActionResult Create()
        {
            ViewData["hayvanId"] = new SelectList(_context.Hayvanlar, "hayvanId", "hayvanId");
            return View();
        }

        // POST: IlanlarYeni/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ilanID,ilanNo,hayvanId,ilanTarihi")] Ilanlar ilanlar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ilanlar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["hayvanId"] = new SelectList(_context.Hayvanlar, "hayvanId", "hayvanId", ilanlar.hayvanId);
            return View(ilanlar);
        }

        // GET: IlanlarYeni/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilanlar = await _context.Ilanlar.FindAsync(id);
            if (ilanlar == null)
            {
                return NotFound();
            }
            ViewData["hayvanId"] = new SelectList(_context.Hayvanlar, "hayvanId", "hayvanId", ilanlar.hayvanId);
            return View(ilanlar);
        }

        // POST: IlanlarYeni/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ilanID,ilanNo,hayvanId,ilanTarihi")] Ilanlar ilanlar)
        {
            if (id != ilanlar.ilanID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ilanlar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IlanlarExists(ilanlar.ilanID))
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
            ViewData["hayvanId"] = new SelectList(_context.Hayvanlar, "hayvanId", "hayvanId", ilanlar.hayvanId);
            return View(ilanlar);
        }

        // GET: IlanlarYeni/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilanlar = await _context.Ilanlar
                .Include(i => i.hayvan)
                .FirstOrDefaultAsync(m => m.ilanID == id);
            if (ilanlar == null)
            {
                return NotFound();
            }

            return View(ilanlar);
        }

        // POST: IlanlarYeni/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ilanlar = await _context.Ilanlar.FindAsync(id);
            _context.Ilanlar.Remove(ilanlar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IlanlarExists(int id)
        {
            return _context.Ilanlar.Any(e => e.ilanID == id);
        }
    }
}
