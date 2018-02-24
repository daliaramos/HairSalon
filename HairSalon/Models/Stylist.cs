using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using HairSalon;
using System;

namespace HairSalon.Models
{
  public class Stylist
  {
    private string _firstName;
    private string _lastName;
    private int _id;

    public Stylist(string firstName, string lastName, int Id = 0)
    {
      _firstName = firstName;
      _lastName = lastName;
      _id = Id;
    }

    public string GetFirstName()
    {
      return _firstName;
    }

    public string GetLastName()
    {
      return _lastName;
    }

    public int GetId()
    {
      return _id;
    }

    public static List<Stylist> GetAll()
        {
            List<Stylist> allStylist = new List<Stylist> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM Stylist;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              string StylistName = rdr.GetString(0);
              string StylistLastName = rdr.GetString(1);
              int StylistId = rdr.GetInt32(2);
              Stylist newStylist = new Stylist(StylistName, StylistLastName, StylistId);
              allStylist.Add(newStylist);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allStylist;
        }

        public static void DeleteAll()
         {
             MySqlConnection conn = DB.Connection();
             conn.Open();

             var cmd = conn.CreateCommand() as MySqlCommand;
             cmd.CommandText = @"DELETE FROM Stylist;";

             cmd.ExecuteNonQuery();

             conn.Close();
             if (conn != null)
             {
                 conn.Dispose();
             }
        }

        //
        public override bool Equals(System.Object otherStylist)
        {
            if (!(otherStylist is Stylist))
            {
              return false;
            }
            else
            {
              Stylist newStylist = (Stylist) otherStylist;
              bool idEquality = (this.GetId() == newStylist.GetId());
              bool nameEquality = (this.GetFirstName() == newStylist.GetFirstName());
              bool lastNameEquality = (this.GetLastName() == newStylist.GetLastName());
              return (idEquality && nameEquality && lastNameEquality);
            }
        }


        public void Save()
           {
             MySqlConnection conn = DB.Connection();
             conn.Open();

             var cmd = conn.CreateCommand() as MySqlCommand;
             cmd.CommandText = @"INSERT INTO `stylists` (`firts_name, last_Name`) VALUES (@FirstName, @LastName);";

             MySqlParameter name = new MySqlParameter();
             name.ParameterName = "@FirstName";
             name.Value = this._firstName;


             MySqlParameter lastName = new MySqlParameter();
             lastName.ParameterName = "@LastName";
             lastName.Value = this._lastName;

             cmd.Parameters.Add(name);
             cmd.Parameters.Add(lastName);

             cmd.ExecuteNonQuery();
             _id = (int) cmd.LastInsertedId;

              conn.Close();
              if (conn != null)
              {
                conn.Dispose();
              }
        }




  }
}
