using Xunit;
using System;
using System.Collections.Generic;
using BestRestaurants.Objects;
using System.Data;
using System.Data.SqlClient;

namespace  BestRestaurants
{
  public class RestaurantTest : IDisposable
  {
    public RestaurantTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_restaurants_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void GetAll_DatabaseIsInitiallyEmpty_true()
    {
      //Arrange
      List<Restaurant> RestaurantList = Restaurant.GetAll();
      //Act
      //Assert
      Assert.Equal(true, RestaurantList.Count == 0);
    }
    [Fact]
    public void Equal_SameRestaurantsAreEqual_true()
    {
      //Arrange
      Restaurant newRestaurant = new Restaurant("Ana Purna");
      Restaurant newRestaurant2 = new Restaurant("Ana Purna");

      //Act
      //Assert
      Assert.Equal(newRestaurant, newRestaurant2);
    }
    [Fact]
    public void Save_SavesRestaurantToTable_true()
    {
      //Arrange
      Restaurant newRestaurant = new Restaurant("Ana Purna", 0);
      //Act
      newRestaurant.Save();
      List<Restaurant> RestaurantList = Restaurant.GetAll();
      //Assert
      Assert.Equal( RestaurantList[0], newRestaurant);
    }
    [Fact]
    public void Edit_EditRestaurantProperties_true()
    {
      Restaurant newRestaurant = new Restaurant("Ana Purna", 0);
      newRestaurant.Save();
      newRestaurant.Edit("Benson Bar", 1);
      List<Restaurant> RestaurantList = Restaurant.GetAll();
      Restaurant compareRestaurant = new Restaurant("Benson Bar", 1);

      Assert.Equal(compareRestaurant, RestaurantList[0]);
    }

    public void Dispose()
    {
      Restaurant.DeleteAll();
    }

  }
}
