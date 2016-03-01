namespace Newq
{
    using System.Collections.Generic;

    /// <summary>
    /// Target collection of statement or clause.
    /// </summary>
    public class Target
    {
        /// <summary>
        /// 
        /// </summary>
        private List<object> items;

        /// <summary>
        /// 
        /// </summary>
        private List<SortOrder> itemOrders;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public Target(DbContext dbContext)
        {
            DbContext = dbContext;
            items = new List<object>();
            itemOrders = new List<SortOrder>();
        }

        /// <summary>
        /// 
        /// </summary>
        public DbContext DbContext { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<object> GetTargetObjects()
        {
            return items;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IReadOnlyDictionary<object, SortOrder> GetTargetColumns()
        {
            var columns = new Dictionary<object, SortOrder>();

            for (int i = 0; i < items.Count; i++)
            {
                columns.Add(items[i], itemOrders[i]);
            }

            return columns;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var target = string.Empty;

            if (items.Count > 0)
            {
                items.ForEach(item => target += string.Format("{0}, ", item));
                target = target.Remove(target.Length - 2);
            }

            return target;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="order"></param>
        public void Add(object item, SortOrder order = SortOrder.Unspecified)
        {
            items.Add(item);
            itemOrders.Add(order);
        }
    }
}
