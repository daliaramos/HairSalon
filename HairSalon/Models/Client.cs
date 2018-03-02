using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

namespace HairSalon.Models
{
    public class Client
    {
        private string _firstName;
        private string _lastName;
        private string _phoneNumber;
        private int _id;
        private int _stylistId;

        public Client(string firstName, string lastName, string phoneNumber, int Id = 0, int stylistId = 0)
        {
            _firstName = firstName;
            _lastName = lastName;
            _phoneNumber = phoneNumber;
            _id = Id;
            _stylistId = stylistId;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients;";

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public string GetFirstName()
         {
           return _firstName;
         }

        public string GetLastName()
         {
           return _lastName;
         }

        public string GetPhoneNumber()
         {
           return _phoneNumber;
         }

        public int GetId()
         {
           return _id;
         }

        public int GetStylistId()
         {
           return _stylistId;
         }

        public void SetStylistId(int id)
        {
            _stylistId = id;
        }

        public static List<Client> GetAllClients()
        {
            List<Client> allClients = new List<Client>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              string clientFirstName = rdr.GetString(1);
              string clientLastName = rdr.GetString(2);
              string clientPhone = rdr.GetString(3);
              int clientId = rdr.GetInt32(0);
              int clientStylistId = rdr.GetInt32(4);
              Client newClient = new Client(clientFirstName, clientLastName, clientPhone, clientId, clientStylistId);
              allClients.Add(newClient);
            }
            conn.Close();
            if (conn != null)
            {
              conn.Dispose();
            }
            return allClients;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO `clients` (`first_name`, `last_name`, `phone_number`, `stylist_id`) VALUES (@FirstName, @LastName, @PhoneNumber, @StylistId);";

            MySqlParameter firstName = new MySqlParameter();
            firstName.ParameterName = "@FirstName";
            firstName.Value = this._firstName;

            MySqlParameter lastName = new MySqlParameter();
            lastName.ParameterName = "@LastName";
            lastName.Value = this._lastName;

            MySqlParameter phoneNumber = new MySqlParameter();
            phoneNumber.ParameterName = "@PhoneNumber";
            phoneNumber.Value = this._phoneNumber;

            MySqlParameter stylistId = new MySqlParameter();
            stylistId.ParameterName = "@StylistId";
            stylistId.Value = this._stylistId;

            cmd.Parameters.Add(firstName);
            cmd.Parameters.Add(lastName);
            cmd.Parameters.Add(phoneNumber);
            cmd.Parameters.Add(stylistId);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
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
                bool firstNameEquality = (this.GetFirstName() == newClient.GetFirstName());
                bool lastNameEquality = (this.GetLastName() == newClient.GetLastName());
                bool phoneEquality = (this.GetPhoneNumber() == newClient.GetPhoneNumber());
                bool stylistIdEquality = (this.GetStylistId() == newClient.GetStylistId());

                return (idEquality && firstNameEquality && lastNameEquality && phoneEquality && stylistIdEquality);
            }
        }

        public static Client Find(int id)
        {
            List<Client> allClients = Client.GetAllClients();
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * from `clients` WHERE id = @thisId;";

            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = id;
            cmd.Parameters.Add(thisId);

            string clientFirstName = "";
            string clientLastName = "";
            string clientPhoneNumber = "";
            int clientId = 0;
            int clientStylistId = 0;

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                clientFirstName = rdr.GetString(1);
                clientLastName = rdr.GetString(2);
                clientPhoneNumber = rdr.GetString(3);
                clientId = rdr.GetInt32(0);
                clientStylistId = rdr.GetInt32(4);
            }

            Client foundClient = new Client(clientFirstName, clientLastName, clientPhoneNumber, clientId, clientStylistId);

            conn.Close();
            if (conn != null)
            {
            conn.Dispose();
            }

            return foundClient;
        }
    }
}
