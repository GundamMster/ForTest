using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModels.Game;

namespace WebApplication1.Controllers
{
    public class GameController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Game
        public ActionResult Index(Guid id)
        {
            var game = _db.Games.FirstOrDefault(i => i.Id == id);
            return View(game);
        }
        public ActionResult Create(string id)
        {
            if (ModelState.IsValid)
            {
                var newgame = new Game()
                {
                    Id = Guid.NewGuid(),
                    Grid = new string('_', 9),
                    Player2Id = id,
                    Player1Id = _db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).Id
                };
                _db.Games.Add(newgame);
                _db.SaveChanges();
                return RedirectToAction("Index", new { Id = newgame.Id });
            }
            return View();
        }
        public ActionResult Step(int cellNum, Guid gameId)
        {
            var game = _db.Games.FirstOrDefault(i => i.Id == gameId);
            var UserId = _db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).Id;
            if (game.Steps % 2 == 1)
            {
                game.Grid = game.Grid.Remove(cellNum, 1);
                game.Grid = game.Grid.Insert(cellNum, "X");
            }
            else
            {
                game.Grid = game.Grid.Remove(cellNum, 1);
                game.Grid = game.Grid.Insert(cellNum, "O");
            }
            _db.SaveChanges();
            return RedirectToAction("#");
        }
    }
}