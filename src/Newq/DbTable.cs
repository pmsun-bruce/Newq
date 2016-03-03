namespace Newq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Simulate tables in Sql
    /// </summary>
    public class DbTable
    {
        /// <summary>
        /// An dictionary used to get columns in the table
        /// </summary>
        private Dictionary<string, DbColumn> columns;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbTable"/> class.
        /// </summary>
        /// <param name="type">Type of the corresponding class</param>
        public DbTable(Type type)
        {
            Name = type.Name;
            columns = new Dictionary<string, DbColumn>();
            SetColumns(type);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbTable"/> class.
        /// </summary>
        /// <param name="entity">An entity of the corresponding class</param>
        public DbTable(object entity)
        {
            var type = entity.GetType();

            Name = type.Name;
            columns = new Dictionary<string, DbColumn>();
            SetAssignedTable(entity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbTable"/> class.
        /// </summary>
        /// <param name="name">Name of the corresponding class</param>
        internal DbTable(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Name of this
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Define which column in this should be the primary key.
        /// </summary>
        public DbColumn PrimaryKey
        {
            get
            {
                if (columns == null && columns.Count == 0)
                {
                    return null;
                }

                return Columns[0];
            }
        }

        /// <summary>
        /// A read-only list contains the columns in this
        /// </summary>
        public IReadOnlyList<DbColumn> Columns
        {
            get { return columns.Values.ToList(); }
        }

        /// <summary>
        /// Gets an certain entity of column by its name with an indexer
        /// </summary>
        /// <param name="columnName">name of the column</param>
        /// <returns>an entity of column</returns>
        public DbColumn this[string columnName]
        {
            get
            {
                if (!columns.ContainsKey(columnName))
                {
                    return new DbColumn(Name, columnName);
                }

                return columns[columnName];
            }
        }

        /// <summary>
        /// Insert all columns and set primary key if needed
        /// </summary>
        /// <param name="type">Type of the corresponding class</param>
        private void SetColumns(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            DbColumn column = null;
            var columnName = string.Empty;
            var hasId = false;

            // only properties that can be set is needed(except "Id")
            var properties = type.GetProperties().Where(p => {
                if (p.Name == "Id")
                {
                    hasId = true;

                    return false;
                }

                return p.CanWrite;
            });

            if (hasId)
            {
                column = new DbColumn(this, "Id");
                columns.Add("Id", column);
            }

            foreach (var prop in properties)
            {
                columnName = prop.Name;
                column = new DbColumn(this, columnName);
                columns.Add(columnName, column);
            }
        }

        /// <summary>
        /// Insert all columns which has certain value
        /// and set primary key if needed
        /// </summary>
        /// <param name="entity">An entity of the corresponding class</param>
        private void SetAssignedTable(object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            DbColumn column = null;
            var columnName = string.Empty;
            var hasId = false;
            object columnValue = null;

            // only properties that can be set is needed(except "Id")
            var properties = entity.GetType().GetProperties().Where(p => {
                if (p.Name == "Id")
                {
                    hasId = true;
                    columnValue = p.GetValue(entity);

                    return false;
                }

                return p.CanWrite;
            });

            if (hasId)
            {
                column = new DbColumn(this, "Id", columnValue);
                columns.Add("Id", column);
            }

            foreach (var prop in properties)
            {
                columnValue = prop.GetValue(entity);
                columnName = prop.Name;
                column = new DbColumn(this, columnName, columnValue);
                columns.Add(columnName, column);
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>Name of this with a pair of square brackets</returns>
        public override string ToString()
        {
            return string.Format("[{0}]", Name);
        }
    }
}
