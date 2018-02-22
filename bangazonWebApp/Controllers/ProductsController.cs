using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using bangazonWebApp.Data;
using bangazonWebApp.Models;
using bangazonWebApp.Models.ProductViewModels;
using Microsoft.AspNetCore.Authorization;

namespace bangazonWebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;

        // This task retrieves the currently authenticated user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        public ProductsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Products - Contributed by Greg Turner
        public async Task<IActionResult> Index()
        {
            //gets the current user
            ApplicationUser _user = await GetCurrentUserAsync();
            //only returns the products for the current user that are not status false (inactive) and include the categories
            List<Product> userProducts = await _context.Product.Where(p => p.User == _user && p.Status).Include(p => p.Category).ToListAsync();

            return View(userProducts);
        }

        // GET: Products - Contributed by Greg Turner
        public async Task<IActionResult> Recent()
        {
            //only returns the 20 most recently added products that are not status false (inactive)
            List<Product> recent20 = await _context.Product.Where(p => p.Status).OrderByDescending(p => p.DateCreated).Take(20).ToListAsync();

            return View(recent20);
        }

        // GET: Products/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/ProductDetails/5
        public async Task<IActionResult> ProductDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductDetailViewModel model = new ProductDetailViewModel();

            model.Product = await _context.Product.Where(p => p.Id == id).Take(1).SingleOrDefaultAsync();            

            if (model.Product == null)
            {
                return NotFound();
            }

            Category category = await _context.Category.Where(c => c.Id == model.Product.CategoryId).Take(1).SingleOrDefaultAsync();
            model.CategoryName = category.CategoryType;

            if (model.CategoryName == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "CategoryType");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,CategoryId,Status,Price,DateCreated,Quantity,Photo,City,State,DeliverLocal")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "CategoryType", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "CategoryType", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CategoryId,Status,Price,DateCreated,Quantity,Photo,City,State,DeliverLocal")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "CategoryType", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5 - Contributed by Greg Turner
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //gets the list of orderproducts that contain the product the user wants to delete
            List<OrderProduct> orderProducts = await _context.OrderProduct.Where(op => op.ProductId == id).ToListAsync();
            // number of orderproducts that that contain the product the user wants to delete
            int numOrders = orderProducts.Count();

            //Product that user wants to delete
            Product product = await _context.Product.SingleOrDefaultAsync(p => p.Id == id);


            if (numOrders == 0)
            {
                //if there are no orderproducts that contain the product the user wants to delete, delete it from the database
                _context.Product.Remove(product);
            }
            else
            {
                //if one or more orderproducts that contain the product the user wants to delete, set the Status property to false
                product.Status = false;

            }

            //save changes and return to index
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Categories()
        {
            var model = new CategorizedProductsViewModel();

            model.CategorizedProducts = await (
                from c in _context.Category
                join p in _context.Product
                on c.Id equals p.CategoryId
                group new { c, p } by new { c.Id, c.CategoryType } into grouped
                select new CategorizedProducts
                {
                    CategoryId = grouped.Key.Id,
                    CategoryName = grouped.Key.CategoryType,
                    ProductCount = grouped.Select(x => x.p.Id).Count(),
                    Products = grouped.Select(x => x.p).Take(3)
                }).ToListAsync();
            return View(model);
        }
    }
}
