namespace Newq
{
    using System;
    using System.Collections.Generic;
    using Newq.Extensions;

    /// <summary>
    /// 
    /// </summary>
    public class Comparison
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Comparison"/> class.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="comparisonOperator"></param>
        /// <param name="value"></param>
        public Comparison(Column column, ComparisonOperator comparisonOperator, object value)
        {
            if ((object)column == null || value == null)
            {
                throw new ArgumentException();
            }

            Column = column;
            Operator = comparisonOperator;
            Values = new List<object> { value };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Comparison"/> class.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="comparisonOperator"></param>
        /// <param name="values"></param>
        public Comparison(Column column, ComparisonOperator comparisonOperator, object[] values)
        {
            if ((object)column == null || values == null || values.Length == 0)
            {
                throw new ArgumentException();
            }

            Column = column;
            Operator = comparisonOperator;
            Values = new List<object>();

            foreach (var obj in values)
            {
                Values.Add(obj);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Comparison"/> class.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="comparisonOperator"></param>
        /// <param name="values"></param>
        public Comparison(Column column, ComparisonOperator comparisonOperator, List<object> values)
        {
            if ((object)column == null || values == null || values.Count == 0)
            {
                throw new ArgumentException();
            }

            Column = column;
            Operator = comparisonOperator;
            Values = values;
        }

        /// <summary>
        /// Gets <see cref="Column"/>.
        /// </summary>
        public Column Column { get; }

        /// <summary>
        /// Gets or sets <see cref="Operator"/>.
        /// </summary>
        public ComparisonOperator Operator { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Values"/>.
        /// </summary>
        public List<object> Values { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetFirstValue()
        {
            var value = Values[0].ToSqlValue();

            if (value.Length < 2)
            {
                throw new Exception("value is null");
            }

            return Values[0].ToSqlValue();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetInValues()
        {
            var values = string.Empty;

            Values.ForEach(v => values += v.ToSqlValue() + ", ");

            if (values.Length < 3)
            {
                throw new Exception("values are null");
            }

            return values.Remove(values.Length - 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetBetweenValues()
        {
            var value1 = Values[0].ToSqlValue();
            var value2 = Values[1].ToSqlValue();

            if (value1.Length == 0 || value2.Length == 0)
            {
                throw new Exception("value is null");
            }

            return string.Format("{0} AND {1}", value1, value2);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            switch (Operator)
            {
                case ComparisonOperator.GreaterThan:
                    return string.Format("{0} > {1}", Column, GetFirstValue());
                case ComparisonOperator.GreaterThanOrEqualTo:
                    return string.Format("{0} >= {1}", Column, GetFirstValue());
                case ComparisonOperator.LessThan:
                    return string.Format("{0} < {1}", Column, GetFirstValue());
                case ComparisonOperator.LessThanOrEqualTo:
                    return string.Format("{0} <= {1}", Column, GetFirstValue());
                case ComparisonOperator.EqualTo:
                    return string.Format("{0} = {1}", Column, GetFirstValue());
                case ComparisonOperator.NotEqualTo:
                    return string.Format("{0} != {1}", Column, GetFirstValue());
                case ComparisonOperator.Like:
                    return string.Format("{0} LIKE {1}", Column, GetFirstValue());
                case ComparisonOperator.NotLike:
                    return string.Format("{0} NOT LIKE {1}", Column, GetFirstValue());
                case ComparisonOperator.In:
                    return string.Format("{0} IN ({1})", Column, GetInValues());
                case ComparisonOperator.NotIn:
                    return string.Format("{0} NOT IN ({1})", Column, GetInValues());
                case ComparisonOperator.Between:
                    return string.Format("{0} BETWEEN {1}", Column, GetBetweenValues());
                case ComparisonOperator.NotBetween:
                    return string.Format("{0} NOT BETWEEN {1}", Column, GetBetweenValues());
                default:
                    return string.Empty;
            }
        }
    }
}
