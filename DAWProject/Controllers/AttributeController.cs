using DAWProject.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAWProject.Controllers
{
    public class AttributeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            List<Models.Attribute> attributes = db.Attributes.ToList();
            ViewBag.Attributes = attributes;
            return View();
        }

        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Models.Attribute attribute = db.Attributes.Find(id);
                if (attribute != null)
                {
                    return View(attribute);
                }
                return HttpNotFound("Couldn't find the attribute with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing attribute id parameter!");
        }

        [HttpGet]
        public ActionResult New()
        {
            Models.Attribute attribute = new Models.Attribute();
            attribute.UserId = User.Identity.GetUserId();
            return View(attribute);
        }

        [HttpPost]
        public ActionResult New(Models.Attribute attributeRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Attributes.Add(attributeRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(attributeRequest);
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return View(attributeRequest);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Models.Attribute attribute = db.Attributes.Find(id);

                if (attribute == null)
                {
                    return HttpNotFound("Coludn't find the attribute with id " + id.ToString() + "!");
                }

                if (User.IsInRole("Player"))
                    if (User.Identity.GetUserId() != attribute.UserId)
                        return new HttpUnauthorizedResult("Unauthorized acces!");
                return View(attribute);
            }
            return HttpNotFound("Missing attribute id parameter!");
        }

        [HttpPut]
        public ActionResult Edit(int id, Models.Attribute attributeRequest)
        {

            Models.Attribute attribute = db.Attributes
                        .SingleOrDefault(b => b.AttributeId.Equals(id));

            try
            {
                if (ModelState.IsValid)
                {
                    if (TryUpdateModel(attribute))
                    {
                        attribute.Name = attributeRequest.Name;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(attributeRequest);
            }
            catch (Exception)
            {
                return View(attributeRequest);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Models.Attribute attribute = db.Attributes.Find(id);

            if (attribute != null)
            {
                if (User.IsInRole("Player"))
                    if (User.Identity.GetUserId() != attribute.UserId)
                        return new HttpUnauthorizedResult("Unauthorized acces!");

                db.Attributes.Remove(attribute);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Couldn't find the attribute with id " + id.ToString() + "!");
        }

    }
}