using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BestRestaurants.Objects
{
  public class Cuisine
  {
    // where TEMPLATE_OBJECTId references a property of the object
    public string Type {get; set;}
    public int Id {get; set;}

    public Cuisine(string Type, int id = 0)
    {
      this.Type = Type;
      this.Id = id;
    }
    public override bool Equals(System.Object otherCuisine)
    {
      if (!(otherCuisine is Cuisine))
      {
        return false;
      }
      else
      {
        Cuisine newCuisine = (Cuisine) otherCuisine;
        bool typeEquality = (this.Type == newCuisine.Type);

        return (typeEquality);
      }
    }
    public override int GetHashCode()
    {
         return this.Type.GetHashCode();
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO cuisines(type) OUTPUT INSERTED.id VALUES (@CuisineType);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@CuisineType";
      nameParameter.Value = this.Type;

      cmd.Parameters.Add(nameParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this.Id = rdr.GetInt32(0);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }
    public static List<Cuisine> GetAll()
    {
      List<Cuisine> allCuisines = new List<Cuisine>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * from cuisines;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int CuisineId = rdr.GetInt32(0);
        string CuisineType = rdr.GetString(1);
        Cuisine newCuisine = new Cuisine(CuisineType, CuisineId);
        allCuisines.Add(newCuisine);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return allCuisines;
    }
    public static Cuisine Find(int cuisineId)
    {
      SqlConnection conn = DB.Connection();
      conn. Open();

      SqlCommand cmd = new SqlCommand("SELECT * from cuisines WHERE id = @CuisineId;", conn);
      SqlParameter cuisineIdParameter = new SqlParameter();
      cuisineIdParameter.ParameterName = "@CuisineId";
      cuisineIdParameter.Value = cuisineId;

      cmd.Parameters.Add(cuisineIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      int foundCuisineId = 0;
      string foundCuisineType = null;

      while(rdr.Read())
      {
        foundCuisineId = rdr.GetInt32(0);
        foundCuisineType = rdr.GetString(1);
      }
      Cuisine foundCuisine = new Cuisine(foundCuisineType, foundCuisineId);

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return foundCuisine;
    }
    public List<Restaurant> GetRestaurants()
    {
      List<Restaurant> selectedRestaurants = new List<Restaurant>{};

      SqlConnection conn = DB.Connection();
      conn. Open();

      SqlCommand cmd = new SqlCommand("SELECT * from restaurants WHERE cuisine_id = @CuisineId;", conn);
      SqlParameter cuisineIdParameter = new SqlParameter();
      cuisineIdParameter.ParameterName = "@CuisineId";
      cuisineIdParameter.Value = this.Id;

      cmd.Parameters.Add(cuisineIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      int foundRestaurantId = 0;
      string foundRestaurantName = null;
      int foundRestaurantCuisineId = 0;

      while(rdr.Read())
      {
        foundRestaurantId = rdr.GetInt32(0);
        foundRestaurantName = rdr.GetString(1);
        foundRestaurantCuisineId = rdr.GetInt32(2);
        Restaurant newRestaurant = new Restaurant(foundRestaurantName, foundRestaurantCuisineId, foundRestaurantId);
        selectedRestaurants.Add(newRestaurant);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return selectedRestaurants;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM cuisines;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
