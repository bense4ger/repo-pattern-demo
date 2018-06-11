
using AutoMapper;

namespace RepositoryDemo.Web
{
    public static class MappingConfig
    {
        public static void RegisterMappings()
        {
            // We've only got two profiles, so by hand is fine.
            // In real life, we'd use assembly scanning
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<RepositoryDemo.Domain.Games.Profiles.GameProfile>();
                cfg.AddProfile<Profiles.GameProfile>();
            });
        }
    }
}