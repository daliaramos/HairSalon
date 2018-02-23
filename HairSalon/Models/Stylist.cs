using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using HairSalon;
using System;

namespace HairSalon.Models
{
  public class Stylist
  {
    private string _name;
    private string _lastName;
    private int _id;

    public Stylist(string name, string lastName, int Id = 0)
    {
      _name = name;
      _lastName = lastName;
      _id = Id;
    }

    public string GetName()
    {
      return _name;
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

        public void Save()
           {
             MySqlConnection conn = DB.Connection();
             conn.Open();

             var cmd = conn.CreateCommand() as MySqlCommand;
             cmd.CommandText = @"INSERT INTO `stylist` (`first_name, first_lastName`) VALUES (@firstName, @lastName);";

             MySqlParameter name = new MySqlParameter();
             name.ParameterName = "@firstName";
             name.Value = this._name;
             cmd.Parameters.Add(name);

             MySqlParameter lastName = new MySqlParameter();
             lastName.ParameterName = "@lastName";
             lastName.Value = this._lastName;
             cmd.Parameters.Add(lastName);

             cmd.ExecuteNonQuery();
             _id = (int) cmd.LastInsertedId;

              conn.Close();
              if (conn != null)
              {
                  conn.Dispose();
              }
         }













      //
      // public override bool Equals(System.Object otherItem)
      // {
      //     if (!(otherItem is Item))
      //     {
      //       return false;
      //     }
      //     else
      //     {
      //       Item newItem = (Item) otherItem;
      //       bool idEquality = (this.GetId() == newItem.GetId());
      //       bool descriptionEquality = (this.GetDescription() == newItem.GetDescription());
      //       return (idEquality && descriptionEquality);
      //   }

  }
}
