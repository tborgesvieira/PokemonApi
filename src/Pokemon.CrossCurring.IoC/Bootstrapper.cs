using AutoMapper;
using Pokemon.Data;
using Pokemon.Data.Context;
using Pokemon.Domain.Interfaces;
using Pokemon.Domain.Services;
using Pokemon.Services;
using Pokemon.Services.Services.Interfaces;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System.Collections.Generic;
using System.Net.Http;

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

            container.Register<IMestrePokemonService, MestrePokemonService>(Lifestyle.Scoped);

            container.Register<IMestrePokemonRepository, MestrePokemonRepository>(Lifestyle.Scoped);

            container.Register<PokemonContext>(Lifestyle.Scoped);
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
