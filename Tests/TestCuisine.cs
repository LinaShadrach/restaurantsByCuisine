using Xunit;
using System;
using System.Collections.Generic;
using BestRestaurants.Objects;
using System.Data;
using System.Data.SqlClient;

namespace  BestRestaurants
{
  public class CuisineTest : IDisposable
  {
    public CuisineTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_restaurants_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Equal_CuisinesAreTheSame_true()
    {
      Cuisine newCuisine = new Cuisine("Cajun");
      Cuisine otherCuisine = new Cuisine("Cajun");
      Assert.Equal(newCuisine, otherCuisine);
    }
    [Fact]
    public void GetAll_TableIsEmptyAtFirst()
    {
      List<Cuisine> allCuisines = Cuisine.GetAll();

      Assert.Equal(0, allCuisines.Count);
    }
    
    public void Dispose()
    {
      Cuisine.DeleteAll();
    }

  }
}
