namespace Newq
{
    /// <summary>
    /// 
    /// </summary>
    public enum FunctionType
    {
        /* Aggregate Functions */

        MAX,
        MIN,
        COUNT,
        COUNT_BIG,
        AVG,
        SUM,
        CHECKSUM_AGG,
        STDEV,
        STDEVP,
        GROUPING,
        GROUPING_ID,
        VAR,
        VARP,

        /* Conversion Functions */

        CAST,
        CONVERT,
        PARSE,
        TRY_CAST,
        TRY_CONVERT,
        TRY_PARSE,

        /* Ranking Functions */

        RANK,
        NTILE,
        DENSE_RANK,
        ROW_NUMBER,

        /* String Functions */

        ASCII,
        LTRIM,
        SOUNDEX,
        CHAR,
        NCHAR,
        SPACE,
        CHARINDEX,
        PATINDEX,
        STR,
        CONCAT,
        QUOTENAME,
        STUFF,
        DIFFERENCE,
        REPLACE,
        SUBSTRING,
        FORMAT,
        REPLICATE,
        UNICODE,
        LEFT,
        REVERSE,
        UPPER,
        LEN,
        RIGHT,
        LOWER,
        RTRIM,

        /* Mathematical Functions */

        ABS,
        DEGREES,
        RAND,
        ACOS,
        EXP,
        ROUND,
        ASIN,
        FLOOR,
        SIGN,
        ATAN,
        LOG,
        SIN,
        ATN2,
        LOG10,
        SQRT,
        CEILING,
        PI,
        SQUARE,
        COS,
        POWER,
        TAN,
        COT,
        RADIANS,

        /* System Functions */

        ISNULL,
        ISNUMERIC,
        NEWID,
    }
}
