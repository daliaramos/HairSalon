using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Tests
{
    [TestClass]
    public class ClientTests : IDisposable
    {
        public void Dispose()
        {
            Stylist.DeleteAll();
            Client.DeleteAll();
        }

        public ClientTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=hairsalon_test";
        }

        [TestMethod]
        public void Getters_GettersReturnAppropriately_StringsAndInts()
        {
            //arrange
            Client newClient = new Client("cynthia", "Rodriguez", "503", 1, 1);
            string controlFirst = "cynthia";
            string controlLast = "Rodriguez";
            string controlPhone = "503";
            int controlId = 1;
            int controlStylistId = 1;

            //act
            string resultFirst = newClient.GetFirstName();
            string resultLast = newClient.GetLastName();
            string resultPhone = newClient.GetPhoneNumber();
            int resultId = newClient.GetId();
            int resultStylistId = newClient.GetStylistId();

            //assert
            Assert.AreEqual(controlFirst, resultFirst);
            Assert.AreEqual(controlLast, resultLast);
            Assert.AreEqual(controlPhone, resultPhone);
            Assert.AreEqual(controlId, resultId);
            Assert.AreEqual(controlStylistId, resultStylistId);
        }

        [TestMethod]
        public void GetAllClients_DBEmptyAtFirst_0()
        {
            //arrange, act
            int result = Client.GetAllClients().Count;

            //assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Save_SavesToDatabase_ClientList()
        {
            //arrange
            Client newClient = new Client("cynthia", "Rodriguez", "503");

            //act
            newClient.Save();
            List<Client> result = Client.GetAllClients();
            List<Client> testList = new List<Client>{newClient};
            string testName = result[0].GetFirstName();
            //assert
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void SetStylistId_ReturnCorrectStylistId_StylistId()
        {
            //arrange
            Client newClient = new Client("cynthia", "Rodriguez", "503", 1);
            Stylist newStylist = new Stylist("Carol", "Smith", 5);
            int controlId = 5;

            //act
            newClient.SetStylistId(newStylist.GetId());
            int result = newClient.GetStylistId();

            //assert
            Assert.AreEqual(result, controlId);
        }

        [TestMethod]
        public void Save_AssignsIdToObject_Id()
        {
            //arrange
            Client newClient = new Client("cynthia", "Rodriguez", "503");

            //act
            newClient.Save();
            Client savedClient = Client.GetAllClients()[0];
            int result = savedClient.GetId();
            int testId = newClient.GetId();

            //assign
            Assert.AreEqual(result, testId);
        }

        public void Equals_ReturnsTrueIfSame_Client()
        {
            //arrange, act
            Client firstClient = new Client("cynthia", "Rodriguez", "503", 1, 1);
            Client secondClient = new Client("cynthia", "Rodriguez", "503", 1, 1);

            //assert
            Assert.AreEqual(firstClient, secondClient);
        }

        [TestMethod]
        public void Find_FindAClient_Client()
        {
            //arrange
            Client controlClient = new Client("cynthia", "Rodriguez", "503");
            controlClient.Save();

            //act
            Client foundClient = Client.Find(controlClient.GetId());

            //Assert
            Assert.AreEqual(foundClient, controlClient);
        }
    }
}
