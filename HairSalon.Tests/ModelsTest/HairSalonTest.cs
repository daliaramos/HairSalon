using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Models.Tests
{
  [TestClass]
  public class StylistTests : IDisposable
    {
        public void Dispose()
        {
            Stylist.DeleteAll();
        }

        public StylistTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=HairSalon;";
        }


        [TestMethod]
        public void Getters_JustATest()
        {
          Stylist newStylist = new Stylist("cynthia", "smith", 1);
          string name = "cynthia";
          string lastName = "smith";
          int Id = 1;

          string nameresult = newStylist.GetFirstName();
          string lastnameresult = newStylist.GetLastName();
          int Idresult = newStylist.GetId();

          Assert.AreEqual(name, nameresult);
          Assert.AreEqual(lastName, lastnameresult);
          Assert.AreEqual(Id, Idresult);
        }

        [TestMethod]
        public void DeleteAll_DeleteStylist()
        {
          Stylist newStylist = new Stylist("cynthia", "smith", 1);

          Stylist.DeleteAll();

          int result = Stylist.GetAll().Count;

          Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Item()
        {
          // Arrange, Act
          Stylist firstStylist = new Stylist("cynthia", "smith");
          Stylist secondStylist = new Stylist("cynthia", "smith");

          // Assert
          Assert.AreEqual(firstStylist, secondStylist);
        }


        [TestMethod]
        public void Save_SavesToDatabase_ItemList()
        {
          //Arrange
          Stylist testStylist = new Stylist("cynthia", "Smith");

          //Act
          testStylist.Save();
          List<Stylist> result = Stylist.GetAll();
          List<Stylist> testList = new List<Stylist>{testStylist};

          //Assert
          CollectionAssert.AreEqual(testList, result);
        }










  }
}
