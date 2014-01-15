namespace Mashup.Data.Mapping
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration;
    using System.Data.Entity.ModelConfiguration.Configuration;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Klasa inicjalizująca mappingi
    /// </summary>
    public static class MapInitializer
    {
        /// <summary>
        /// Metoda inicjalizująca mappingi
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void InitMapping(DbModelBuilder modelBuilder)
        {
            var classMaps = Assembly.GetExecutingAssembly().GetTypes().Where(type => type.BaseType != null && type.BaseType.Name == typeof(EntityTypeConfiguration<>).Name);

            var addMethod = typeof(ConfigurationRegistrar).GetMethods(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(method => method.Name == "Add" && method.GetGenericArguments()[0].Name == "TEntityType");
            if (addMethod == null)
            {
                return;
            }

            foreach (var classMap in classMaps.Where(x => x.BaseType != null))
            {
                var instance = Activator.CreateInstance(classMap);

                if (classMap.BaseType != null)
                {
                    var addClassMap = addMethod.MakeGenericMethod(classMap.BaseType.GetGenericArguments()[0]);
                    addClassMap.Invoke(modelBuilder.Configurations, new[] { instance });
                }
            }
        }
    }
}
