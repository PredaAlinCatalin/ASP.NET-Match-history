using DAWProject.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAWProject.Controllers
{
    public class DivisionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            List<Models.Division> divisions = db.Divisions.ToList();
            ViewBag.Divisions = divisions;
            return View();
        }

        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                //Models.Division division = db.Divisions.Include("User").Single(d => d.DivisionId == id);
                Models.Division division = db.Divisions.Single(d => d.DivisionId == id);
                if (division != null)
                {
                    return View(division);
                }
                return HttpNotFound("Couldn't find the division with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing division id parameter!");
        }

        [HttpGet]
        public ActionResult New()
        {
            Models.Division division = new Models.Division();
            division.UserId = User.Identity.GetUserId();
            return View(division);
        }

        [HttpPost]
        public ActionResult New(Models.Division divisionRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Divisions.Add(divisionRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(divisionRequest);
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return View(divisionRequest);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {

                Models.Division division = db.Divisions.Find(id);

                if (division == null)
                {
                    return HttpNotFound("Coludn't find the division with id " + id.ToString() + "!");
                }

                if (User.IsInRole("Player"))
                    if (User.Identity.GetUserId() != division.UserId)
                        return new HttpUnauthorizedResult("Unauthorized acces!");
                return View(division);
            }
            return HttpNotFound("Missing division id parameter!");
        }

        [HttpPut]
        public ActionResult Edit(int id, Models.Division divisionRequest)
        {

            Models.Division division = db.Divisions
                        .SingleOrDefault(b => b.DivisionId.Equals(id));

            try
            {
                if (ModelState.IsValid)
                {
                    if (TryUpdateModel(division))
                    {
                        division.Name = divisionRequest.Name;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(divisionRequest);
            }
            catch (Exception)
            {
                return View(divisionRequest);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Models.Division division = db.Divisions.Find(id);
            if (division != null)
            {
                if (User.IsInRole("Player"))
                    if (User.Identity.GetUserId() != division.UserId)
                        return new HttpUnauthorizedResult("Unauthorized acces!");
                db.Divisions.Remove(division);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Couldn't find the division with id " + id.ToString() + "!");
        }
    }
}