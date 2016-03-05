namespace Newq
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class Condition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Condition"/> class.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="logicalOperator"></param>
        public Condition(Comparison source, Comparison target, LogicalOperator logicalOperator = LogicalOperator.And)
        {
            if (source == null || target == null)
            {
                throw new ArgumentNullException("source or target is null");
            }

            Source = source;
            Target = target;
            LogicalOperator = logicalOperator;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="logicalOperator"></param>
        public Condition(Condition source, Condition target, LogicalOperator logicalOperator = LogicalOperator.And)
        {
            if (source == null || target == null)
            {
                throw new ArgumentNullException("source or target is null");
            }

            Source = source;
            Target = target;
            LogicalOperator = logicalOperator;
        }

        /// <summary>
        /// Gets or sets <see cref="Source"/>.
        /// </summary>
        public object Source { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Target"/>.
        /// </summary>
        public object Target { get; set; }

        /// <summary>
        /// Gets or sets <see cref="LogicalOperator"/>.
        /// </summary>
        public LogicalOperator LogicalOperator { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logicalOperator"></param>
        /// <returns></returns>
        private string GetLinkType(LogicalOperator logicalOperator)
        {
            return logicalOperator == LogicalOperator.And ? "AND" : "OR";
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var condition = string.Empty;
            
            if (Source == null || Target == null)
            {
                return condition;
            }
            
            if (Source.Equals(Target))
            {
                condition = Source.ToString();
            }
            else
            {
                condition = string.Format("({0} {1} {2})",
                    Source, GetLinkType(LogicalOperator), Target);
            }
            
            return condition;
        }

        /// <summary>
        /// Returns a <see cref="Condition"/> that 'and' with a comparison.
        /// </summary>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public Condition And(Comparison comparison)
        {
            return And(new Condition(comparison, comparison));
        }

        /// <summary>
        /// Returns a <see cref="Condition"/> that 'and' with another condition.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Condition And(Condition condition)
        {
            return new Condition(this, condition);
        }

        /// <summary>
        /// Returns a <see cref="Condition"/> that 'or' with a comparison.
        /// </summary>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public Condition Or(Comparison comparison)
        {
            return Or(new Condition(comparison, comparison));
        }

        /// <summary>
        /// Returns a <see cref="Condition"/> that 'or' with another condition.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Condition Or(Condition condition)
        {
            return new Condition(this, condition, LogicalOperator.Or);
        }

        /// <summary>
        /// <see cref="And(Comparison)"/>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Condition operator &(Condition source, Comparison target)
        {
            return source.And(target);
        }

        /// <summary>
        /// <see cref="And(Condition)"/>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Condition operator &(Condition source, Condition target)
        {
            return source.And(target);
        }

        /// <summary>
        /// <see cref="Or(Comparison)"/>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Condition operator |(Condition source, Comparison target)
        {
            return source.Or(target);
        }

        /// <summary>
        /// <see cref="Or(Condition)"/>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Condition operator |(Condition source, Condition target)
        {
            return source.Or(target);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum LogicalOperator
    {
        /// <summary>
        /// The AND operator displays a record
        /// if both the first condition AND the second condition are true.
        /// </summary>
        And,

        /// <summary>
        /// The OR operator displays a record
        /// if either the first condition OR the second condition is true.
        /// </summary>
        Or,
    }
}
