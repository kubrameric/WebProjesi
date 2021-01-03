using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProje.Data;
using WebProje.Models;

namespace WebProje.Controllers
{
    public class IlanController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public IlanController(ApplicationDbContext context,IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Ilan
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Ilanlar.Include(i => i.hayvan);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Ilan/Details/5
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

        // GET: Ilan/Create
        public IActionResult Create()
        {
            ViewData["hayvanId"] = new SelectList(_context.Hayvanlar, "hayvanId", "hayvanId");
            return View();
        }

        // POST: Ilan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ilanID,ilanNo,hayvanId,resimDosyasi,ilanTarihi")] Ilanlar ilanlar)
        {
            if (ModelState.IsValid)
            {
                // Save image to wwwroot/ilanResimleri
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(ilanlar.resimDosyasi.FileName);
                string extansion = Path.GetExtension(ilanlar.resimDosyasi.FileName);
                ilanlar.resim = fileName = fileName + DateTime.Now.ToString("yyyymmddssfff") + extansion;
                string path = Path.Combine(wwwRootPath + "/ilanResimleri/", fileName);
                
                using (var fileStream = new FileStream(path,FileMode.Create))
                {
                    await ilanlar.resimDosyasi.CopyToAsync(fileStream);
                }
                // insert Record
                ilanlar.hayvanId = Convert.ToInt32(TempData["hayvanID"].ToString());
                _context.Add(ilanlar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["hayvanId"] = new SelectList(_context.Hayvanlar, "hayvanId", "hayvanId", ilanlar.hayvanId);
            return View(ilanlar);
        }

        // GET: Ilan/Edit/5
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

        // POST: Ilan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ilanID,ilanNo,hayvanId,resimDosyasi,ilanTarihi")] Ilanlar ilanlar)
        {
            if (id != ilanlar.ilanID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "ilanResimleri", ilanlar.resim);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    // Save image to wwwroot/ilanResimleri
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(ilanlar.resimDosyasi.FileName);
                    string extansion = Path.GetExtension(ilanlar.resimDosyasi.FileName);
                    ilanlar.resim = fileName = fileName + DateTime.Now.ToString("yyyymmddssfff") + extansion;
                    string path = Path.Combine(wwwRootPath + "/ilanResimleri/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await ilanlar.resimDosyasi.CopyToAsync(fileStream);
                    }


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

        // GET: Ilan/Delete/5
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

        // POST: Ilan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ilanlar = await _context.Ilanlar.FindAsync(id);

            // delete image from wwwroot/ilanResimleri
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "ilanResimleri", ilanlar.resim);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            
            // delete the record
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
