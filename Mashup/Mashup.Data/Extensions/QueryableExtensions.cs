namespace Mashup.Data.Extensions
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using EntityFramework.Extensions;

    /// <summary>
    /// The extensions for type <see cref="IQueryable{TSource}"/>.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// The cast.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <typeparam name="TSource">
        /// Type of result class.
        /// </typeparam>
        /// <returns>
        /// The <see cref="ProjectionExpression{TSource}"/>.
        /// </returns>
        public static ProjectionExpression<TSource> Cast<TSource>(this IQueryable<TSource> query)
        {
            return new ProjectionExpression<TSource>(query);
        }

        /// <summary>
        /// Batch update
        /// </summary>
        /// <typeparam name="TSource">Typ encji</typeparam>
        /// <param name="query">Obiekt queryable encji/</param>
        /// <param name="updateExpression">Sposób aktualizacji</param>
        public static void BatchUpdate<TSource>(this IQueryable<TSource> query, Expression<Func<TSource, TSource>> updateExpression)
            where TSource : class
        {
            query.Update(updateExpression);
        }

        /// <summary>
        /// Batch delete
        /// </summary>
        /// <typeparam name="TSource">Typ encji</typeparam>
        /// <param name="query">Obiekt queryable encji/</param>
        public static void BatchDelete<TSource>(this IQueryable<TSource> query)
            where TSource : class
        {
            query.Delete();
        }
    }
}
