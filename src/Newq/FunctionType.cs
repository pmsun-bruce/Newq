namespace Newq
{
    /// <summary>
    /// 
    /// </summary>
    public enum FunctionType
    {
        /* Aggregate Functions */

        /// <summary>
        /// The MAX() function returns the largest value of the selected column.
        /// </summary>
        MAX,

        /// <summary>
        /// The MIN() function returns the smallest value of the selected column.
        /// </summary>
        MIN,

        /// <summary>
        /// <para>
        /// The COUNT(column_name) function returns the number of
        /// values (NULL values will not be counted) of the specified column.
        /// </para>
        /// <para>
        /// The COUNT(*) function returns the number of records in a table.
        /// </para>
        /// <para>
        /// The COUNT(DISTINCT column_name) function returns the number of
        /// distinct values of the specified column
        /// </para>
        /// </summary>
        COUNT,

        /// <summary>
        /// 
        /// </summary>
        COUNT_BIG,

        /// <summary>
        /// The AVG() function returns the average value of a numeric column.
        /// </summary>
        AVG,

        /// <summary>
        /// The SUM() function returns the total sum of a numeric column.
        /// </summary>
        SUM,

        /// <summary>
        /// 
        /// </summary>
        CHECKSUM_AGG,

        /// <summary>
        /// 
        /// </summary>
        STDEV,

        /// <summary>
        /// 
        /// </summary>
        STDEVP,

        /// <summary>
        /// 
        /// </summary>
        GROUPING,

        /// <summary>
        /// 
        /// </summary>
        GROUPING_ID,

        /// <summary>
        /// 
        /// </summary>
        VAR,

        /// <summary>
        /// 
        /// </summary>
        VARP,

        /* Conversion Functions */

        CAST,

        /// <summary>
        /// 
        /// </summary>
        CONVERT,

        /// <summary>
        /// 
        /// </summary>
        PARSE,

        /// <summary>
        /// 
        /// </summary>
        TRY_CAST,

        /// <summary>
        /// 
        /// </summary>
        TRY_CONVERT,

        /// <summary>
        /// 
        /// </summary>
        TRY_PARSE,

        /* Ranking Functions */

        /// <summary>
        /// 
        /// </summary>
        RANK,

        /// <summary>
        /// 
        /// </summary>
        NTILE,

        /// <summary>
        /// 
        /// </summary>
        DENSE_RANK,

        /// <summary>
        /// 
        /// </summary>
        ROW_NUMBER,

        /* String Functions */

        /// <summary>
        /// 
        /// </summary>
        ASCII,

        /// <summary>
        /// 
        /// </summary>
        LTRIM,

        /// <summary>
        /// 
        /// </summary>
        SOUNDEX,

        /// <summary>
        /// 
        /// </summary>
        CHAR,

        /// <summary>
        /// 
        /// </summary>
        NCHAR,

        /// <summary>
        /// 
        /// </summary>
        SPACE,

        /// <summary>
        /// 
        /// </summary>
        CHARINDEX,

        /// <summary>
        /// 
        /// </summary>
        PATINDEX,

        /// <summary>
        /// 
        /// </summary>
        STR,

        /// <summary>
        /// 
        /// </summary>
        CONCAT,

        /// <summary>
        /// 
        /// </summary>
        QUOTENAME,

        /// <summary>
        /// 
        /// </summary>
        STUFF,

        /// <summary>
        /// 
        /// </summary>
        DIFFERENCE,

        /// <summary>
        /// 
        /// </summary>
        REPLACE,

        /// <summary>
        /// 
        /// </summary>
        SUBSTRING,

        /// <summary>
        /// 
        /// </summary>
        FORMAT,

        /// <summary>
        /// 
        /// </summary>
        REPLICATE,

        /// <summary>
        /// 
        /// </summary>
        UNICODE,

        /// <summary>
        /// 
        /// </summary>
        LEFT,

        /// <summary>
        /// 
        /// </summary>
        REVERSE,

        /// <summary>
        /// 
        /// </summary>
        UPPER,

        /// <summary>
        /// 
        /// </summary>
        LEN,

        /// <summary>
        /// 
        /// </summary>
        RIGHT,

        /// <summary>
        /// 
        /// </summary>
        LOWER,

        /// <summary>
        /// 
        /// </summary>
        RTRIM,

        /* Mathematical Functions */

        /// <summary>
        /// 
        /// </summary>
        ABS,

        /// <summary>
        /// 
        /// </summary>
        DEGREES,

        /// <summary>
        /// 
        /// </summary>
        RAND,

        /// <summary>
        /// 
        /// </summary>
        ACOS,

        /// <summary>
        /// 
        /// </summary>
        EXP,

        /// <summary>
        /// The ROUND() function is used to
        /// round a numeric field to the number of decimals specified.
        /// </summary>
        ROUND,

        /// <summary>
        /// 
        /// </summary>
        ASIN,

        /// <summary>
        /// 
        /// </summary>
        FLOOR,

        /// <summary>
        /// 
        /// </summary>
        SIGN,

        /// <summary>
        /// 
        /// </summary>
        ATAN,

        /// <summary>
        /// 
        /// </summary>
        LOG,

        /// <summary>
        /// 
        /// </summary>
        SIN,

        /// <summary>
        /// 
        /// </summary>
        ATN2,

        /// <summary>
        /// 
        /// </summary>
        LOG10,

        /// <summary>
        /// 
        /// </summary>
        SQRT,

        /// <summary>
        /// 
        /// </summary>
        CEILING,

        /// <summary>
        /// 
        /// </summary>
        PI,

        /// <summary>
        /// 
        /// </summary>
        SQUARE,

        /// <summary>
        /// 
        /// </summary>
        COS,

        /// <summary>
        /// 
        /// </summary>
        POWER,

        /// <summary>
        /// 
        /// </summary>
        TAN,

        /// <summary>
        /// 
        /// </summary>
        COT,

        /// <summary>
        /// 
        /// </summary>
        RADIANS,

        /* System Functions */

        /// <summary>
        /// 
        /// </summary>
        ISNULL,

        /// <summary>
        /// 
        /// </summary>
        ISNUMERIC,

        /// <summary>
        /// 
        /// </summary>
        NEWID,
    }
}
