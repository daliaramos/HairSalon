using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Models.Tests
{
  [TestClass]
  public class ClientTests : IDisposable
  {
        public ClientTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=hairsalon;";
        }

        public void Dispose()
        {
          // Item.DeleteAll();
          Client.DeleteAll();
        }

       [TestMethod]
       public void GetAllClients_CategoriesEmptyAtFirst_0()
       {
         Client newClient = new Client("sam", "smith", 503753, 1);
         string name = "sam";
         string lastName = "smith";
         int phoneNumber = 503753;
         int Id = 1;
         //Arrange, Act
         string nameresult = newClient.GetFirstName();
         string lastnameresult = newClient.GetLastName();
         int phoneNumberresult = newClient.GetPhoneNumber();
         int Idresult = newClient.GetId();

         //Assert
         Assert.AreEqual(name, nameresult);
         Assert.AreEqual(lastName, lastnameresult);
         Assert.AreEqual(phoneNumber, phoneNumberresult);
         Assert.AreEqual(Idresult, Idresult);
       }

      [TestMethod]
      public void Equals_ReturnsTrueForSameName_Client()
      {
        //Arrange, Act
        Client firstClient = new Client("samantha", "smith", 503753);
        Client secondClient = new Client("samantha", "smith", 503753);

        //Assert
        Assert.AreEqual(firstClient, secondClient);
      }

      [TestMethod]
      public void SaveClients_SavesToDatabase()
      {
        //Arrange
        Client testClient = new Client("cynthia", "Smith", 503753);

        //Act
        testClient.SaveClients();
        List<Client> result = Client.GetAllClients();
        List<Client> testList = new List<Client>{testClient};

        //Assert
        CollectionAssert.AreEqual(testList, result);
      }

     [TestMethod]
     public void SaveClients_DatabaseAssignsIdToClient_Id()
     {
       //Arrange
       Client testClient = new Client("sam", "smith", 503753);
       testClient.SaveClients();

       //Act
       Client savedClient = Client.GetAllClients()[0];

       int result = savedClient.GetId();
       int testId = testClient.GetId();

       //Assert
       Assert.AreEqual(testId, result);
    }
// //
//
    [TestMethod]
    public void Find_FindsClientInDatabase_Client()
    {
      //Arrange
      Client testClient = new Client("samantha", "smith", 503753, 1);
      testClient.SaveClients();

      //Act
      Client foundClient = Client.Find(testClient.GetId());

      //Assert
      Assert.AreEqual(testClient, foundClient);
    }
//
//     [TestMethod]
//     public void GetItems_RetrievesAllItemsWithClient_ItemList()
//     {
//       Client testClient = new Client("Household chores");
//       testClient.Save();
//
//       Item firstItem = new Item("Mow the lawn", testClient.GetId());
//       firstItem.Save();
//       Item secondItem = new Item("Do the dishes", testClient.GetId());
//       secondItem.Save();
//
//
//       List<Item> testItemList = new List<Item> {firstItem, secondItem};
//       List<Item> resultItemList = testClient.GetItems();
//
//       CollectionAssert.AreEqual(testItemList, resultItemList);
// }


  }
}
