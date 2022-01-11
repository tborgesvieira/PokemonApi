using AutoMapper;
using Pokemon.Services;
using Pokemon.Services.Services.Interfaces;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.CrossCurring.IoC
{
    public static class Bootstrapper
    {
        public static void RegisterContainers(this Container container)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.Register(() => new HttpClient(), Lifestyle.Scoped);            

            container.Register<IApiService, ApiService>(Lifestyle.Scoped);

            container.Register<IPokemonApi, PokemonApi>(Lifestyle.Scoped);
        }

        public static void RegisterAutoMapper(this Container container, IEnumerable<Profile> profiles)
        {
            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });

            container.RegisterInstance(config);

            container.Register(() => config.CreateMapper(container.GetInstance));
        }
    }
}
