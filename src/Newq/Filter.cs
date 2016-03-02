namespace Newq
{
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class Filter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Filter"/> class.
        /// </summary>
        /// <param name="dbContext"></param>
        public Filter(DbContext dbContext)
        {
            DbContext = dbContext;
            Conditions = new List<Condition>();
        }

        /// <summary>
        /// Gets or sets database context.
        /// </summary>
        public DbContext DbContext { get; private set; }

        /// <summary>
        /// Filter conditions.
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
        /// Adds an condition to the end of the filter.
        /// </summary>
        /// <param name="condition"></param>
        public void Add(Condition condition)
        {
            Conditions.Add(condition);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the filter.
        /// </summary>
        /// <param name="condition"></param>
        public void Remove(Condition condition)
        {
            Conditions.Remove(condition);
        }

        /// <summary>
        /// Removes all conditions from the filter.
        /// </summary>
        public void Clear()
        {
            Conditions.Clear();
        }
    }
}
