namespace Newq
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Target collection of statement or clause.
    /// </summary>
    public class Target
    {
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<object, SortOrder> items;

        /// <summary>
        /// Initializes a new instance of the <see cref="Target"/> class.
        /// </summary>
        /// <param name="context"></param>
        public Target(Context context)
        {
            Context = context;
            items = new Dictionary<object, SortOrder>();
        }

        /// <summary>
        /// 
        /// </summary>
        public Context Context { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object[] GetTargetObjects()
        {
            return items.Keys.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IReadOnlyDictionary<object, SortOrder> GetTargetColumns()
        {
            return items;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var target = string.Empty;

            if (items.Count > 0)
            {
                foreach (var item in items)
                {
                    target += string.Format("{0}, ", item.Key);
                }

                target = target.Remove(target.Length - 2);
            }

            return target;
        }

        /// <summary>
        /// Adds a object to the end of the target.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="order"></param>
        public void Add(object item, SortOrder order = SortOrder.Unspecified)
        {
            items.Add(item, order);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the target.
        /// </summary>
        /// <param name="item"></param>
        public void Remove(object item)
        {

        }

        /// <summary>
        /// Removes all objects from the target.
        /// </summary>
        public void Clear()
        {
            items.Clear();
        }

        /// <summary>
        /// <see cref="Add(object, SortOrder)"/>
        /// </summary>
        /// <param name="target"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Target operator +(Target target, object item)
        {
            target.Add(item);

            return target;
        }

        /// <summary>
        /// <see cref="Add(object, SortOrder)"/>
        /// </summary>
        /// <param name="target"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Target operator +(Target target, KeyValuePair<Column, SortOrder> item)
        {
            target.Add(item.Key, item.Value);

            return target;
        }

        /// <summary>
        /// <see cref="Remove(object)"/>
        /// </summary>
        /// <param name="target"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Target operator -(Target target, object item)
        {
            target.Remove(item);

            return target;
        }
    }
}
