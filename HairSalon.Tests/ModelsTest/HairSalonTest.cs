using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;

namespace HairSalon.Models.Tests
{
  [TestClass]
  public class HairSalonTest
  {
    [TestMethod]
    public void GetName_JustATest()
    {
      Stylist newStylist = new Stylist("cynthia", "smith", 1);
      string name = "cynthia";
      string lastName = "smith";
      int Id = 1;

      string nameresult = newStylist.GetName();
      string lastnameresult = newStylist.GetLastName();
      int Idresult = newStylist.GetId();

      Assert.AreEqual(name, nameresult);
      Assert.AreEqual(lastName, lastnameresult);
      Assert.AreEqual(Id, Idresult);
    }
  }
}
