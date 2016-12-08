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
    [Fact]
    public void Find_ReturnsCuisineById_true()
    {
      Cuisine newCuisine = new Cuisine("Cajun");
      newCuisine.Save();
      Cuisine testCuisine = Cuisine.Find(newCuisine.Id);
      Assert.Equal(newCuisine, testCuisine);
    }
    [Fact]
    public void GetRestaurants_ReturnsAllRestaurantsForCuisine_true()
    {
      Cuisine selectedCuisine = new Cuisine("Thai");
      List<Restaurant> tempRestaurants  = new List<Restaurant>{};

      Restaurant restaurant1 = new Restaurant("Thai Square", 0);
      restaurant1.Save();
      tempRestaurants.Add(restaurant1);
      Restaurant restaurant2 = new Restaurant("Chaba Thai", 0);
      restaurant2.Save();
      tempRestaurants.Add(restaurant2);

      List<Restaurant> cuisineRestaurants = selectedCuisine.GetRestaurants();
      Assert.Equal(tempRestaurants, cuisineRestaurants);
    }

    public void Dispose()
    {
      Cuisine.DeleteAll();
      Restaurant.DeleteAll();
    }
  }
}
