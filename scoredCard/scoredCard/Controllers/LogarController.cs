using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using scoredCard.Context;
using scoredCard.Models;
using scoredCard.Repository;
using scoredCard.ViewModels;

namespace scoredCard.Controllers
{
    public class LogarController : Controller
    {
        private readonly UserContext _context;
        public IEnumerable<Scorecard> ScoredCard { get; set; }
        private readonly IScoreCardRepository _rep;

        public LogarController(UserContext context, IScoreCardRepository rep)
        {
            _context = context;
            _rep = rep;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Acessar([Bind("Id,Login,Password")] User user)
        {

            if (ModelState.IsValid)
            {
                if (!UserExists(user))
                {
                    return View("Views/Login/View.cshtml");
                }
                else
                {
                    ScoredCard = _rep.ListarDashBoard();
                    return View("Views/Dashboard/Index.cshtml", ScoredCard.First());
                }
            }
            return View("Views/Login/View.cshtml");

        }

        private bool UserExists(User user)
        {
            return _context.Users.Any(e => e.Login == user.Login && e.Password == user.Password);
        }

        public IActionResult Login()
        {
            return View("Views/Login/View.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
