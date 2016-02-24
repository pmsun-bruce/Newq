namespace Newq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    public class DbTable
    {
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, DbColumn> columns;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public DbTable(Type type)
        {
            Name = type.Name;
            columns = new Dictionary<string, DbColumn>();
            SetColumns(type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public DbTable(object entity)
        {
            var type = entity.GetType();

            Name = type.Name;
            columns = new Dictionary<string, DbColumn>();
            SetAssignedTable(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        internal DbTable(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        public IReadOnlyList<DbColumn> Columns
        {
            get { return columns.Values.ToList(); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="type"></param>
        private void SetColumns(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            DbColumn column = null;
            var columnName = string.Empty;
            var properties = type.GetProperties().Where(p => p.CanWrite);
            var idProperty = properties.Where(p => p.Name == "Id");

            if (idProperty.Count() > 0)
            {
                columnName = "Id";
                column = new DbColumn(this, columnName);
                columns.Add(columnName, column);
            }

            foreach (var prop in properties)
            {
                columnName = prop.Name;

                if (columnName == "Id")
                {
                    continue;
                }

                column = new DbColumn(this, columnName);
                columns.Add(columnName, column);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        private void SetAssignedTable(object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            DbColumn column = null;
            object columnValue = null;
            var columnName = string.Empty;
            var properties = entity.GetType().GetProperties().Where(p => p.CanWrite);
            var idProperty = properties.Where(p => p.Name == "Id");

            if (idProperty.Count() > 0)
            {
                columnValue = idProperty.First().GetValue(entity);
                columnName = "Id";
                column = new DbColumn(this, columnName, columnValue);
                columns.Add(columnName, column);
            }

            foreach (var prop in properties)
            {
                columnValue = prop.GetValue(entity);
                columnName = prop.Name;

                if (columnName != "Id")
                {
                    column = new DbColumn(this, columnName, columnValue);
                    columns.Add(columnName, column);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[{0}]", Name);
        }
    }
}
