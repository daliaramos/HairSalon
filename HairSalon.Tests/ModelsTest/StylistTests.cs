using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Models.Tests
{
  [TestClass]
  public class StylistTests
  : IDisposable
    {
        public StylistTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=hairsalon_test;";
        }

        public void Dispose()
        {
            Stylist.DeleteAll();
            Client.DeleteAll();
        }


        [TestMethod]
        public void Getters_JustATest()
        {
          Stylist newStylist = new Stylist("cynthia", "smith", 1, 1);
          string name = "cynthia";
          string lastName = "smith";
          int Id = 1;
          int Client = 1;

          string nameresult = newStylist.GetFirstName();
          string lastnameresult = newStylist.GetLastName();
          int Idresult = newStylist.GetId();
          int ClientResult = newStylist.GetClient();

          Assert.AreEqual(name, nameresult);
          Assert.AreEqual(lastName, lastnameresult);
          Assert.AreEqual(Id, Idresult);
          Assert.AreEqual(Client, ClientResult);
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
        public void Equals_ReturnsTrueIfStylistAreTheSame_Stylist()
        {
          // Arrange, Act
          Stylist firstStylist = new Stylist("cynthia", "smith", 1);
          Stylist secondStylist = new Stylist("cynthia", "smith", 1);

          // Assert
          Assert.AreEqual(firstStylist, secondStylist);
        }


        [TestMethod]
        public void Save_SavesToDatabase_Stylist()
        {
          //Arrange
          Stylist testStylist = new Stylist("cynthia", "Smith", 1);

          //Act
          testStylist.Save();
          List<Stylist> result = Stylist.GetAll();
          List<Stylist> testList = new List<Stylist>{testStylist};

          //Assert
          CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Save_DatabaseAssignsIdToObject_Id()
        {
          //Arrange
          Stylist testStylist = new Stylist("cynthia","smith", 1);
          testStylist.Save();

          //Act
          Stylist savedStylist = Stylist.GetAll()[0];

          int result = savedStylist.GetId();
          int testId = testStylist.GetId();

          //Assert
          Assert.AreEqual(testId, result);
        }

          [TestMethod]
          public void Find_FindsStylistInDatabase_Item()
          {
            //Arrange
            Stylist testStylist = new Stylist("Cynthia", "smith", 1);
            testStylist.Save();

            //Act
            Stylist foundStylist = Stylist.Find(testStylist.GetId());

            //Assert
            Assert.AreEqual(testStylist, foundStylist);
          }
  }
}
