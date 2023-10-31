using Microsoft.AspNetCore.Mvc;
using ReDoMusic.Domain.Entities;
using ReDoMusic.Persistence.Contexts;

namespace ReDoMusic.MVC.Controllers
{
    public class BrandController : Controller
    {
        private readonly ReDoMusicDbContext _context;

        public BrandController()
        {
            _context = new();
        }
        [HttpGet]
        public IActionResult Index()
        {
            var brands = _context.Brands.ToList();
            return View(brands);
        }
        [HttpGet]
        public IActionResult AddBrand()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBrand(string brandName, string brandDescription, string brandAdress)
        {
            var brand = new Brand()
            {
                Id = Guid.NewGuid(),
                Name = brandName,
                Description = brandDescription,
                Adress = brandAdress,
                CreatedOn = DateTime.UtcNow
            };
            _context.Brands.Add(brand);
            _context.SaveChanges();
            return View();
        }

        [HttpGet]
        public IActionResult DeleteBrand()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteBrand(string id)
        {
            var brand = _context.Brands.Where(x => x.Id == Guid.Parse(id)).FirstOrDefault();
            _context.Brands.Remove(brand);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
