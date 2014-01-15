namespace Mashup.Data.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    using EntityFramework.Extensions;

    /// <summary>
    /// The table.
    /// </summary>
    /// <typeparam name="T">
    /// Type of table.
    /// </typeparam>
    public class Table<T> where T : class
    {
        /// <summary>
        /// The data context.
        /// </summary>
        private readonly DbContext dataContext;

        /// <summary>
        /// The data set.
        /// </summary>
        private readonly DbSet<T> dataSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Table{T}"/> class.
        /// </summary>
        /// <param name="dataContext">
        /// The data context.
        /// </param>
        public Table(DbContext dataContext)
        {
            this.dataContext = dataContext;
            this.dataSet = this.dataContext.Set<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Table{T}"/> class.
        /// </summary>
        /// <param name="dataSet">
        /// The data set.
        /// </param>
        /// <param name="dataContext">
        /// The data context.
        /// </param>
        public Table(DbSet<T> dataSet, DbContext dataContext)
        {
            this.dataSet = dataSet;
            this.dataContext = dataContext;
        }

        /// <summary>
        /// The new.
        /// </summary>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T NewRow()
        {
            return this.dataSet.Create();
        }

        /// <summary>
        /// The get by primary key.
        /// </summary>
        /// <param name="keys">
        /// The keys.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T GetByPrimaryKey(params object[] keys)
        {
            return this.dataSet.Find(keys);
        }

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T GetById(int id)
        {
            return this.GetByPrimaryKey(id);
        }

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="row">
        /// The row.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T Insert(T row)
        {
            return this.dataSet.Add(row);
        }

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="rows">
        /// The rows.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        public IEnumerable<T> Insert(IEnumerable<T> rows)
        {
            if (rows == null)
            {
                return null;
            }

            return rows.Select(row => this.dataSet.Add(row)).ToList();
        }

        /// <summary>
        /// The count.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Count()
        {
            return this.dataSet.Count();
        }

        /// <summary>
        /// The new query.
        /// </summary>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public IQueryable<T> NewQuery()
        {
            return this.dataSet;
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="row">
        /// The row.
        /// </param>
        public void Delete(T row)
        {
            this.dataSet.Remove(row);
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="whereExpression">
        /// The where expression.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        public IEnumerable<T> Delete(Expression<Func<T, bool>> whereExpression)
        {
            var rows = this.dataSet.Where(whereExpression);
            if (!rows.Any())
            {
                return rows;
            }

            var returnList = new List<T>();
            foreach (var row in rows)
            {
                returnList.Add(this.dataSet.Remove(row));
            }

            return returnList;
        }

        public void DeleteWhere(Expression<Func<T, bool>> whereExpression)
        {
            this.dataSet.Delete(whereExpression);
            this.dataContext.Database.Log.Invoke(whereExpression.Body.ToString());
        }

        /// <summary>
        /// The delete first.
        /// </summary>
        /// <param name="whereExpression">
        /// The where expression.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T DeleteFirst(Expression<Func<T, bool>> whereExpression)
        {
            var row = this.dataSet.FirstOrDefault(whereExpression);

            return row == null ? null : this.dataSet.Remove(row);
        }

        /// <summary>
        /// The to list.
        /// </summary>
        /// <returns>
        /// The <see cref="List{T}"/>.
        /// </returns>
        public List<T> ToList()
        {
            return this.dataSet.ToList();
        }

        /// <summary>
        /// The load.
        /// </summary>
        /// <param name="row">
        /// The row.
        /// </param>
        /// <param name="navigationProperty">
        /// The navigation property.
        /// </param>
        /// <typeparam name="TProperty">
        /// Type of property to load.
        /// </typeparam>
        /// <returns>
        /// The <see cref="Table{T}"/>.
        /// </returns>
        public Table<T> Load<TProperty>(T row, Expression<Func<T, ICollection<TProperty>>> navigationProperty) where TProperty : class
        {
            var collection = this.dataContext.Entry(row).Collection(navigationProperty);

            if (!collection.IsLoaded)
            {
                collection.Load();
            }

            return this;
        }

        /// <summary>
        /// The load.
        /// </summary>
        /// <param name="row">
        /// The row.
        /// </param>
        /// <param name="navigationProperty">
        /// The navigation property.
        /// </param>
        /// <typeparam name="TProperty">
        /// Type of proeprty to load.
        /// </typeparam>
        /// <returns>
        /// The <see cref="Table{T}"/>.
        /// </returns>
        public Table<T> Load<TProperty>(T row, Expression<Func<T, TProperty>> navigationProperty) where TProperty : class
        {
            var collection = this.dataContext.Entry(row).Reference(navigationProperty);

            if (!collection.IsLoaded)
            {
                collection.Load();
            }

            return this;
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="row">
        /// The row.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T Update(T row)
        {
            var entry = this.dataContext.Entry(row);

            if (entry != null && entry.State == EntityState.Detached)
            {
                row = this.dataSet.Attach(row);
            }

            if (entry != null)
            {
                entry.State = EntityState.Modified;
            }

            return row;
        }

        public void Update(Expression<Func<T, T>> updateExpression, Expression<Func<T, bool>> whereExpression = null)
        {
            if (whereExpression == null)
            {
                this.dataSet.Update(updateExpression);                
            }

            this.dataSet.Update(whereExpression, updateExpression);
        }

        /// <summary>
        /// The get or new.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T GetOrNew(int id)
        {
            var row = this.GetById(id);
            return row ?? this.NewRow();
        }

        /// <summary>
        /// The get or new.
        /// </summary>
        /// <param name="keys">
        /// The keys.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T GetOrNew(params object[] keys)
        {
            var row = this.GetByPrimaryKey(keys);
            return row ?? this.NewRow();
        }
    }
}
