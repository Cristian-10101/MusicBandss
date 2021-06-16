using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicBands.Data;
using MusicBands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicBands.Controllers
{
    public class MusicBandController : Controller
    {
        private readonly ApplicationDbContext db;
        
        public MusicBandController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.MusicBands.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var musicb = await db.MusicBands.FirstOrDefaultAsync(m => m.BandId == id);
            if (musicb == null)
            {
                return NotFound(); 
            }
            return View(musicb);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MusicBand musicBand)
        {
            if (ModelState.IsValid)
            {
                db.Add(musicBand);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(musicBand);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicb = await db.MusicBands.FindAsync(id);
            if (musicb == null)
            {
                return NotFound();
            }
            return View(musicb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MusicBand musicb )
        {
            if (id != musicb.BandId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(musicb);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(musicb);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var musicb = await db.MusicBands.FirstOrDefaultAsync(m => m.BandId == id);
            if (musicb == null)
            {
                return NotFound();
            }
            return View(musicb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var musicb = await db.MusicBands.FindAsync(id);
            db.MusicBands.Remove(musicb);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
