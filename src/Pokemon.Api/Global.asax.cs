using AutoMapper;
using Pokemon.CrossCurring.IoC;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System;
using System.Linq;
using System.Web.Http;

namespace Pokemon.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            InitializeContiner();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        private Container container = new Container();

        private void InitializeContiner()
        {            
            container.RegisterContainers();

            AutomapperRegister();

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();                       
            
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }

        private void AutomapperRegister()
        {
            var profiles =
                from t in typeof(WebApiApplication).Assembly.GetTypes()
                where typeof(Profile).IsAssignableFrom(t)
                select (Profile)Activator.CreateInstance(t);

            container.RegisterAutoMapper(profiles);
        }
    }
}
