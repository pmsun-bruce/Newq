namespace Newq
{
    /// <summary>
    /// 
    /// </summary>
    public enum ComparisonOperator
    {
        /// <summary>
        /// Greater than a specified value.
        /// </summary>
        GreaterThan,

        /// <summary>
        /// Greater than or equal to a specified value.
        /// </summary>
        GreaterThanOrEqualTo,

        /// <summary>
        /// Less than a specified value.
        /// </summary>
        LessThan,

        /// <summary>
        /// Less than or equal to a specified value.
        /// </summary>
        LessThanOrEqualTo,

        /// <summary>
        /// Equal to a specified value.
        /// </summary>
        EqualTo,

        /// <summary>
        /// Not equal to a specified value.
        /// </summary>
        NotEqualTo,

        /// <summary>
        /// The LIKE operator is used to
        /// search for a specified pattern in a column.
        /// </summary>
        Like,

        /// <summary>
        /// The NOT LIKE operator is used to
        /// search for not match a specified pattern in a column.
        /// </summary>
        NotLike,

        /// <summary>
        /// The IN operator allows you to
        /// specify multiple values in a WHERE clause.
        /// </summary>
        In,

        /// <summary>
        /// The NOT IN operator allows you to
        /// specify multiple values in a WHERE clause.
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
