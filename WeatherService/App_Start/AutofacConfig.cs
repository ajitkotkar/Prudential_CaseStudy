using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherService.Helper;

namespace WeatherService.App_Start
{
    public class AutofacConfig
    {
        public static void ConfigureIOCContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            RegisterTypes(builder);
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void RegisterTypes(ContainerBuilder builder)
        {
            
            builder.RegisterType<CSVReader>().As<ICSVReader>();
            builder.RegisterType<WeatherInfoGenerator>().As<IWeatherInfoGenerator>();
            builder.RegisterType<OpenWeatherMapService>().As<IOpenWeatherMapService>();

        }
    }
}