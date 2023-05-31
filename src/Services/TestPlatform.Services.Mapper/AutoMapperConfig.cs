namespace TestPlatform.Services.Mapper
{
    using System.Reflection;

    using AutoMapper;
    using TestPlatform.Database.Entities;
    using TestPlatform.Services.Mapper.Interfaces;
    using TestPlatform.Services.Mapper.Profiles;

    public static class AutoMapperConfig
    {
        private static bool isInitialized;

        public static IMapper MapperInstance { get; set; }

        public static void RegisterMappings(params Assembly[] assemblies)
        {
            if (isInitialized)
            {
                return;
            }

            isInitialized = true;

            List<Type> types = assemblies
                .SelectMany(a => a.GetExportedTypes())
                .ToList();

            MapperConfigurationExpression config = new MapperConfigurationExpression();
            config.CreateProfile(
                    "ReflectionProfile",
                    configuration =>
                    {
                        // IMapFrom<>
                        foreach (var map in GetFromMaps(types))
                        {
                            configuration.CreateMap(map.Source, map.Destination);
                        }

                        // IMapTo<>
                        foreach (var map in GetToMaps(types))
                        {
                            configuration.CreateMap(map.Source, map.Destination);
                        }

                        // IHaveCustomMappings
                        foreach (var map in GetCustomMappings(types))
                        {
                            map.CreateMappings(configuration);
                        }

                        // EntityTypes
                        foreach (var map in GetEntityTypes(types))
                        {
                            configuration.CreateMap(map.Source, map.Destination);
                        }

                        // GetEntityTypesToBaseEntity
                        foreach (var map in GetEntityTypesToBaseEntity(types))
                        {
                            configuration.CreateMap(map.Source, map.Destination);
                        }

                        // GetEntityTypesToBaseBM
                        foreach (var map in GetEntityTypesToBaseEntity(types))
                        {
                            configuration.CreateMap(map.Source, map.Destination);
                        }
                    });

            config.AddProfile<SelectListItemProfile>();

            MapperInstance = new Mapper(new MapperConfiguration(config));
        }

        private static IEnumerable<TypesMap> GetEntityTypes(IEnumerable<Type> types)
        {
            IEnumerable<TypesMap> entityTypes = GetTypesWithBaseTypeBaseEntity(types)
                .Select(t => new TypesMap() { Source = t, Destination = t });

            return entityTypes;
        }

        private static IEnumerable<TypesMap> GetEntityTypesToBaseEntity(IEnumerable<Type> types)
        {
            IEnumerable<TypesMap> entityTypes = GetTypesWithBaseTypeBaseEntity(types)
                .Select(t => new TypesMap() { Source = t, Destination = typeof(BaseEntity) });

            return entityTypes;
        }

        private static IEnumerable<TypesMap> GetEntityTypesToBaseBM(IEnumerable<Type> types)
        {
            Type baseBM = types.Where(t => t.Name == "BaseBM")
                .First();

            IEnumerable<TypesMap> entityTypes = GetTypesWithBaseTypeBaseEntity(types)
                .Select(t => new TypesMap() { Source = t, Destination = baseBM });

            return entityTypes;
        }

        private static IEnumerable<Type> GetTypesWithBaseTypeBaseEntity(IEnumerable<Type> types)
        {
            IEnumerable<Type> entityTypes = types.Where(t =>
                                    t.GetTypeInfo().BaseType == typeof(BaseEntity)
                                    && t.GetTypeInfo().IsAbstract == false
                                    && t.GetTypeInfo().IsInterface == false);

            return entityTypes;
        }

        private static IEnumerable<TypesMap> GetFromMaps(IEnumerable<Type> types)
        {
            IEnumerable<TypesMap> fromMaps = types
                .SelectMany(t => t.GetTypeInfo().GetInterfaces(), (t, i) => new { Type = t, Interface = i })
                .Where(x => x.Interface.GetTypeInfo().IsGenericType)
                .Where(x => x.Interface.GetTypeInfo().GetGenericTypeDefinition() == typeof(IMapFrom<>))
                .Where(x => !x.Type.GetTypeInfo().IsAbstract)
                .Where(x => !x.Type.GetTypeInfo().IsInterface)
                .Select(x => new TypesMap
                {
                    Source = x.Interface.GetTypeInfo().GetGenericArguments()[0],
                    Destination = x.Type,
                });

            return fromMaps;
        }

        private static IEnumerable<TypesMap> GetToMaps(IEnumerable<Type> types)
        {
            IEnumerable<TypesMap> toMaps = types
                .SelectMany(t => t.GetTypeInfo().GetInterfaces(), (t, i) => new { Type = t, Interface = i })
                .Where(x => x.Interface.GetTypeInfo().IsGenericType)
                .Where(x => x.Interface.GetTypeInfo().GetGenericTypeDefinition() == typeof(IMapTo<>))
                .Where(x => !x.Type.GetTypeInfo().IsAbstract)
                .Where(x => !x.Type.GetTypeInfo().IsInterface)
                .Select(x => new TypesMap
                {
                    Source = x.Type,
                    Destination = x.Interface.GetTypeInfo().GetGenericArguments()[0],
                });

            return toMaps;
        }

        private static IEnumerable<IHaveCustomMappings> GetCustomMappings(IEnumerable<Type> types)
        {
            IEnumerable<IHaveCustomMappings> customMaps = types
                .SelectMany(t => t.GetTypeInfo().GetInterfaces(), (t, i) => new { Type = t, Interface = i })
                .Where(x => typeof(IHaveCustomMappings).GetTypeInfo().IsAssignableFrom(x.Type))
                .Where(x => !x.Type.GetTypeInfo().IsAbstract)
                .Where(x => !x.Type.GetTypeInfo().IsInterface)
                .Select(x => (IHaveCustomMappings)Activator.CreateInstance(x.Type));

            return customMaps;
        }
    }
}
