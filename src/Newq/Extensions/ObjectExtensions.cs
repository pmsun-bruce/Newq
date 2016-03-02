namespace Newq.Extensions
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToSqlValue(this object obj)
        {
            var value = string.Empty;

            if (obj is string || obj == null)
            {
                value = string.Format("'{0}'", obj);
            }
            else if (obj is DateTime)
            {
                value = string.Format("'{0}'",
                    ((DateTime)obj).ToString("yyyy-MM-dd hh:mm:ss fff"));
            }
            else
            {
                value = obj.ToString();
            }

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="comparisonOperator"></param>
        /// <returns></returns>
        public static string ToSqlValue(this object obj, ComparisonOperator comparisonOperator)
        {
            var value = string.Empty;

            switch (comparisonOperator)
            {
                case ComparisonOperator.Like:
                case ComparisonOperator.NotLike:
                    value = string.Format("'%{0}%'", obj);
                    break;
                case ComparisonOperator.BeginWith:
                case ComparisonOperator.NotBeginWith:
                    value = string.Format("'{0}%'", obj);
                    break;
                case ComparisonOperator.EndWith:
                case ComparisonOperator.NotEndWith:
                    value = string.Format("'%{0}'", obj);
                    break;
                default:
                    break;
            }

            return obj is DbColumn ? obj.ToString() : value;
        }
    }
}
