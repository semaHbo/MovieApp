using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieApp.Web.Data;
using MovieApp.Web.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MoviesController(ApplicationDbContext context)
            => _context = context;

        // GET /Movies → /Movies/List
        public IActionResult Index()
            => RedirectToAction(nameof(List));

        // GET /Movies/List?id=2&q=arama
        public async Task<IActionResult> List(int? id, string q)
        {
            // 1) DbContext üzerinden sorgu oluştur
            var query = _context.Movies.AsQueryable();

            // 2) Genre filtreleme (opsiyonel)
            if (id.HasValue)
                query = query.Where(m => m.GenreId == id.Value);

            // 3) Arama filtreleme (opsiyonel)
            if (!string.IsNullOrWhiteSpace(q))
            {
                var lower = q.ToLower();
                query = query.Where(m =>
                    m.Title.ToLower().Contains(lower) ||
                    m.Description.ToLower().Contains(lower));
            }

            // 4) Veritabanından çek
            var movies = await query.ToListAsync();

            // 5) Arama terimini ViewBag’e koy
            ViewBag.SearchQuery = q;

            // 6) ViewModel’e aktar
            var model = new MoviesViewModel { Movies = movies };

            // 7) “Movies.cshtml” view’ını döndür
            return View("Movies", model);
        }

        // GET /Movies/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return NotFound();
            return View(movie);
        }

        // GET /Movies/Create
        [HttpGet]
        public IActionResult Create()
        {
            // Tür listesini hala GenreRepository üzerinden getiriyoruz
            ViewBag.GenreList = new SelectList(GenreRepository.Genres, "GenreId", "Name");
            return View();
        }

        // POST /Movies/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie m)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.GenreList = new SelectList(GenreRepository.Genres, "GenreId", "Name");
                return View(m);
            }

            _context.Movies.Add(m);
            await _context.SaveChangesAsync();
            TempData["Message"] = $"{m.Title} isimli film eklendi.";
            return RedirectToAction(nameof(List));
        }

        // GET /Movies/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return NotFound();

            ViewBag.GenreList = new SelectList(GenreRepository.Genres, "GenreId", "Name", movie.GenreId);
            return View(movie);
        }

        // POST /Movies/Edit
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Movie m)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.GenreList = new SelectList(GenreRepository.Genres, "GenreId", "Name", m.GenreId);
                return View(m);
            }

            _context.Movies.Update(m);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = m.MovieId });
        }

        // GET /Movies/Delete?MovieId=5&Title=Foo
        [HttpGet]
         public async Task<IActionResult> Delete(int MovieId, string Title)
        {
            var movie = await _context.Movies.FindAsync(MovieId);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"{Title} isimli film silindi.";
            }
            return RedirectToAction(nameof(List));
        }

        // GET /Movies/Point/5
        [HttpGet]
        public async Task<IActionResult> Point(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return NotFound();

            return View(movie);
        }

        // POST /Movies/Point
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Point(Movie m)
        {
            var movie = await _context.Movies.FindAsync(m.MovieId);
            if (movie != null)
            {
                movie.Point = m.Point;
                await _context.SaveChangesAsync();
                TempData["Message"] = $"{movie.Title} puanı güncellendi.";
            }
            return RedirectToAction(nameof(List));
        }
    }
}
