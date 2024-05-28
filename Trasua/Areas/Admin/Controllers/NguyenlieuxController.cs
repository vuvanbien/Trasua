using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trasua.Models;
using X.PagedList;

namespace Trasua.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NguyenlieuxController : Controller
    {
        private readonly TrasuaContext _context;

        public NguyenlieuxController(TrasuaContext context)
        {
            _context = context;
        }

        // GET: Admin/Nguyenlieux
        public async Task<IActionResult> Index(string nl, int page = 1, int pageSize = 5)
        {
            var query = _context.Nguyenlieus.ToList();

            if (!string.IsNullOrEmpty(nl))
                query = query.Where(h => h.TenNl.Contains(nl, StringComparison.OrdinalIgnoreCase)).ToList();

            var totalItemCount = query.Count();
            var model = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var pagedList = new StaticPagedList<Nguyenlieu>(model, page, pageSize, totalItemCount);
            ViewBag.PageStartItem = (page - 1) * pageSize + 1;
            ViewBag.PageEndItem = Math.Min(page * pageSize, totalItemCount);
            ViewBag.TotalItemCount = totalItemCount;
            ViewBag.Page = page;
            ViewBag.nl = nl;
            return View(pagedList);
        }

        // GET: Admin/Nguyenlieux/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Nguyenlieus == null)
            {
                return NotFound();
            }

            var nguyenlieu = await _context.Nguyenlieus
                .FirstOrDefaultAsync(m => m.MaNl == id);
            if (nguyenlieu == null)
            {
                return NotFound();
            }

            return View(nguyenlieu);
        }
        private int GenerateUniqueMaNl()
        {
            Random random = new Random();
            int newMaNl;
            do
            {
                newMaNl = random.Next(1, int.MaxValue);
            } while (_context.Nguyenlieus.Any(n => n.MaNl == newMaNl));
            return newMaNl;
        }
        // GET: Admin/Nguyenlieux/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Nguyenlieux/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenNl,Soluong,Dvt")] Nguyenlieu nguyenlieu)
        {
            if (ModelState.IsValid)
            {
                nguyenlieu.MaNl = GenerateUniqueMaNl();
                _context.Add(nguyenlieu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nguyenlieu);
        }
        // GET: Admin/Nguyenlieux/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Nguyenlieus == null)
            {
                return NotFound();
            }

            var nguyenlieu = await _context.Nguyenlieus.FindAsync(id);
            if (nguyenlieu == null)
            {
                return NotFound();
            }
            return View(nguyenlieu);
        }

        // POST: Admin/Nguyenlieux/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaNl,TenNl,Soluong,Dvt")] Nguyenlieu nguyenlieu)
        {
            if (id != nguyenlieu.MaNl)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nguyenlieu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NguyenlieuExists(nguyenlieu.MaNl))
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
            return View(nguyenlieu);
        }

        // GET: Admin/Nguyenlieux/Delete/5
        public ActionResult Delete(int MaNl)
        {

            Nguyenlieu nguyenlieu = _context.Nguyenlieus.Single(ma => ma.MaNl == MaNl);
            if (nguyenlieu == null)
            {
                return NotFound();
            }

            _context.Nguyenlieus.Remove(nguyenlieu);
            _context.SaveChanges();
            return RedirectToAction("Index", "Nguyenlieux");
        }

        private bool NguyenlieuExists(int id)
        {
          return (_context.Nguyenlieus?.Any(e => e.MaNl == id)).GetValueOrDefault();
        }
    }
}
