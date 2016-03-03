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

            if (obj is string)
            {
                value = string.Format("'{0}'", obj);
            }
            else if (obj is DateTime)
            {
                value = string.Format("'{0}'", ((DateTime)obj).ToString("yyyy-MM-dd hh:mm:ss fff"));
            }
            else
            {
                value = obj.ToString();
            }

            return value;
        }
    }
}
