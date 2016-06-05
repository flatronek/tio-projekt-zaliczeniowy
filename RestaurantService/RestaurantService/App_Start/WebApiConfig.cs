﻿using System.Web.Http;
using System.Web.OData.Builder;
using RestaurantService.Models;
using System.Web.OData.Extensions;


namespace RestaurantService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            //config.MapHttpAttributeRoutes();

            ODataModelBuilder builder = new ODataConventionModelBuilder();

            builder.EntitySet<Restaurant>("Restaurants");

            builder.EntitySet<Rate>("Rates");

            builder.Function("TestFunction").Returns<string>();

            builder.Function("MockRate").Returns<string>();

            builder.Function("RateRestaurant").Returns<string>();

            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null,
                model: builder.GetEdmModel());
        }
    }
}
