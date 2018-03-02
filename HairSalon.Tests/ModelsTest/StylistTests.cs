using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Tests
{
    [TestClass]
    public class StylistTests : IDisposable
    {
        public void Dispose()
        {
            Stylist.DeleteAll();
            Client.DeleteAll();
        }

        public StylistTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=hairsalon_test";
        }

        [TestMethod]
        public void Getters_GettersReturnAppropriately_StringsAndInts()
        {
            //arrange
            Stylist newStylist = new Stylist("cynthia", "Rodriguez", 1);
            string controlFirst = "cynthia";
            string controlLast = "Rodriguez";
            int controlId = 1;


            //act
            string resultFirst = newStylist.GetFirstName();
            string resultLast = newStylist.GetLastName();
            int resultId = newStylist.GetId();


            //assert
            Assert.AreEqual(controlFirst, resultFirst);
            Assert.AreEqual(controlLast, resultLast);
            Assert.AreEqual(controlId, resultId);

        }

        //update when Save method is created
        [TestMethod]
        public void DeleteAll_RemoveAllStylists_Void()
        {
            //arrange
            Stylist newStylist = new Stylist("cynthia", "Rodriguez");

            //act
            Stylist.DeleteAll();
            int result = Stylist.GetAllStylists().Count;

            //assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Find_ReturnAStylist_Stylist()
        {
            //arrange
            Stylist newStylist = new Stylist("cynthia", "Rodriguez");
            Stylist newStylist2 = new Stylist("paloma", "luis");
            newStylist.Save();
            newStylist2.Save();
            List<Stylist> allStylists = Stylist.GetAllStylists();

            //act
            Stylist result = Stylist.Find(newStylist.GetId());

            //assert
            Assert.AreEqual(newStylist, result);
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfFirstNameSame_Stylist()
        {
            //arrange, act
            Stylist firstStylist = new Stylist("cynthia", "Rodriguez", 1);
            Stylist secondStylist = new Stylist("cynthia", "Rodriguez", 1);

            //assert
            Assert.AreEqual(firstStylist, secondStylist);
        }

        [TestMethod]
        public void GetAllStylists_DBEmptyAtFirst_0()
        {
            //arrange, act
            int result = Stylist.GetAllStylists().Count;

            //assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Save_SavesToDatabase_StylistList()
        {
            //arrange
            Stylist newStylist = new Stylist("cynthia", "Rodriguez");

            //act
            newStylist.Save();
            List<Stylist> result = Stylist.GetAllStylists();
            Console.WriteLine(result.Count);
            List<Stylist> testList = new List<Stylist>{newStylist};
            Console.WriteLine(testList.Count);

            //assert
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Save_AssignsIdToObject_Id()
        {
            //arrange
            Stylist newStylist = new Stylist("cynthia", "Rodriguez");

            //act
            newStylist.Save();
            Stylist savedStylist = Stylist.GetAllStylists()[0];
            int result = savedStylist.GetId();
            int testId = newStylist.GetId();

            //assign
            Assert.AreEqual(result, testId);
        }

        [TestMethod]
        public void GetClients_GetClientsForThisStylist_ListClients()
        {
            //arrange
            Stylist newStylist = new Stylist("cynthia", "Rodriguez");
            newStylist.Save();
            Client newClient = new Client("steve", "stand", "503");
            newClient.SetStylistId(newStylist.GetId());
            newClient.Save();
            Client otherClient = new Client("stu", "keep", "503", 3, 4);
            otherClient.SetStylistId(newStylist.GetId());
            otherClient.Save();
            List<Client> controlList = new List<Client>{newClient, otherClient};

            //act
            List<Client> result = newStylist.GetClients();

            //assert
            CollectionAssert.AreEqual(result, controlList);
        }
    }
}
