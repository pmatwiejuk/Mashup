namespace Mashup.Data.Extensions
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// The projection expression.
    /// </summary>
    /// <typeparam name="TSource">
    /// Type of source class.
    /// </typeparam>
    public class ProjectionExpression<TSource>
    {
        /// <summary>
        /// The source.
        /// </summary>
        private readonly IQueryable<TSource> source;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectionExpression{TSource}"/> class.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        public ProjectionExpression(IQueryable<TSource> source)
        {
            this.source = source;
        }

        /// <summary>
        /// The build expression.
        /// </summary>
        /// <typeparam name="TResult">
        /// Type of result class
        /// </typeparam>
        /// <returns>
        /// The <see cref="Expression"/>.
        /// </returns>
        public static Expression<Func<TSource, TResult>> BuildExpression<TResult>()
        {
            var sourceMembers = typeof(TSource).GetProperties();
            var destinationMembers = typeof(TResult).GetProperties();
            var matchingMembers = destinationMembers.Join(sourceMembers, dest => dest.Name, src => src.Name, (dest, src) => dest);

            const string Name = "src";

            var parameterExpression = Expression.Parameter(typeof(TSource), Name);
            var propertyExpressions = matchingMembers.Select(dest => Expression.Bind(dest, Expression.Property(parameterExpression, sourceMembers.First(pi => pi.Name == dest.Name)))).ToArray();

            return Expression.Lambda<Func<TSource, TResult>>(Expression.MemberInit(Expression.New(typeof(TResult)), propertyExpressions), parameterExpression);
        }

        /// <summary>
        /// The to.
        /// </summary>
        /// <typeparam name="TResult">
        /// Type of result class.
        /// </typeparam>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public IQueryable<TResult> To<TResult>()
        {
            var expr = BuildExpression<TResult>();

            return this.source.Select(expr);
        }
    }
}