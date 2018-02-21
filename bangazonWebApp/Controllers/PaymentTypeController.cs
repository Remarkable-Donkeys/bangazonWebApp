//author: Kristen Norris
//purpose: Add and Delete Payment Types
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

namespace bangazonWebApp.Controllers
{
    //ensures that user is authorized before being able to access the Payment Type page
    [Authorize]
    public class PaymentTypeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;

        public PaymentTypeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // This task retrieves the currently authenticated user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: PaymentTypes
        public async Task<IActionResult> Index()
        {
            //gets the current user
            ApplicationUser _user = await GetCurrentUserAsync();
            //only returns the payment types for the current user
            List<PaymentType> userPayments = await _context.PaymentType.Where(p => p.User == _user && p.Active == true).ToListAsync();
            return View(userPayments);

        }


        // GET: PaymentTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,AccountNumber")] PaymentType paymentType)
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
                ApplicationUser user = await GetCurrentUserAsync();
                paymentType.User = user;

                _context.Add(paymentType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paymentType);
        }


        // GET: PaymentTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentType = await _context.PaymentType
                .SingleOrDefaultAsync(m => m.Id == id);
            if (paymentType == null)
            {
                return NotFound();
            }

            return View(paymentType);
        }

        // POST: PaymentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //gets the list of completed orders that use the payment type the user wants to delete
            List<Order> paymentOrders = await _context.Order.Where(o => o.PaymentId == id && o.DateClosed != null).ToListAsync();
            // number of completed orders that use the payment type the user wants to delete
            int numCompleted = paymentOrders.Count();

            //payment type that user wants to delete
            PaymentType paymentType = await _context.PaymentType.SingleOrDefaultAsync(m => m.Id == id);


            if (numCompleted == 0)
            {
                //if there are no completed orders that use this payment type, delete it from the database
                _context.PaymentType.Remove(paymentType);
            } else
            {
                //if one or more completed orders have used this payment type, set the Active property to false
                paymentType.Active = false;

            }

            //save changes and return to index
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentTypeExists(int id)
        {
            return _context.PaymentType.Any(e => e.Id == id);
        }
    }
}
