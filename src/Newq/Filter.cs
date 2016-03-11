namespace Newq
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class Filter : ICustomizable<Action<Filter, Context>>
    {
        /// <summary>
        /// 
        /// </summary>
        protected Action<Filter, Context> customization;

        /// <summary>
        /// 
        /// </summary>
        protected Context context;

        /// <summary>
        /// Initializes a new instance of the <see cref="Filter"/> class.
        /// </summary>
        /// <param name="context"></param>
        public Filter(Context context)
        {
            this.context = context;
            Items = new List<Condition>();
        }

        /// <summary>
        /// Gets or sets <see cref="Items"/>
        /// </summary>
        public List<Condition> Items { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customization"></param>
        public void Customize(Action<Filter, Context> customization)
        {
            this.customization = customization;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Perform()
        {
            Items.Clear();

            if (customization != null)
            {
                customization(this, context);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a string that represents the current customization.
        /// </summary>
        /// <returns></returns>
        public string GetCustomization()
        {
            var filter = string.Empty;

            Perform();
            Items.ForEach(item => filter += string.Format("{0} AND ", item));

            return filter.Length > 0 ? filter.Remove(filter.Length - 5) : filter;
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
