using DAWProject.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAWProject.Controllers
{
    public class GameController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Games
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            List<Game> games = db.Games.ToList();
            ViewBag.Games = games;
            return View();
        }

        // GET: Games/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Game game = db.Games.Include("User").Single(b => b.GameId == id);

                if (game != null)
                {
                    return View(game);
                }
                return HttpNotFound("Couldn't find the game with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing game id parameter!");
        }

        [HttpGet]
        public ActionResult New()
        {
            Game game = new Game();
            game.UserId = User.Identity.GetUserId();

            game.AttributesList = GetAllAttributes();
            game.Attributes = new List<Models.Attribute>();
            return View(game);
        }

        [HttpPost]
        public ActionResult New(Game gameRequest)
        {
            var selectedAttributes = gameRequest.AttributesList.Where(b => b.Checked).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    gameRequest.Attributes = new List<Models.Attribute>();
                    for (int i = 0; i < selectedAttributes.Count(); i++)
                    {
                        Models.Attribute attribute = db.Attributes.Find(selectedAttributes[i].Id);
                        gameRequest.Attributes.Add(attribute);
                    }
                    db.Games.Add(gameRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(gameRequest);
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return View(gameRequest);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Game game = db.Games.Find(id);
                game.AttributesList = GetAllAttributes();

                foreach (Models.Attribute checkedAttribute in game.Attributes)
                {   
                    game.AttributesList.FirstOrDefault(g => g.Id == checkedAttribute.AttributeId).Checked = true;
                }
                if (game == null)
                {
                    return HttpNotFound("Coludn't find the game with id " + id.ToString() + "!");
                }

                if (User.IsInRole("Player"))
                    if (User.Identity.GetUserId() != game.UserId)
                        return new HttpUnauthorizedResult("Unauthorized acces!");
                return View(game);
            }
            return HttpNotFound("Missing game id parameter!");
        }

        [HttpPut]
        public ActionResult Edit(int id, Game gameRequest)
        {

            Game game = db.Games
                        .SingleOrDefault(b => b.GameId.Equals(id));

            var selectedAttributes = gameRequest.AttributesList.Where(b => b.Checked).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    if (TryUpdateModel(game))
                    {
                        game.Duration = gameRequest.Duration;
                        game.Day = gameRequest.Day;
                        game.Month = gameRequest.Month;
                        game.Year = gameRequest.Year;
    
                        game.Attributes.Clear();
                        game.Attributes = new List<Models.Attribute>();

                        for (int i = 0; i < selectedAttributes.Count(); i++)
                        {
                            Models.Attribute attribute = db.Attributes.Find(selectedAttributes[i].Id);
                            game.Attributes.Add(attribute);
                        }
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                return View(gameRequest);
            }
            catch (Exception)
            {
                return View(gameRequest);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Game game = db.Games.Find(id);
            if (game != null)
            {
                if (User.IsInRole("Player"))
                    if (User.Identity.GetUserId() != game.UserId)
                        return new HttpUnauthorizedResult("Unauthorized acces!"); db.Games.Remove(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Couldn't find the game with id " + id.ToString() + "!");
        }

        [NonAction]
        public List<CheckBoxViewModel> GetAllAttributes()
        {
            var checkboxList = new List<CheckBoxViewModel>();
            foreach (var attribute in db.Attributes.ToList())
            {
                checkboxList.Add(new CheckBoxViewModel
                {
                    Id = attribute.AttributeId,
                    Name = attribute.Name,
                    Checked = false
                });
            }
            return checkboxList;
        }
    }

}