namespace Newq
{
    /// <summary>
    /// 
    /// </summary>
    public enum ComparisonOperator
    {
        /// <summary>
        /// 
        /// </summary>
        GreaterThan,

        /// <summary>
        /// 
        /// </summary>
        GreaterThanOrEqualTo,

        /// <summary>
        /// 
        /// </summary>
        LessThan,

        /// <summary>
        /// 
        /// </summary>
        LessThanOrEqualTo,

        /// <summary>
        /// 
        /// </summary>
        EqualTo,

        /// <summary>
        /// 
        /// </summary>
        NotEqualTo,

        /// <summary>
        /// The LIKE operator is used to
        /// search for a specified pattern in a column.
        /// </summary>
        Like,

        /// <summary>
        /// 
        /// </summary>
        NotLike,

        /// <summary>
        /// 
        /// </summary>
        BeginWith,

        /// <summary>
        /// 
        /// </summary>
        NotBeginWith,

        /// <summary>
        /// 
        /// </summary>
        EndWith,

        /// <summary>
        /// 
        /// </summary>
        NotEndWith,

        /// <summary>
        /// The IN operator allows you to
        /// specify multiple values in a WHERE clause.
        /// </summary>
        In,

        /// <summary>
        /// 
        /// </summary>
        NotIn,

        /// <summary>
        /// The BETWEEN operator selects values within a range.
        /// The values can be numbers, text, or dates.
        /// </summary>
        Between,

        /// <summary>
        /// The NOT BETWEEN operator selects values not within a range.
        /// The values can be numbers, text, or dates.
        /// </summary>
        NotBetween,
    }
}
