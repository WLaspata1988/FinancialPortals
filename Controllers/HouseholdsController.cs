using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FinancialPortals.Models;
using FinancialPortals.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FinancialPortals.Controllers
{
    public class HouseholdsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(
            new UserStore<ApplicationUser>(new ApplicationDbContext()));

        // GET: Households
        public ActionResult Dashboard()
        {
            var userId = User.Identity.GetUserId();
            var houseId = db.Users.AsNoTracking().FirstOrDefault(u => u.Id == userId).HouseholdId;
            var houseVM = new HouseholdViewModel();
            houseVM.Household = db.Households.Find(houseId);
            ViewBag.AccountTypeId = new SelectList(db.AccountTypes.ToList(), "Id", "Type");
            return View(houseVM);
        }

        // GET: Households/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // GET: Households/Create
        public ActionResult Create()
        {
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: Households/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<ActionResult> Create([Bind(Include = "Name,Greeting")] Household household)
        {
            

            if (ModelState.IsValid)
            {
                household.Established = DateTime.Now;
                db.Households.Add(household);
                db.SaveChanges();
                var user = db.Users.Find(User.Identity.GetUserId());
                user.HouseholdId = household.Id;
                db.SaveChanges();
                userManager.RemoveFromRole(user.Id, "Lobbyist");
                userManager.AddToRole(user.Id, "HeadOfHousehold");
                household.OwnerId = user.Id;
                household.Members.Add(user);
                db.SaveChanges();
                

                await Extensions.UserExtension.ReauthorizeUserAsync(user);
                return RedirectToAction("Dashboard", "Households");
            }            
            return View(household);
        }

        public ActionResult Setup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WizardSubmit(BankAccount bankAccount, BudgetCategory budgetCategory, BudgetItem budgetCategoryItem, int houseId, HouseholdViewModel model)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            if (ModelState.IsValid)            {

                bankAccount.HouseholdId = houseId;
                bankAccount.OwnerId = user.Id;                
                bankAccount.Created = DateTime.Now;
                db.BankAccounts.Add(bankAccount);
                db.SaveChanges();
                var household = db.Households.Find(houseId);
                budgetCategory.Household = household;
                budgetCategory.Created = DateTime.Now;
                db.BudgetCategories.Add(budgetCategory);
                db.SaveChanges();
                budgetCategoryItem.BudgetCategoryId = budgetCategory.Id;
                budgetCategoryItem.Created = DateTime.Now;
                db.BudgetItems.Add(budgetCategoryItem);
                db.SaveChanges();
                model.Household.IsConfigured = true;
                db.SaveChanges();

                
                
            }
            return RedirectToAction("Dashboard", new {user.Household.Id});
        }


        // GET: Households/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "FirstName", household.OwnerId);
            return View(household);
        }

        // POST: Households/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OwnerId,Name,Greeting,Established")] Household household)
        {
            if (ModelState.IsValid)
            {
                db.Entry(household).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "FirstName", household.OwnerId);
            return View(household);
        }

        // GET: Households/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Households/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Household household = db.Households.Find(id);
            var user = db.Users.Find(User.Identity.GetUserId());
            userManager.RemoveFromRole(user.Id, "HeadOfHousehold");
            userManager.AddToRole(user.Id, "Lobbyist");
            db.Households.Remove(household);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
