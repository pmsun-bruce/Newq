namespace Newq
{
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class Filter
    {
        public Filter(DbContext dbContext)
        {
            DbContext = dbContext;
            Conditions = new List<Condition>();
        }

        /// <summary>
        /// 
        /// </summary>
        public DbContext DbContext { get; private set; }

        /// <summary>
        /// Filter conditions
        /// </summary>
        public List<Condition> Conditions { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var conditions = string.Empty;

            if (Conditions.Count > 0)
            {
                Conditions.ForEach(con => conditions += string.Format("{0} AND ", con));
                conditions = conditions.Remove(conditions.Length - 5);
            }

            return conditions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        public void Add(Condition condition)
        {
            Conditions.Add(condition);
        }
    }
}
