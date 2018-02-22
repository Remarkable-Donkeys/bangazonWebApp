﻿//Author: Max Wolf
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using bangazonWebApp.Data;
using bangazonWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace bangazonWebApp.Controllers
{
    public class ProductsController : Controller
    {

        private IHostingEnvironment _hostingEnvironment;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;

        // This task retrieves the currently authenticated user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public ProductsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = environment;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Product.Include(p => p.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Products/Details/5
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
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,CategoryId,Status,Price,DateCreated,Quantity,Photo,City,State,DeliverLocal")] Product product, IFormFile file)
        {
            // Remove the user from the model validation because it is
            // not information posted in the form
            ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                /*
                    If all other properties validation, then grab the 
                    currently authenticated user and assign it to the 
                    product before adding it to the db _context
                */
                var user = await GetCurrentUserAsync();

                product.User = user;

                // TODO: Add the user to the corresponding property of the product


                // Adds the img path to the product

                //specify the filepath
                var upload = Path.Combine(_hostingEnvironment.WebRootPath, "images");

                //store the relative filepath in the database for use as the src of img in view
                product.Photo = Path.Combine(
                    "images/",
                    file.FileName
                );

                if (file.Length > 0)
                    {
                    var filePath = Path.Combine(upload, file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
            

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
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CategoryId,Status,Price,DateCreated,Quantity,Photo,City,State,DeliverLocal")] Product product)
        {
            // Remove the user from the model validation because it is
            // not information posted in the form
            ModelState.Remove("User");

            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                /*
                   If all other properties validation, then grab the 
                   currently authenticated user and assign it to the 
                   product before adding it to the db _context
               */
                var user = await GetCurrentUserAsync();

                product.User = user;

                // TODO: Add the user to the corresponding property of the product

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

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.SingleOrDefaultAsync(m => m.Id == id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
