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
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
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
        /// <returns></returns>
        private Condition Compare(ComparisonOperator comparisonOperator, object value)
        {
            var comparison = new Comparison(this, comparisonOperator, value);

            return new Condition(comparison, comparison);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comparisonOperator"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        private Condition Compare(ComparisonOperator comparisonOperator, object[] values)
        {
            var comparison = new Comparison(this, comparisonOperator, values);

            return new Condition(comparison, comparison);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string IsNull()
        {
            return string.Format("{0} IS NULL", this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string IsNotNull()
        {
            return string.Format("{0} IS NOT NULL", this);
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
        /// <param name="type"></param>
        /// <param name="escape"></param>
        /// <returns></returns>
        public Condition Like(object value, PatternType type = PatternType.Fuzzy, char escape = ' ')
        {
            return Compare(ComparisonOperator.Like, new Pattern(value, type, escape));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="escape"></param>
        /// <returns></returns>
        public Condition NotLike(object value, PatternType type = PatternType.Fuzzy, char escape = ' ')
        {
            return Compare(ComparisonOperator.NotLike, new Pattern(value, type, escape));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public Condition In(object[] values)
        {
            return Compare(ComparisonOperator.In, values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subQuery"></param>
        /// <returns></returns>
        public Condition In(QueryBuilder subQuery)
        {
            return Compare(ComparisonOperator.In, subQuery);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public Condition NotIn(object[] values)
        {
            return Compare(ComparisonOperator.NotIn, values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subQuery"></param>
        /// <returns></returns>
        public Condition NotIn(QueryBuilder subQuery)
        {
            return Compare(ComparisonOperator.NotIn, subQuery);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public Condition Between(object value1, object value2)
        {
            return Compare(ComparisonOperator.Between, new object[] { value1, value2 });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public Condition NotBetween(object value1, object value2)
        {
            return Compare(ComparisonOperator.NotBetween, new object[] { value1, value2 });
        }

        /// <summary>
        /// <see cref="EqualTo(object)"/>
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Condition operator ==(DbColumn column, object value)
        {
            return column.EqualTo(value);
        }

        /// <summary>
        /// <see cref="NotEqualTo(object)"/>
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Condition operator !=(DbColumn column, object value)
        {
            return column.NotEqualTo(value);
        }

        /// <summary>
        /// <see cref="GreaterThan(object)"/>
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Condition operator >(DbColumn column, object value)
        {
            return column.GreaterThan(value);
        }

        /// <summary>
        /// <see cref="LessThan(object)"/>
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Condition operator <(DbColumn column, object value)
        {
            return column.LessThan(value);
        }

        /// <summary>
        /// <see cref="GreaterThanOrEqualTo(object)"/>
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Condition operator >=(DbColumn column, object value)
        {
            return column.GreaterThanOrEqualTo(value);
        }

        /// <summary>
        /// <see cref="LessThanOrEqualTo(object)"/>
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Condition operator <=(DbColumn column, object value)
        {
            return column.LessThanOrEqualTo(value);
        }
    }
}
