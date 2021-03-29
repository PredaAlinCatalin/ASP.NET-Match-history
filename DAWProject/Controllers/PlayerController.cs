using DAWProject.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAWProject.Controllers
{
    public class PlayerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            List<Models.Player> players = db.Players.ToList();
            ViewBag.Players = players;
            return View();
        }

        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Models.Player player = db.Players.Find(id);
                if (player != null)
                {
                    return View(player);
                }
                return HttpNotFound("Couldn't find the player with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing player id parameter!");
        }

        [HttpGet]
        public ActionResult New()
        {

            PlayerStatPlayerViewModel psp = new PlayerStatPlayerViewModel();
            psp.UserId = User.Identity.GetUserId();
            ViewBag.DivisionList = GetAllDivisions();
            return View(psp);
        }

        [HttpPost]
        public ActionResult New(PlayerStatPlayerViewModel pspViewModel)
        {
            ViewBag.DivisionList = GetAllDivisions();
            try
            {
                if (ModelState.IsValid)
                {
                    PlayerStat playerStat = new PlayerStat
                    {
                        NrWins = pspViewModel.NrWins,
                        NrLosses = pspViewModel.NrLosses,
                        UserId = pspViewModel.UserId
                    };
                    db.PlayersStat.Add(playerStat);

                    Player player = new Player {
                        Name = pspViewModel.Name,
                        PlayerStat = playerStat,
                        DivisionId = pspViewModel.DivisionId,
                        UserId = pspViewModel.UserId
                    };
                    db.Players.Add(player);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(pspViewModel);
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return View(pspViewModel);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            ViewBag.DivisionList = GetAllDivisions();
            if (id.HasValue)
            {
                Models.Player player = db.Players.Find(id);
                PlayerStat playerStat = db.PlayersStat.Find(player.PlayerStat.PlayerStatId);

                if (player == null)
                {
                    return HttpNotFound("Coludn't find the player with id " + id.ToString() + "!");
                }

                if (User.IsInRole("Player"))
                    if (User.Identity.GetUserId() != player.UserId)
                        return new HttpUnauthorizedResult("Unauthorized acces!");
                PlayerStatPlayerViewModel pspViewModel = new PlayerStatPlayerViewModel{
                    Name = player.Name,
                    NrWins = playerStat.NrWins,
                    NrLosses = playerStat.NrLosses,
                    DivisionId = player.DivisionId,
                };

                return View(pspViewModel);
            }
            return HttpNotFound("Missing player id parameter!");
        }

        [HttpPut]
        public ActionResult Edit(int id, Models.PlayerStatPlayerViewModel pspViewModel)
        {
            ViewBag.DivisionList = GetAllDivisions();
            Models.Player player = db.Players
                        .SingleOrDefault(b => b.PlayerId.Equals(id));
            PlayerStat playerStat = db.PlayersStat.Find(player.PlayerStat.PlayerStatId);

            try
            {
                if (ModelState.IsValid)
                {
                    if (TryUpdateModel(player) && TryUpdateModel(playerStat))
                    {
                        player.Name = pspViewModel.Name;
                        player.DivisionId = pspViewModel.DivisionId;
                        playerStat.NrWins = pspViewModel.NrWins;
                        playerStat.NrLosses = pspViewModel.NrLosses;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(pspViewModel);
            }
            catch (Exception)
            {
                return View(pspViewModel);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Models.Player player = db.Players.Find(id);
            PlayerStat playerStat = db.PlayersStat.Find(player.PlayerStat.PlayerStatId);


            if (player != null)
            {
                if (User.IsInRole("Player"))
                    if (User.Identity.GetUserId() != player.UserId)
                        return new HttpUnauthorizedResult("Unauthorized acces!"); db.Players.Remove(player);
                db.PlayersStat.Remove(playerStat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Couldn't find the player with id " + id.ToString() + "!");
        }

        [NonAction]
        public IEnumerable<SelectListItem> GetAllDivisions()
        {
            var selectList = new List<SelectListItem>();
            foreach (var division in db.Divisions.ToList())
            {
                selectList.Add(new SelectListItem
                {
                    Value = division.DivisionId.ToString(),
                    Text = division.Name
                });
            }
            return selectList;
        }
    }
}