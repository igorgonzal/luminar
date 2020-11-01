using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using scoredCard.Models;
using scoredCard.Repository;

namespace scoredCard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public IEnumerable<Scorecard> ScoredCard { get; set; }
        private readonly IScoreCardRepository _rep;

        public HomeController(ILogger<HomeController> logger, IScoreCardRepository rep)
        {
            _logger = logger;
            _rep = rep;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Buscando dados");
            ScoredCard = _rep.Listar();

            return View(ScoredCard);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
