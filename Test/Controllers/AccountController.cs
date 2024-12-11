using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DiaSymReader;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Test.Models;

namespace Test.Controllers
{
    public class AccountController : Controller
    {
        private readonly DBContext context;

        public AccountController(DBContext context)
        {
            ;
            this.context = context;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserDetail userDetail)
        {
            context.userDetails.Add(userDetail);
            context.SaveChanges();
            return RedirectToAction("LoginPage", "Account");
        }
        public ActionResult LoginPage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginPage(LoginPage login)
        {
            var user = context.userDetails.Where(U => U.Password == login.Password && U.Username == login.Username);
            if (user != null)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid username or password.");
            return View(login);
        }
        public ActionResult Displayusers()
        {
            ViewBag.TotalUsers = context.userDetails.Count();
            ViewBag.ActiveUsers = context.userDetails.Count(u => u.Status == true);
            ViewBag.InactiveUsers = context.userDetails.Count(u => u.Status == false);
            return View(context.userDetails);
        }
        public ActionResult Edit(int Id)
        {
            return View(context.userDetails.Find(Id));
        }
        [HttpPost]       
        public ActionResult Update(UserDetail userDetail)
        {
            var user = context.userDetails.Find(userDetail.Id);
            if (user != null)
            {
                user.Id = userDetail.Id;
                user.FirstName = userDetail.FirstName;
                user.MiddleName = userDetail.MiddleName;
                user.LastName = userDetail.LastName;
                user.Address = userDetail.Address;
                user.PhoneNumber = userDetail.PhoneNumber;
                user.Email = userDetail.Email;
                user.City = userDetail.City;
                user.State = userDetail.State;
                user.PinCode = userDetail.PinCode;
                 user.Username = userDetail.Username;
                user.Status = userDetail.Status;          
            }
            context.SaveChanges();
            return RedirectToAction("Displayusers", "Account");
        }
        public ViewResult Delete(int Id)
        {
            var user = context.userDetails.Find(Id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Delete(UserDetail userDetail)
        {
            var user = context.userDetails.FirstOrDefault(u => u.Id == userDetail.Id);
            user.Status=false;
            context.SaveChanges();
            return RedirectToAction("Displayusers","Account");
        }
        public IActionResult Dashboard()
        {
            ViewBag.TotalUsers = context.userDetails.Count();
            ViewBag.ActiveUsers = context.userDetails.Count(u => u.Status == true);
            ViewBag.InactiveUsers = context.userDetails.Count(u => u.Status == false);
            return View();
        }
    }
}
