using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodFinderWebApp.Data;
using FoodFinderWebApp.Models;

namespace FoodFinderWebApp.wwwroot
{
    public class SavedFoodLocationsController : Controller
    {
        private readonly FoodFinderWebAppContext _context;

        public SavedFoodLocationsController(FoodFinderWebAppContext context)
        {
            _context = context;
        }

        // GET: SavedFoodLocations
        public async Task<IActionResult> Index(string foodCategory, string searchString)
        {
            if (_context.SavedFoodLocation != null)
            {
                // Use LINQ to get list of genres.
                IQueryable<string> categoryQuery = from m in _context.SavedFoodLocation
                                                orderby m.Category
                                                select m.Category;

                var foodLocations = from f in _context.SavedFoodLocation
                                    select f;

                if (!string.IsNullOrEmpty(searchString))
                {
                    foodLocations = foodLocations.Where(s => s.Name!.Contains(searchString));
                }

                if (!string.IsNullOrEmpty(foodCategory))
                {
                    foodLocations = foodLocations.Where(x => x.Category == foodCategory);
                }

                var foodCategoryVM = new FoodLocationViewModel
                {
                    Categories = new SelectList(await categoryQuery.Distinct().ToListAsync()),
                    SavedFoodLocations = await foodLocations.ToListAsync()
                };

                return View(foodCategoryVM);
            }
            else
            {
                return Problem("Entity set 'FoodFinderWebAppContext.SavedFoodLocation'  is null.");
            }
        }

        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }

        // GET: SavedFoodLocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SavedFoodLocation == null)
            {
                return NotFound();
            }

            var savedFoodLocation = await _context.SavedFoodLocation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (savedFoodLocation == null)
            {
                return NotFound();
            }

            return View(savedFoodLocation);
        }

        // GET: SavedFoodLocations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SavedFoodLocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,DateAdded,Category")] SavedFoodLocation savedFoodLocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(savedFoodLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(savedFoodLocation);
        }

        // GET: SavedFoodLocations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SavedFoodLocation == null)
            {
                return NotFound();
            }

            var savedFoodLocation = await _context.SavedFoodLocation.FindAsync(id);
            if (savedFoodLocation == null)
            {
                return NotFound();
            }
            return View(savedFoodLocation);
        }

        // POST: SavedFoodLocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,DateAdded,Category")] SavedFoodLocation savedFoodLocation)
        {
            if (id != savedFoodLocation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(savedFoodLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SavedFoodLocationExists(savedFoodLocation.Id))
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
            return View(savedFoodLocation);
        }

        // GET: SavedFoodLocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SavedFoodLocation == null)
            {
                return NotFound();
            }

            var savedFoodLocation = await _context.SavedFoodLocation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (savedFoodLocation == null)
            {
                return NotFound();
            }

            return View(savedFoodLocation);
        }

        // POST: SavedFoodLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SavedFoodLocation == null)
            {
                return Problem("Entity set 'FoodFinderWebAppContext.SavedFoodLocation'  is null.");
            }
            var savedFoodLocation = await _context.SavedFoodLocation.FindAsync(id);
            if (savedFoodLocation != null)
            {
                _context.SavedFoodLocation.Remove(savedFoodLocation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SavedFoodLocationExists(int id)
        {
          return (_context.SavedFoodLocation?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
