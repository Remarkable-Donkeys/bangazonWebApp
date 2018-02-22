//author: Kristen Norris
//purpose: complete an order

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
using bangazonWebApp.Models.OrderViewModels;
using bangazonWebApp.Models.PaymentViewModels;
using Microsoft.AspNetCore.Authorization;

namespace bangazonWebApp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;

        // This task retrieves the currently authenticated user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Incomplete Order
        public async Task<IActionResult> Index()
        {
            //gets the current user
            ApplicationUser _user = await GetCurrentUserAsync();

            //only returns the user's incomplete order
            Order order = await _context.Order.SingleOrDefaultAsync(o => o.User ==_user && o.DateClosed ==null);
            if (order == null)
            {
                return View();
            }
     
            //gets list of orderproducts on the order and includes complete product data
            List<OrderProduct> orderedProducts = await _context.OrderProduct.Include("Product").Where(op => op.OrderId == order.Id).ToListAsync();

            //detailed order view model for imcomplete order
            OrderDetailViewModel details = new OrderDetailViewModel()
            {
                ProductList = orderedProducts,
                Order = order,
                OrderSum = orderedProducts.Where(op => op.Product.Status).Sum(op => op.Product.Price)
            };

            return View(details);
      
        }



        // GET: Order/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PaymentId,DateCreated,DateClosed")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            //gets the current user
            ApplicationUser _user = await GetCurrentUserAsync();
            
            //displays list of payment types
            PaymentTypeViewModel payList = new PaymentTypeViewModel(_context, _user);

            //PaymentTypeViewModel droplist = new SelectList(_context.Set<PaymentType>(), "PaymentTypeId", "Name");
            return View(payList);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PaymentType payment)
        {
            //gets the current user
            ApplicationUser _user = await GetCurrentUserAsync();
            //gets current order
            Order currentOrder = _context.Order.Single(o => o.Id == id);

            //adds payment to order
            currentOrder.PaymentId = payment.Id;
            //date closed will be the date the user adds a payment type
            currentOrder.DateClosed = DateTime.Now;


            //gets list of orderproducts on the order and includes complete product data
            List<OrderProduct> productsOnOrder = await _context.OrderProduct.Include("Product").Where(op => op.OrderId == currentOrder.Id).ToListAsync();
            //if product is no longer available, delete the OrderProduct relationship so that the product doesn't appear in the order history
            productsOnOrder.ForEach(op =>
            {
                if(op.Product.Status == false || op.Product.Quantity == 0)
                {
                    int pId = op.Product.Id;
                    var product =  _context.OrderProduct.Single(m => m.ProductId == pId);
                    _context.OrderProduct.Remove(product);
                }
            });

            if (currentOrder.PaymentId != null)
            {
                try
                {
                    _context.Update(currentOrder);
                    await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(currentOrder.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }



            //displays list of payment types
            PaymentTypeViewModel payList = new PaymentTypeViewModel(_context, _user);

            return View(payList);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.SingleOrDefaultAsync(m => m.Id == id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
