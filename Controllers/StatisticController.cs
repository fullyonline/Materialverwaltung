using Materialverwaltung.Data;
using Materialverwaltung.Models;
using Microsoft.AspNetCore.Mvc;

namespace Materialverwaltung.Controllers
{
    public class StatisticController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StatisticController(ApplicationDbContext context)
        {
            _context= context;
        }

        public IActionResult Index()
        {
            if(_context == null || !_context.Materials.Any()) {
                return CustomerWarning();
            }

            var res = from material in _context.Materials
                      group material by material.MaterialgruppeId into materialGruppiert
                      select new {
                        Gruppe = materialGruppiert.First().Materialgruppe.Name,
                        Anzahl = materialGruppiert.Count()
                      };
            IDictionary<string, int> dictionary = new Dictionary<string, int>();
            foreach(var item in res)
            {
                dictionary.Add(item.Gruppe, item.Anzahl);
            }

            var viewModel = new StatistikViewModel(dictionary);


            return View(viewModel);
        }

        public IActionResult CustomerWarning()
        {
            ViewData["Warning"] = "Es muss mindestens eine Materialerfasst sein um eine Statistik darzustellen.";
            return View("_CustomerWarning");
        }
    }
}
