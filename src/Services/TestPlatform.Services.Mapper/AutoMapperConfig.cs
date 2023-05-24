namespace TestPlatform.Services.Mapper
{
    using System.Reflection;

    using AutoMapper;
    using TestPlatform.Database.Entities;
    using TestPlatform.Services.Mapper.Interfaces;

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
                    });

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
            // TODO: make it with linq
            IEnumerable<TypesMap> fromMaps = from t in types
                                             from i in t.GetTypeInfo().GetInterfaces()
                                             where i.GetTypeInfo().IsGenericType
                                                         && i.GetTypeInfo().GetGenericTypeDefinition() == typeof(IMapFrom<>)
                                                         && t.GetTypeInfo().IsAbstract == false
                                                         && t.GetTypeInfo().IsInterface == false
                                             select new TypesMap
                                             {
                                                 Source = i.GetTypeInfo().GetGenericArguments()[0],
                                                 Destination = t,
                                             };

            return fromMaps;
        }

        private static IEnumerable<TypesMap> GetToMaps(IEnumerable<Type> types)
        {
            // TODO: make it with linq
            IEnumerable<TypesMap> toMaps = from t in types
                                           from i in t.GetTypeInfo().GetInterfaces()
                                           where i.GetTypeInfo().IsGenericType
                                                       && i.GetTypeInfo().GetGenericTypeDefinition() == typeof(IMapTo<>)
                                                       && t.GetTypeInfo().IsAbstract == false
                                                       && t.GetTypeInfo().IsInterface == false
                                           select new TypesMap
                                           {
                                               Source = t,
                                               Destination = i.GetTypeInfo().GetGenericArguments()[0],
                                           };

            return toMaps;
        }

        private static IEnumerable<IHaveCustomMappings> GetCustomMappings(IEnumerable<Type> types)
        {
            // TODO: make it with linq
            IEnumerable<IHaveCustomMappings> customMaps = from t in types
                                                          from i in t.GetTypeInfo().GetInterfaces()
                                                          where typeof(IHaveCustomMappings).GetTypeInfo().IsAssignableFrom(t)
                                                                      && t.GetTypeInfo().IsAbstract == false
                                                                      && t.GetTypeInfo().IsInterface == false
                                                          select (IHaveCustomMappings)Activator.CreateInstance(t);

            return customMaps;
        }
    }
}
