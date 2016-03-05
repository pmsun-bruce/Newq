namespace Newq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Simulate tables in Sql
    /// </summary>
    public class Table
    {
        /// <summary>
        /// An dictionary used to get columns in the table.
        /// </summary>
        private Dictionary<string, Column> columns;

        /// <summary>
        /// Default primary key.
        /// </summary>
        public static string DefaultPrimaryKey = "Id";

        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class.
        /// </summary>
        /// <param name="type">Type of the corresponding class</param>
        public Table(Type type)
        {
            Name = type.Name;
            columns = new Dictionary<string, Column>();
            SetColumns(type);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class.
        /// </summary>
        /// <param name="obj">An entity of the corresponding class</param>
        public Table(object obj)
        {
            var type = obj.GetType();

            Name = type.Name;
            columns = new Dictionary<string, Column>();
            SetAssignedColumns(obj);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class.
        /// </summary>
        /// <param name="name">Name of the corresponding class</param>
        internal Table(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets or sets <see cref="Name"/>.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Define which column in this should be the primary key.
        /// </summary>
        public List<Column> PrimaryKey { get; set; }

        /// <summary>
        /// A read-only list contains the columns in this.
        /// </summary>
        public IReadOnlyList<Column> Columns
        {
            get { return columns.Values.ToList(); }
        }

        /// <summary>
        /// Gets an certain entity of column by its name with an indexer
        /// </summary>
        /// <param name="index">name of the column</param>
        /// <returns>an entity of column</returns>
        public Column this[int index]
        {
            get { return Columns[index]; }
        }

        /// <summary>
        /// Gets <see cref="Column"/> by column name.
        /// </summary>
        /// <param name="columnName">name of the column</param>
        /// <returns>an entity of column</returns>
        public Column this[string columnName]
        {
            get
            {
                if (!columns.ContainsKey(columnName))
                {
                    return new Column(Name, columnName);
                }

                return columns[columnName];
            }
        }

        /// <summary>
        /// Gets <see cref="KeyValuePair{TKey, TValue}"/> by column name and sort order.
        /// </summary>
        /// <param name="columnName">name of the column</param>
        /// <param name="order"></param>
        /// <returns>an entity of column</returns>
        public KeyValuePair<Column, SortOrder> this[string columnName, SortOrder order]
        {
            get
            {
                if (!columns.ContainsKey(columnName))
                {
                    var column = new Column(this, columnName);

                    return new KeyValuePair<Column, SortOrder>(column, order);
                }

                return new KeyValuePair<Column, SortOrder>(columns[columnName], order);
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

            Column column = null;
            var defaultPK = type.GetProperty(DefaultPrimaryKey);
            var properties = type.GetProperties().Where(p => p.CanRead && p.CanWrite && p.Name != DefaultPrimaryKey);

            if (defaultPK != null)
            {
                column = new Column(this, DefaultPrimaryKey);
                columns.Add(DefaultPrimaryKey, column);
                PrimaryKey = new List<Column> { column };
            }

            foreach (var prop in properties)
            {
                column = new Column(this, prop.Name);
                columns.Add(prop.Name, column);
            }
        }

        /// <summary>
        /// Insert all columns which has certain value
        /// and set primary key if needed
        /// </summary>
        /// <param name="obj">An entity of the corresponding class</param>
        private void SetAssignedColumns(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            Column column = null;
            var type = obj.GetType();
            var defaultPK = type.GetProperty(DefaultPrimaryKey);
            var properties = type.GetProperties().Where(p => p.CanRead && p.CanWrite && p.Name != DefaultPrimaryKey);

            if (defaultPK != null)
            {
                column = new Column(this, DefaultPrimaryKey, defaultPK.GetValue(obj));
                columns.Add(DefaultPrimaryKey, column);
                PrimaryKey = new List<Column> { column };
            }

            foreach (var prop in properties)
            {
                column = new Column(this, prop.Name, prop.GetValue(obj));
                columns.Add(prop.Name, column);
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetPrimaryKey()
        {
            var primarykey = string.Empty;

            if (PrimaryKey != null && PrimaryKey.Count > 0)
            {
                PrimaryKey.ForEach(pk => primarykey += string.Format("{0}, ", pk));
                primarykey = primarykey.Remove(primarykey.Length - 2);
            }

            return primarykey;
        }
    }
}
