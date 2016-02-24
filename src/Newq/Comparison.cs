namespace Newq
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class Comparison
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="column"></param>
        /// <param name="comparisonOperator"></param>
        /// <param name="value"></param>
        /// <param name="otherValues"></param>
        public Comparison(DbColumn column, ComparisonOperator comparisonOperator, object value, params object[] otherValues)
        {
            Column = column;
            Operator = comparisonOperator;
            SetValues(value, otherValues);
        }

        /// <summary>
        /// 
        /// </summary>
        public DbColumn Column { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public ComparisonOperator? Operator { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyList<object> Values { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="otherValues"></param>
        private void SetValues(object value, params object[] otherValues)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var values = new List<object> { value };

            if (otherValues.Length > 0)
            {
                foreach (var val in otherValues)
                {
                    values.Add(val);
                }
            }

            Values = values;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var comparison = string.Empty;

            switch (Operator)
            {
                case ComparisonOperator.GreaterThan:
                    comparison = string.Format("{0} > {1}", Column, Values[0].ToSqlValue());
                    break;
                case ComparisonOperator.GreaterThanOrEqualTo:
                    comparison = string.Format("{0} >= {1}", Column, Values[0].ToSqlValue());
                    break;
                case ComparisonOperator.LessThan:
                    comparison = string.Format("{0} < {1}", Column, Values[0].ToSqlValue());
                    break;
                case ComparisonOperator.LessThanOrEqualTo:
                    comparison = string.Format("{0} <= {1}", Column, Values[0].ToSqlValue());
                    break;
                case ComparisonOperator.EqualTo:
                    comparison = string.Format("{0} = {1}", Column, Values[0].ToSqlValue());
                    break;
                case ComparisonOperator.NotEqualTo:
                    comparison = string.Format("{0} > {1}", Column, Values[0].ToSqlValue());
                    break;
                case ComparisonOperator.Like:
                    comparison = string.Format("{0} LIKE {1}", Column, Values[0].ToSqlValue(ComparisonOperator.Like));
                    break;
                case ComparisonOperator.NotLike:
                    comparison = string.Format("{0} NOT LIKE {1}", Column, Values[0].ToSqlValue(ComparisonOperator.NotLike));
                    break;
                case ComparisonOperator.BeginWith:
                    comparison = string.Format("{0} LIKE {1}", Column, Values[0].ToSqlValue(ComparisonOperator.BeginWith));
                    break;
                case ComparisonOperator.NotBeginWith:
                    comparison = string.Format("{0} NOT LIKE {1}", Column, Values[0].ToSqlValue(ComparisonOperator.NotBeginWith));
                    break;
                case ComparisonOperator.EndWith:
                    comparison = string.Format("{0} LIKE {1}", Column, Values[0].ToSqlValue(ComparisonOperator.EndWith));
                    break;
                case ComparisonOperator.NotEndWith:
                    comparison = string.Format("{0} NOT LIKE {1}", Column, Values[0].ToSqlValue(ComparisonOperator.NotEndWith));
                    break;
                case ComparisonOperator.In:
                    foreach (var val in Values)
                    {
                        comparison += string.Concat(val.ToSqlValue(), ", ");
                    }

                    comparison = string.Format("{0} IN ({1})", Column, comparison.Remove(comparison.Length - 2));
                    break;
                case ComparisonOperator.NotIn:
                    foreach (var val in Values)
                    {
                        comparison += string.Concat(val.ToSqlValue(), ", ");
                    }

                    comparison = string.Format("{0} NOT IN ({1})", Column, comparison.Remove(comparison.Length - 2));
                    break;
                case ComparisonOperator.Between:
                    comparison = string.Format("{0} BETWEEN {1} AND {2}",
                        Column, Values[0].ToSqlValue(), Values[1].ToSqlValue());
                    break;
                case ComparisonOperator.NotBetween:
                    comparison = string.Format("{0} NOT BETWEEN {1} AND {2}",
                        Column, Values[0].ToSqlValue(), Values[1].ToSqlValue());
                    break;
                default:
                    break;
            }

            return comparison;
        }
    }
}
