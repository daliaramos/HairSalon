using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Controllers
{
  public class StylistController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
        List<Stylist> allStylists = Stylist.GetAll();
        return View("Index", allStylists);
    }

    [HttpGet("/Stylists/new")]
      public ActionResult CreateForm()
      {
          return View();
      }

      [HttpPost("/Stylists")]
      public ActionResult Create()
      {
          Stylist newStylist = new Stylist(Request.Form["StylistFirstName"], Request.Form["StylistLastName"]);
          List<Stylist> allStylists = Stylist.GetAll();

          return View("Index", allStylists);
      }

      // [HttpGet("/Stylists/{id}")]
      // public ActionResult Details(int id)
      // {
      //    Stylist Stylist = Stylist.Find(id);
      //    return View(Stylist);
      // }
  }
}
