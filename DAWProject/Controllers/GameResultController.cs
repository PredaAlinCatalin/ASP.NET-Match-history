using DAWProject.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAWProject.Controllers
{

    public class GameResultController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            List<Models.GameResult> gameResults = db.GamesResults.ToList();
            ViewBag.GamesResults = gameResults;
            return View();
        }

        [AllowAnonymous]
        public ActionResult Details(int? gameId, int? playerId)
        {
            if (gameId.HasValue && playerId.HasValue)
            {
                Models.GameResult gameResult = db.GamesResults.Find(gameId, playerId);
                if (gameResult != null)
                {
                    return View(gameResult);
                }
                return HttpNotFound("Couldn't find the gameResult with gameId " + gameId.ToString() + " and playerId " + playerId + "!");
            }
            return HttpNotFound("Missing gameResult id parameters!");
        }

        [HttpGet]
        public ActionResult New()
        {
            ViewBag.GameList = GetAllGames();
            ViewBag.PlayerList = GetAllPlayers();
            Models.GameResult gameResult = new Models.GameResult();
            gameResult.UserId = User.Identity.GetUserId();
            return View(gameResult);
        }

        [HttpPost]
        public ActionResult New(Models.GameResult gameResultRequest)
        {
            ViewBag.GameList = GetAllGames();
            ViewBag.PlayerList = GetAllPlayers();
            try
            {
                if (ModelState.IsValid)
                {
                    db.GamesResults.Add(gameResultRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(gameResultRequest);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ExceptionExtensions.GetFullMessage(ex);
                return View(gameResultRequest);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? gameId, int? playerId)
        {
            if (gameId.HasValue && playerId.HasValue)
            {
                Models.GameResult gameResult = db.GamesResults.Find(gameId, playerId);

                if (gameResult == null)
                {
                    return HttpNotFound("Couldn't find the gameResult with gameId " + gameId.ToString() + " and playerId " + playerId + "!");
                }

                if (User.IsInRole("Player"))
                    if (User.Identity.GetUserId() != gameResult.UserId)
                        return new HttpUnauthorizedResult("Unauthorized acces!");
                return View(gameResult);
            }
            return HttpNotFound("Missing gameResult id parameters!");
        }

        [HttpPut]
        public ActionResult Edit(int gameId, int playerId, Models.GameResult gameResultRequest)
        {
            Models.GameResult gameResult = db.GamesResults.Find(gameId, playerId);

            try
            {
                if (ModelState.IsValid)
                {
                    if (TryUpdateModel(gameResult))
                    {
                        gameResult.NrKills = gameResultRequest.NrKills;
                        gameResult.NrDeaths = gameResultRequest.NrDeaths;
                        gameResult.NrAssists = gameResultRequest.NrAssists;
                        gameResult.DamageDealt = gameResultRequest.DamageDealt;
                        gameResult.CreepScore = gameResultRequest.CreepScore;
                        db.SaveChanges();
                        
                    }
                    return RedirectToAction("Index");
                }
                return View(gameResultRequest);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ExceptionExtensions.GetFullMessage(ex);
                return View(gameResultRequest);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int gameId, int playerId)
        {
            Models.GameResult gameResult = db.GamesResults.Find(gameId, playerId);
            if (gameResult != null)
            {
                if (User.IsInRole("Player"))
                    if (User.Identity.GetUserId() != gameResult.UserId)
                        return new HttpUnauthorizedResult("Unauthorized acces!"); db.GamesResults.Remove(gameResult);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Couldn't find the gameResult with gameId " + gameId.ToString() + " and playerId " + playerId + "!");
        }

        [NonAction]
        public IEnumerable<SelectListItem> GetAllGames()
        {
            var selectList = new List<SelectListItem>();
            foreach (var game in db.Games.ToList())
            {
                selectList.Add(new SelectListItem
                {
                    Value = game.GameId.ToString(),
                    Text = game.GameId.ToString()
                });
            }
            return selectList;
        }

        [NonAction]
        public IEnumerable<SelectListItem> GetAllPlayers()
        {
            var selectList = new List<SelectListItem>();
            foreach (var player in db.Players.ToList())
            {
                selectList.Add(new SelectListItem
                {
                    Value = player.PlayerId.ToString(),
                    Text = player.Name
                });
            }
            return selectList;
        }
    }
    public static class ExceptionExtensions
    {
        public static string GetFullMessage(this Exception ex)
        {
            return ex.InnerException == null
                 ? ex.Message
                 : ex.Message + "->" + ex.InnerException.GetFullMessage();
        }
    }
}
