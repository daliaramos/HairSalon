using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class StylistsController : Controller
    {
        [Route("/")]
        public ActionResult Index()
        {
            List<Stylist> allStylists = Stylist.GetAllStylists();
            return View("Index", allStylists);
        }

        [HttpGet("/stylists/new")]
        public ActionResult CreateFormS()
        {
            return View();
        }

        [HttpPost("/stylists")]
        public ActionResult CreateNewStylist()
        {
            Stylist newStylist = new Stylist(Request.Form["firstName"], Request.Form["lastName"]);
            newStylist.Save();
            return RedirectToAction("Index");
        }

        [HttpGet("/stylists/{id}")]
        public ActionResult Details(int id)
        {
            Stylist foundStylist = Stylist.Find(id);
            List<Client> clients = foundStylist.GetClients();
            Dictionary<string, object> model = new Dictionary<string,object>();
            model.Add("stylist", foundStylist);
            model.Add("clients", clients);

            return View("Details", model);
        }

        [HttpPost("/stylists/{id}/clients")]
        public ActionResult CreateNewClient(int id)
        {
            Client newClient = new Client(Request.Form["firstName"], Request.Form["lastName"], Request.Form["phoneNumber"]);
            newClient.SetStylistId(id);
            newClient.Save();
            return RedirectToAction("Details");
        }
    }
}
