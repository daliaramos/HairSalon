using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using HairSalon;
using System;

namespace HairSalon.Models
{
    public class Client
    {
        private string _firstName;
        private string _lastName;
        private int _phoneNumber;
        private int _id;

        public Client(string firstName, string lastName, int phoneNumber, int id = 0)
        {
            _firstName = firstName;
            _lastName = lastName;
            _phoneNumber = phoneNumber;
            _id = id;
        }
        public override bool Equals(System.Object otherClient)
        {
            if (!(otherClient is Client))
            {
                return false;
            }
            else
            {
                Client newClient = (Client) otherClient;
                bool idEquality = (this.GetId() == newClient.GetId());
                bool nameEquality = (this.GetFirstName() == newClient.GetFirstName());
                bool lastNameEquality = (this.GetLastName() == newClient.GetLastName());
                bool phoneNumberEquality = (this.GetPhoneNumber() == newClient.GetPhoneNumber());
                return (idEquality && nameEquality && lastNameEquality && phoneNumberEquality);
            }
        }

        public override int GetHashCode()
        {
            return this.GetFirstName().GetHashCode();
        }

        public string GetFirstName()
        {
            return _firstName;
        }

        public string GetLastName()
        {
          return _lastName;
        }

        public int GetPhoneNumber()
        {
          return _phoneNumber;
        }

        public int GetId()
        {
            return _id;
        }
        public void SaveClients()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO clients (firstName, lastName, phoneNumber) VALUES (@ClientFirstName, @ClientLastName, @ClientPhoneNumber);";

            MySqlParameter firstName = new MySqlParameter();
            firstName.ParameterName = "@ClientFirstName";
            firstName.Value = this._firstName;

            MySqlParameter lastName = new MySqlParameter();
            lastName.ParameterName = "@ClientLastName";
            lastName.Value = this._lastName;

            MySqlParameter phoneNumber = new MySqlParameter();
            phoneNumber.ParameterName = "@ClientPhoneNumber";
            phoneNumber.Value = this._phoneNumber;

            cmd.Parameters.Add(firstName);
            cmd.Parameters.Add(lastName);
            cmd.Parameters.Add(phoneNumber);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

        }
        public static List<Client> GetAllClients()
        {
            List<Client> allClients = new List<Client> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM Clients;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {

              string ClientName = rdr.GetString(0);
              string ClientLastName = rdr.GetString(1);
              int ClientPhoneNumber = rdr.GetInt32(2);
              int ClientId = rdr.GetInt32(3);

              Client newClient = new Client(ClientName, ClientLastName, ClientPhoneNumber, ClientId);
              allClients.Add(newClient);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allClients;
        }
        public static Client Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM Clients WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            string ClientName = "";
            string ClientLastName = "";
            int ClientPhoneNumber = 0;
            int ClientId = 0;

            while(rdr.Read())
            {
              ClientName = rdr.GetString(0);
              ClientLastName = rdr.GetString(1);
              ClientPhoneNumber = rdr.GetInt32(2);
              ClientId = rdr.GetInt32(3);
            }
            Client newClient = new Client(ClientName, ClientLastName, ClientPhoneNumber, ClientId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newClient;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM Clients;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Stylist> GetStylists()
        {
            List<Stylist> allCategoryStylists = new List<Stylist> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM Stylists WHERE category_id = @category_id;";

            MySqlParameter categoryId = new MySqlParameter();
            categoryId.ParameterName = "@category_id";
            categoryId.Value = this._id;
            cmd.Parameters.Add(categoryId);


            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int StylistId = rdr.GetInt32(0);
              string StylistDescription = rdr.GetString(1);
              int StylistCategoryId = rdr.GetInt32(2);
              Stylist newStylist = new Stylist(StylistDescription, StylistCategoryId, StylistId);
              allCategoryStylists.Add(newStylist);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allCategoryStylists;
        }
    }
}
