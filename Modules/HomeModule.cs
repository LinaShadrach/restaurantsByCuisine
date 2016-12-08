using Nancy;
using System.Collections.Generic;
using System;
using BestRestaurants.Objects;

namespace BestRestaurants
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
      Get["/restaurant/new"] = _ => {
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["restaurant-new.cshtml", allCuisines];
      };
      Post["/restaurants-by-cuisine"] = _ => {
        Cuisine selectedCuisine = Cuisine.Find(Int32.Parse(Request.Form["type"]));
        List<Restaurant> cuisineRestaurants = selectedCuisine.GetRestaurants();
        List<object> model = new List<object>{};
        model.Add(selectedCuisine.Type);
        model.Add(cuisineRestaurants);
        return View["cuisine.cshtml", model];
      };
      Post["/restaurant/new"] = _ => {
        Restaurant newRestaurant = new Restaurant(Request.Form["name"], Request.Form["type"]);
        newRestaurant.Save();
        List<Cuisine> allCuisines = Cuisine.GetAll();
        Dictionary<Cuisine, List<Restaurant>> model = new Dictionary<Cuisine, List<Restaurant>>(){};
        foreach(Cuisine cuisine in allCuisines)
        {
          List<Restaurant> allRestaurants = cuisine.GetRestaurants();
          model.Add(cuisine, allRestaurants);
        }
        return View["restaurants.cshtml", model];
      };

    }
  }
}
