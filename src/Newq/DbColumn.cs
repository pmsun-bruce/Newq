namespace Newq
{
    using System.Data;

    /// <summary>
    /// 
    /// </summary>
    public class DbColumn
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbColumn"/> class.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        public DbColumn(DbTable table, string name, object value = null, SqlDbType? type = null)
        {
            Table = table;
            Name = name;
            Value = value;
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbColumn"/> class.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        internal DbColumn(string tableName, string columnName)
        {
            Table = new DbTable(tableName);
            Name = columnName;
        }

        /// <summary>
        /// Gets or sets table.
        /// </summary>
        public DbTable Table { get; private set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets value.
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// Gets or sets type.
        /// </summary>
        public SqlDbType? Type { get; private set; }

        /// <summary>
        /// Gets the alias.
        /// </summary>
        public string Alias
        {
            get { return string.Format("[{0}.{1}]", Table.Name, Name); }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[{0}].[{1}]", Table.Name, Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comparisonOperator"></param>
        /// <param name="value"></param>
        /// <param name="otherValues"></param>
        /// <returns></returns>
        private Condition Compare(ComparisonOperator comparisonOperator, object value, params object[] otherValues)
        {
            var comparison = new Comparison(this, comparisonOperator, value, otherValues);
            return new Condition(comparison, comparison);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Condition GreaterThan(object value)
        {
            return Compare(ComparisonOperator.GreaterThan, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Condition GreaterThanOrEqualTo(object value)
        {
            return Compare(ComparisonOperator.GreaterThanOrEqualTo, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Condition LessThan(object value)
        {
            return Compare(ComparisonOperator.LessThan, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Condition LessThanOrEqualTo(object value)
        {
            return Compare(ComparisonOperator.LessThanOrEqualTo, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Condition EqualTo(object value)
        {
            return Compare(ComparisonOperator.EqualTo, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Condition NotEqualTo(object value)
        {
            return Compare(ComparisonOperator.NotEqualTo, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Condition Like(object value)
        {
            return Compare(ComparisonOperator.Like, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Condition NotLike(object value)
        {
            return Compare(ComparisonOperator.NotLike, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Condition BeginWith(object value)
        {
            return Compare(ComparisonOperator.BeginWith, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Condition NotBeginWith(object value)
        {
            return Compare(ComparisonOperator.NotBeginWith, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Condition EndWith(object value)
        {
            return Compare(ComparisonOperator.EndWith, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Condition NotEndWith(object value)
        {
            return Compare(ComparisonOperator.NotEndWith, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="otherValues"></param>
        /// <returns></returns>
        public Condition In(object value, params object[] otherValues)
        {
            return Compare(ComparisonOperator.In, value, otherValues);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="otherValues"></param>
        /// <returns></returns>
        public Condition NotIn(object value, params object[] otherValues)
        {
            return Compare(ComparisonOperator.NotIn, value, otherValues);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public Condition Between(object value1, object value2)
        {
            return Compare(ComparisonOperator.Between, value1, value2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public Condition NotBetween(object value1, object value2)
        {
            return Compare(ComparisonOperator.Between, value1, value2);
        }
    }
}
