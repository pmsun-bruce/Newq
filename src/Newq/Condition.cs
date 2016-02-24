namespace Newq
{
    /// <summary>
    /// 
    /// </summary>
    public class Condition
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="logicalOperator"></param>
        public Condition(Comparison source, Comparison target, LogicalOperator logicalOperator = LogicalOperator.And)
        {
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
            Source = source;
            Target = target;
            LogicalOperator = logicalOperator;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public object Source { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public object Target { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public LogicalOperator LogicalOperator { get; private set; }
        
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
        /// 
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
                    Source.ToString(),
                    GetLinkType(LogicalOperator),
                    Target.ToString());
            }
            
            return condition;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public Condition And(Comparison comparison)
        {
            return And(new Condition(comparison, comparison));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Condition And(Condition condition)
        {
            return new Condition(this, condition);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public Condition Or(Comparison comparison)
        {
            return Or(new Condition(comparison, comparison));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public Condition Or(Condition condition)
        {
            return new Condition(this, condition, LogicalOperator.Or);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum LogicalOperator
    {
        And,
        Or,
    }
}
