namespace Mashup.Data.Contracts
{
    using System;
    using System.Collections.Generic;

    using Mashup.Data.Context;
    using Mashup.Data.Repository;

    public interface IDataContext : IDisposable
    {
        /// <summary>
        /// The commit changes.
        /// </summary>
        /// <param name="transactionName">
        /// The transaction name.
        /// </param>
        void CommitChanges(string transactionName = null);

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
        IEnumerable<T> Sql<T>(string sqlCommand, params object[] parameters);

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
        int SqlNoResult(string sqlCommand, params object[] parameters);

        /// <summary>
        /// The table.
        /// </summary>
        /// <typeparam name="T">
        /// Type of table model.
        /// </typeparam>
        /// <returns>
        /// The <see cref="DataContext.Table{T}"/>.
        /// </returns>
        Table<T> Table<T>() where T : class;

        /// <summary>
        /// The new.
        /// </summary>
        /// <typeparam name="T">
        /// Type of new row.
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T NewRow<T>() where T : class;

        /// <summary>
        /// Czyści cache dla wszystkich tabel.
        /// </summary>
        void ClearCache();

        /// <summary>
        /// Czyści cache dla wybranej tabeli.
        /// </summary>
        /// <typeparam name="T">Typ encji</typeparam>
        void ClearCache<T>() where T : class;
    }
}