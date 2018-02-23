using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Controllers
{
  public class HomeController : Controller
  {

    [Route("/")]
    public ActionResult Form()
    {
      return View("Index", "This is a message from the controller");
    }
  }
}
