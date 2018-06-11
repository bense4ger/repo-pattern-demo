using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.CompilerServices;
using Autofac;
using Autofac.Core;
using Autofac.Core.Resolving;
using RepositoryDemo.Data.Data;
using RepositoryDemo.Data.Entities;
using RepositoryDemo.Domain.Generic;

namespace RepositoryDemo.Web
{
    public static class DependecyConfig
    {
        // In real life, we'd wouldn't do everything here.  Each project would handle its own dependency configuration.
        public static void Configure(ref ContainerBuilder builder)
        {
            builder.RegisterType(typeof(FileReader))
                .As<IFileReader>()
                .WithParameter((pi, ctx) => pi.Position == 0, (pi, ctx) => Environment.GetFolderPath(System.Environment.SpecialFolder.Personal))
                .InstancePerDependency();
           

            builder.RegisterGeneric(typeof(CsvContext<>))
                .AsSelf()
                .InstancePerLifetimeScope();
            
            builder.RegisterGeneric(typeof(JsonContext<>))
                .AsSelf()
                .InstancePerLifetimeScope();

            //Repostiories
            builder.RegisterGeneric(typeof(Repository<>))
                .WithParameter(new ResolvedParameter((pi, ctx) => pi.Name == "context", (pi, ctx) => ResolveContext(ctx)))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            //Services
            var services = Assembly
                .GetEntryAssembly()
                .GetReferencedAssemblies()
                .Where(a => a.Name.StartsWith("RepositoryDemo."))
                .Select(Assembly.Load)
                .SelectMany(a => a.ExportedTypes)
                .Where(t => t.IsAssignableTo<IDomainService>() && t.IsClass);

            foreach (var service in services)
            {
                builder.RegisterType(service)
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
            }
        }

        private static object ResolveContext(IComponentContext context)
        {
            
            var service = ((context as IInstanceLookup)?
                .ComponentRegistration
                .Services
                .FirstOrDefault() as TypedService);
            
            var genericType = service?
                    .ServiceType
                    .GenericTypeArguments[0];


            var t = service?.ServiceType.GetInterfaces().Any(i => i == typeof(IJsonRepository)) ?? false
                ? typeof(JsonContext<>).MakeGenericType(genericType)
                : typeof(CsvContext<>).MakeGenericType(genericType);

            var obj = context.Resolve(t);

            return obj;
        }
    }
}