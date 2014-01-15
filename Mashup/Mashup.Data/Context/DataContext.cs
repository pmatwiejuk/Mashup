namespace Mashup.Data.Context
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Validation;

    using Mashup.Data.Contracts;
    using Mashup.Data.Mapping;
    using Mashup.Data.Repository;

    /// <summary>
    /// The data context.
    /// </summary>
    public class DataContext : DbContext, IDataContext
    {
        /// <summary>
        /// The connection name.
        /// </summary>
        public readonly string ConnectionName;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext"/> class.
        /// </summary>
        public DataContext()
            : base(typeof(DataContext).Name)
        {
            this.ConnectionName = typeof(DataContext).Name;

            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer<DataContext>(null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext"/> class.
        /// </summary>
        /// <param name="dataContextConfiguration">Konfiguracja kontekstu</param>
        public DataContext(DataContextConfiguration dataContextConfiguration)
            : base(!string.IsNullOrWhiteSpace(dataContextConfiguration.Connection) ? dataContextConfiguration.Connection : typeof(DataContext).Name)
        {
            this.ConnectionName = !string.IsNullOrWhiteSpace(dataContextConfiguration.Connection) ? dataContextConfiguration.Connection : typeof(DataContext).Name;

            this.Configuration.AutoDetectChangesEnabled = dataContextConfiguration.AutoDetectChangesEnabled;
            this.Configuration.LazyLoadingEnabled = dataContextConfiguration.LazyLoadingEnabled;
            this.Configuration.ProxyCreationEnabled = dataContextConfiguration.ProxyCreationEnabled;
            this.Configuration.ValidateOnSaveEnabled = dataContextConfiguration.ValidateOnSaveEnabled;

            Database.SetInitializer<DataContext>(null);
        }

        public void ClearCache()
        {
            if (!this.Configuration.ProxyCreationEnabled)
            {
                return;
            }

            foreach (var dbEntityEntry in this.ChangeTracker.Entries())
            {
                dbEntityEntry.Reload();
            }
        }

        public void ClearCache<T>() where T : class
        {
            if (!this.Configuration.ProxyCreationEnabled)
            {
                return;
            }

            foreach (var dbEntityEntry in this.ChangeTracker.Entries<T>())
            {
                dbEntityEntry.Reload();
            }
        }

        /// <summary>
        /// The commit changes.
        /// </summary>
        /// <param name="transactionName">
        /// The transaction name.
        /// </param>
        public void CommitChanges(string transactionName = null)
        {
            this.SaveChanges();
        }

        /// <summary>
        /// Execute the SQL command.
        /// </summary>
        /// <param name="sqlCommand">
        /// The SQL command.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <typeparam name="T">
        /// Type of model to return.
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        public IEnumerable<T> Sql<T>(string sqlCommand, params object[] parameters)
        {
            return this.Database.SqlQuery<T>(sqlCommand, parameters);
        }

        /// <summary>
        /// Metoda walidująca encję.
        /// </summary>
        /// <typeparam name="T">Type encji</typeparam>
        /// <param name="entity">Obiekt encji</param>
        /// <returns>Rezultat walidacji encji</returns>
        public DbEntityValidationResult Validate<T>(T entity)
            where T : class
        {
            var result = this.ValidateEntity(this.Entry(entity), null);
            return result;
        }

        /// <summary>
        /// Metoda walidująca encję.
        /// </summary>
        /// <typeparam name="T">Type encji</typeparam>
        /// <param name="entity">Obiekt encji</param>
        /// <returns>Rezultat walidacji encji</returns>
        public bool IsValid<T>(T entity)
            where T : class
        {
            var result = this.ValidateEntity(this.Entry(entity), null);
            return result.IsValid;
        }

        /// <summary>
        /// Execute the SQL with no result.
        /// </summary>
        /// <param name="sqlCommand">
        /// The SQL command.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int SqlNoResult(string sqlCommand, params object[] parameters)
        {
            return this.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        /// <summary>
        /// The table.
        /// </summary>
        /// <typeparam name="T">
        /// Type of table model.
        /// </typeparam>
        /// <returns>
        /// The <see cref="Table{T}"/>.
        /// </returns>
        public Table<T> Table<T>() where T : class
        {
            return new Table<T>(this);
        }

        /// <summary>
        /// The new.
        /// </summary>
        /// <typeparam name="T">
        /// Type of new row.
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T NewRow<T>() 
            where T : class
        {
            return this.Set<T>().Create();
        }

        /// <summary>
        /// The on model creating.
        /// </summary>
        /// <param name="modelBuilder">
        /// The model builder.
        /// </param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            MapInitializer.InitMapping(modelBuilder);
        }
    }
}
