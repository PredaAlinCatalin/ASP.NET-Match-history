using DAWProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAWProject.Controllers
{
    public class PlayerStatController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            List<Models.PlayerStat> playerStats = db.PlayersStat.ToList();
            ViewBag.PlayersStat = playerStats;
            return View();
        }

        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Models.PlayerStat playerStat = db.PlayersStat.Find(id);
                if (playerStat != null)
                {
                    return View(playerStat);
                }
                return HttpNotFound("Couldn't find the playerStat with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing playerStat id parameter!");
        }

        [HttpGet]
        public ActionResult New()
        {
            Models.PlayerStat playerStat = new Models.PlayerStat();
            return View(playerStat);
        }

        [HttpPost]
        public ActionResult New(Models.PlayerStat playerStatRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.PlayersStat.Add(playerStatRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(playerStatRequest);
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return View(playerStatRequest);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Models.PlayerStat playerStat = db.PlayersStat.Find(id);

                if (playerStat == null)
                {
                    return HttpNotFound("Coludn't find the playerStat with id " + id.ToString() + "!");
                }
                return View(playerStat);
            }
            return HttpNotFound("Missing playerStat id parameter!");
        }

        [HttpPut]
        public ActionResult Edit(int id, Models.PlayerStat playerStatRequest)
        {

            Models.PlayerStat playerStat = db.PlayersStat
                        .SingleOrDefault(b => b.PlayerStatId.Equals(id));

            try
            {
                if (ModelState.IsValid)
                {
                    if (TryUpdateModel(playerStat))
                    {
                        playerStat.NrWins = playerStatRequest.NrWins;
                        playerStat.NrLosses = playerStatRequest.NrLosses;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }

                return View(playerStatRequest);
            }
            catch (Exception)
            {

                return View(playerStatRequest);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Models.PlayerStat playerStat = db.PlayersStat.Find(id);
            if (playerStat != null)
            {
                db.PlayersStat.Remove(playerStat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Couldn't find the playerStat with id " + id.ToString() + "!");
        }
    }
}