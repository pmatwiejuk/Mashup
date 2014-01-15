namespace Mashup.Data.Extensions
{
    using System;
    using System.Data.Entity.ModelConfiguration;
    using System.Data.Entity.ModelConfiguration.Configuration;
    using System.Linq.Expressions;

    /// <summary>
    /// The entity extensions.
    /// </summary>
    public static class MappingExtensions
    {
        public static EntityTypeConfiguration<T> ToTable<T>(this EntityTypeConfiguration<T> config)  where T : class
        {
            return config.ToTable(typeof(T).Name);
        }

        public static EntityTypeConfiguration<T> InSchema<T>(this EntityTypeConfiguration<T> config, string schemaName) where T : class
        {
            return config.ToTable(typeof(T).Name, schemaName);
        }

        /// <summary>
        /// The has column name.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <param name="navigationProperty">
        /// The navigation property.
        /// </param>
        /// <typeparam name="T">
        /// Type of table.
        /// </typeparam>
        /// <typeparam name="TProp">
        /// Type of property.
        /// </typeparam>
        /// <returns>
        /// The <see cref="PrimitivePropertyConfiguration"/>.
        /// </returns>
        public static PrimitivePropertyConfiguration HasColumnName<T, TProp>(this EntityTypeConfiguration<T> config, Expression<Func<T, TProp>> navigationProperty) 
            where T : class
            where TProp : struct
        {
            return config.Property(navigationProperty).HasColumnName(navigationProperty.Name);
        }

        /// <summary>
        /// The has column name.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <param name="navigationProperty">
        /// The navigation property.
        /// </param>
        /// <typeparam name="T">
        /// Type of table.
        /// </typeparam>
        /// <typeparam name="TProp">
        /// Type of property.
        /// </typeparam>
        /// <returns>
        /// The <see cref="PrimitivePropertyConfiguration"/>.
        /// </returns>
        public static PrimitivePropertyConfiguration HasColumnName<T, TProp>(this EntityTypeConfiguration<T> config, Expression<Func<T, TProp?>> navigationProperty)
            where T : class
            where TProp : struct
        {
            return config.Property(navigationProperty).HasColumnName(navigationProperty.Name);
        }

        /// <summary>
        /// The has column name.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <param name="navigationProperty">
        /// The navigation property.
        /// </param>
        /// <typeparam name="T">
        /// Type of table.
        /// </typeparam>
        /// <returns>
        /// The <see cref="StringPropertyConfiguration"/>.
        /// </returns>
        public static StringPropertyConfiguration HasColumnName<T>(this EntityTypeConfiguration<T> config, Expression<Func<T, string>> navigationProperty)
            where T : class
        {
            return config.Property(navigationProperty).HasColumnName(navigationProperty.Name);
        }

        /// <summary>
        /// The has column name.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <param name="navigationProperty">
        /// The navigation property.
        /// </param>
        /// <typeparam name="T">
        /// Type of table.
        /// </typeparam>
        /// <returns>
        /// The <see cref="DateTimePropertyConfiguration"/>.
        /// </returns>
        public static DateTimePropertyConfiguration HasColumnName<T>(this EntityTypeConfiguration<T> config, Expression<Func<T, DateTime>> navigationProperty)
            where T : class
        {
            return config.Property(navigationProperty).HasColumnName(navigationProperty.Name);
        }

        /// <summary>
        /// The has column name.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <param name="navigationProperty">
        /// The navigation property.
        /// </param>
        /// <typeparam name="T">
        /// Type of table.
        /// </typeparam>
        /// <returns>
        /// The <see cref="DateTimePropertyConfiguration"/>.
        /// </returns>
        public static DateTimePropertyConfiguration HasColumnName<T>(this EntityTypeConfiguration<T> config, Expression<Func<T, DateTime?>> navigationProperty)
            where T : class
        {
            return config.Property(navigationProperty).HasColumnName(navigationProperty.Name);
        }

        /// <summary>
        /// The has column name.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <param name="navigationProperty">
        /// The navigation property.
        /// </param>
        /// <typeparam name="T">
        /// Type of table.
        /// </typeparam>
        /// <returns>
        /// The <see cref="DateTimePropertyConfiguration"/>.
        /// </returns>
        public static DateTimePropertyConfiguration HasColumnName<T>(this EntityTypeConfiguration<T> config, Expression<Func<T, DateTimeOffset>> navigationProperty)
            where T : class
        {
            return config.Property(navigationProperty).HasColumnName(navigationProperty.Name);
        }

        /// <summary>
        /// The has column name.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <param name="navigationProperty">
        /// The navigation property.
        /// </param>
        /// <typeparam name="T">
        /// Type of table.
        /// </typeparam>
        /// <returns>
        /// The <see cref="DateTimePropertyConfiguration"/>.
        /// </returns>
        public static DateTimePropertyConfiguration HasColumnName<T>(this EntityTypeConfiguration<T> config, Expression<Func<T, DateTimeOffset?>> navigationProperty)
            where T : class
        {
            return config.Property(navigationProperty).HasColumnName(navigationProperty.Name);
        }

        /// <summary>
        /// The has column name.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <param name="navigationProperty">
        /// The navigation property.
        /// </param>
        /// <typeparam name="T">
        /// Type of table.
        /// </typeparam>
        /// <returns>
        /// The <see cref="DateTimePropertyConfiguration"/>.
        /// </returns>
        public static DateTimePropertyConfiguration HasColumnName<T>(this EntityTypeConfiguration<T> config, Expression<Func<T, TimeSpan>> navigationProperty)
            where T : class
        {
            return config.Property(navigationProperty).HasColumnName(navigationProperty.Name);
        }

        /// <summary>
        /// The has column name.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <param name="navigationProperty">
        /// The navigation property.
        /// </param>
        /// <typeparam name="T">
        /// Type of table.
        /// </typeparam>
        /// <returns>
        /// The <see cref="DateTimePropertyConfiguration"/>.
        /// </returns>
        public static DateTimePropertyConfiguration HasColumnName<T>(this EntityTypeConfiguration<T> config, Expression<Func<T, TimeSpan?>> navigationProperty)
            where T : class
        {
            return config.Property(navigationProperty).HasColumnName(navigationProperty.Name);
        }

        /// <summary>
        /// The has column name.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <param name="navigationProperty">
        /// The navigation property.
        /// </param>
        /// <typeparam name="T">
        /// Type of table.
        /// </typeparam>
        /// <returns>
        /// The <see cref="DecimalPropertyConfiguration"/>.
        /// </returns>
        public static DecimalPropertyConfiguration HasColumnName<T>(this EntityTypeConfiguration<T> config, Expression<Func<T, decimal>> navigationProperty)
            where T : class
        {
            return config.Property(navigationProperty).HasColumnName(navigationProperty.Name);
        }

        /// <summary>
        /// The has column name.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <param name="navigationProperty">
        /// The navigation property.
        /// </param>
        /// <typeparam name="T">
        /// Type of table.
        /// </typeparam>
        /// <returns>
        /// The <see cref="DecimalPropertyConfiguration"/>.
        /// </returns>
        public static DecimalPropertyConfiguration HasColumnName<T>(this EntityTypeConfiguration<T> config, Expression<Func<T, decimal?>> navigationProperty)
            where T : class
        {
            return config.Property(navigationProperty).HasColumnName(navigationProperty.Name);
        }

        /// <summary>
        /// The has column name.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <param name="navigationProperty">
        /// The navigation property.
        /// </param>
        /// <typeparam name="T">
        /// Type of table.
        /// </typeparam>
        /// <returns>
        /// The <see cref="BinaryPropertyConfiguration"/>.
        /// </returns>
        public static BinaryPropertyConfiguration HasColumnName<T>(this EntityTypeConfiguration<T> config, Expression<Func<T, byte[]>> navigationProperty)
            where T : class
        {
            return config.Property(navigationProperty).HasColumnName(navigationProperty.Name);
        }
    }
}
