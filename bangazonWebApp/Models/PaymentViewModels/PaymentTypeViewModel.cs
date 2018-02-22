using bangazonWebApp.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bangazonWebApp.Models.PaymentViewModels
{
    public class PaymentTypeViewModel
    {

        public List<SelectListItem> PaymentList { get; set; }
        public PaymentType PaymentType { get; set; }

        public PaymentTypeViewModel(ApplicationDbContext ctx, ApplicationUser usr)
        {

            this.PaymentList = ctx.PaymentType
                                    .Where(p => p.User == usr && p.Active)
                                    .AsEnumerable()
                                    .Select(li => new SelectListItem
                                    {
                                        Text = li.Name + "- " + li.AccountNumber,
                                        Value = li.Id.ToString()
                                    }).ToList();

            this.PaymentList.Insert(0, new SelectListItem
            {
                Text = "Choose payment...",
                Value = "0"
            });
        }
    }
}
