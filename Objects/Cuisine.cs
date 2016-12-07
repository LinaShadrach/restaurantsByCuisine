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
    // private List<string> TEMPLATE = new List<string> {};

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
