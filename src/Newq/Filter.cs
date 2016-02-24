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
        /// 
        /// </summary>
        public List<Condition> Conditions { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var conditions = string.Empty;

            Conditions.ForEach(con => conditions += string.Format("{0} AND ", con));

            return conditions.Remove(conditions.Length - 5);
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
