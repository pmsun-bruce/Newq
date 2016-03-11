namespace Newq
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Target collection of statement or clause.
    /// </summary>
    public class Target : ICustomizable<Action<Target, Context>>
    {
        /// <summary>
        /// 
        /// </summary>
        protected Action<Target, Context> customization;

        /// <summary>
        /// 
        /// </summary>
        protected Context context;

        /// <summary>
        /// Initializes a new instance of the <see cref="Target"/> class.
        /// </summary>
        /// <param name="context"></param>
        public Target(Context context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this.context = context;
            Items = new List<object>();
        }

        /// <summary>
        /// 
        /// </summary>
        public List<object> Items { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customization"></param>
        public void Customize(Action<Target, Context> customization)
        {
            this.customization = customization;
        }

        /// <summary>
        /// Returns a result after customization be performed.
        /// </summary>
        public bool Perform()
        {
            if (customization != null)
            {
                Items.Clear();
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
            var target = string.Empty;

            Perform();
            Items.ForEach(item => target += string.Format("{0}, ", item));

            return target.Length > 0 ? target.Remove(target.Length - 2) : target;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<OrderByColumn> GetOrderByColumns()
        {
            var list = new List<OrderByColumn>();

            Items.ForEach(item => {
                if (item is OrderByColumn)
                {
                    list.Add(item as OrderByColumn);
                }
            });

            return list;
        }

        /// <summary>
        /// Adds a object to the end of the target.
        /// </summary>
        /// <param name="item"></param>
        public void Add(object item)
        {
            Items.Add(item);
        }

        /// <summary>
        /// Adds a object to the end of the target.
        /// </summary>
        /// <param name="item"></param>
        public void Add(OrderByColumn item)
        {
            Items.Add(item);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the target.
        /// </summary>
        /// <param name="item"></param>
        public void Remove(object item)
        {
            Items.Remove(item);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the target.
        /// </summary>
        /// <param name="item"></param>
        public void Remove(OrderByColumn item)
        {
            Items.Remove(item);
        }

        /// <summary>
        /// <see cref="Add(object)"/>
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
        /// <see cref="Add(OrderByColumn)"/>
        /// </summary>
        /// <param name="target"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Target operator +(Target target, OrderByColumn item)
        {
            target.Add(item);

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

        /// <summary>
        /// <see cref="Remove(OrderByColumn)"/>
        /// </summary>
        /// <param name="target"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Target operator -(Target target, OrderByColumn item)
        {
            target.Remove(item);

            return target;
        }
    }
}
