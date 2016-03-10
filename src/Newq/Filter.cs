namespace Newq
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class Filter : ICustomizable<Action<Filter>>
    {
        /// <summary>
        /// 
        /// </summary>
        protected Action<Filter> handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="Filter"/> class.
        /// </summary>
        /// <param name="context"></param>
        public Filter(Context context)
        {
            Context = context;
            Items = new List<Condition>();
        }

        /// <summary>
        /// Gets or sets <see cref="Context"/>
        /// </summary>
        public Context Context { get; protected set; }

        /// <summary>
        /// Gets or sets <see cref="Items"/>
        /// </summary>
        public List<Condition> Items { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        public void SetHandler(Action<Filter> handler)
        {
            this.handler = handler;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Run()
        {
            handler(this);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var conditions = string.Empty;

            if (Items.Count > 0)
            {
                Items.ForEach(con => conditions += string.Format("{0} AND ", con));
                conditions = conditions.Remove(conditions.Length - 5);
            }

            return conditions;
        }

        /// <summary>
        /// Adds a <see cref="Condition"/> to the end of the filter.
        /// </summary>
        /// <param name="condition"></param>
        public void Add(Condition condition)
        {
            Items.Add(condition);
        }

        /// <summary>
        /// Removes the first occurrence of a specific <see cref="Condition"/> from the filter.
        /// </summary>
        /// <param name="condition"></param>
        public void Remove(Condition condition)
        {
            Items.Remove(condition);
        }

        /// <summary>
        /// Removes all conditions from the filter.
        /// </summary>
        public void Clear()
        {
            Items.Clear();
        }

        /// <summary>
        /// <see cref="Add(Condition)"/>.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static Filter operator +(Filter filter, Condition condition)
        {
            filter.Add(condition);

            return filter;
        }

        /// <summary>
        /// <see cref="Remove(Condition)"/>.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static Filter operator -(Filter filter, Condition condition)
        {
            filter.Remove(condition);

            return filter;
        }
    }
}
