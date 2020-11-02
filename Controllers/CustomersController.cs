using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        //Database Methods

        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }



        //Route Methods

        public ViewResult Index()
        { 
            return View();
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var customer = new Customer();

            var formCustomerViewModel = new FormCustomerViewModel(customer)
            {
                MembershipTypes = membershipTypes,
            };

            return View("CustomerForm", formCustomerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var formCustomerViewModel = new FormCustomerViewModel(customer)
                {
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("CustomerForm", formCustomerViewModel);
            }

            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var custInDb = _context.Customers.Single(c => c.Id == customer.Id);

                custInDb.Name = customer.Name;
                custInDb.BirthDate = customer.BirthDate;
                custInDb.MemberShipTypeId = customer.MemberShipTypeId;
                custInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var formCustomerViewModel = new FormCustomerViewModel(customer)
            {
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", formCustomerViewModel);
        }
    }
}